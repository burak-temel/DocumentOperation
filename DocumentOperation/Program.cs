using DocumentOperation.API.Mapping;
using DocumentOperation.Services;
using DocumentOperation.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Serilog;
using DocumentOperation.ServiceContracts;
using FluentValidation.AspNetCore;
using Quartz.Spi;
using DocumentOperation.API.Models;
using DocumentOperation.API.ValidationRules;
using FluentValidation;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables();

        builder.Services.AddTransient<IInvoiceService, InvoiceService>();
        builder.Services.AddTransient<IEmailService, EmailService>();
        builder.Services.AddTransient<DocumentProcessingJob>();
        #region FluentValidation

        #endregion
        builder.Services.AddQuartz(options =>
        {
            options.UseMicrosoftDependencyInjectionScopedJobFactory();
        });

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            
        builder.Services.AddAutoMapper(typeof(MappingProfile));
        //builder.Services.AddControllers()
        //    .ConfigureApiBehaviorOptions(options =>
        //    {
        //        options.SuppressModelStateInvalidFilter = true;
        //    })
        //    .AddFluentValidation();

        // Register FluentValidation validators
        builder.Services.AddControllers().AddFluentValidation(fv =>
        {
            fv.RegisterValidatorsFromAssemblyContaining<InvoiceValidator>();
            fv.RegisterValidatorsFromAssemblyContaining<InvoiceHeaderValidator>();
            fv.RegisterValidatorsFromAssemblyContaining<InvoiceDetailValidator>();
        });

        // Register the custom action filter globally
        builder.Services.AddScoped<FluentValidatorInterceptor>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Logging.AddConsole();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();
        app.MapControllers();

        #region Logging
        Log.Logger = Logging.LoggerFactory.ConfigureLogger<Program>();
        #endregion

        #region Quartz
        // Get the Quartz.NET scheduler factory
        var schedulerFactory = app.Services.GetRequiredService<ISchedulerFactory>();
        var scheduler = await schedulerFactory.GetScheduler();

        // Create a job detail for the DocumentProcessingJob
        var jobDetail = JobBuilder.Create<DocumentProcessingJob>()
            .WithIdentity("DocumentProcessingJob", "DocumentGroup")
            .Build();

        // Create a trigger with the desired Cron Expression or interval
        var trigger = TriggerBuilder.Create()
            .WithIdentity("DocumentProcessingTrigger", "DocumentGroup")
            .StartNow()
            .WithSimpleSchedule(x => x
                .WithIntervalInMinutes(2) // Run every 2 minutes
                .RepeatForever())
            .Build();

        // Schedule the job with the trigger
        Log.Information("Scheduling job");
        await scheduler.ScheduleJob(jobDetail, trigger);
        Log.Information("Job scheduled");

        Log.Information("Starting Quartz.NET scheduler");
        await scheduler.Start();
        Log.Information("Quartz.NET scheduler started");
        #endregion

        app.Run();
    }
}
