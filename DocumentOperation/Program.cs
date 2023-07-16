using DocumentOperation.API.Mapping;
using DocumentOperation.Services;
using DocumentOperation.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl;


internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables();

        builder.Services.AddTransient<DocumentService>();

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddAutoMapper(typeof(MappingProfile));
        builder.Services.AddControllers();
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

        #region Quartz
        // Create a job detail for the DocumentProcessingJob
        var jobDetail = JobBuilder.Create<DocumentProcessingJob>()
            .WithIdentity("DocumentProcessingJob", "DocumentGroup")
            .Build();

        // Create a trigger with the desired Cron Expression or interval
        var trigger = TriggerBuilder.Create()
            .WithIdentity("DocumentProcessingTrigger", "DocumentGroup")
            .WithCronSchedule("0 0 8 ? * MON-FRI") // Example Cron Expression: Run at 8:00 AM from Monday to Friday
            .Build();

        // Get the scheduler from the scheduler factory
        var schedulerFactory = new StdSchedulerFactory();
        var scheduler = await schedulerFactory.GetScheduler();

        // Schedule the job with the trigger
        await scheduler.ScheduleJob(jobDetail, trigger);

        await scheduler.Start();
        #endregion

        app.Run();

    }

}