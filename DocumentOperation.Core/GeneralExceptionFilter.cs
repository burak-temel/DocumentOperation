using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

public class GeneralExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        // Log the exception using Serilog or any other logging mechanism
        Log.Error(context.Exception, "An unhandled exception occurred.");

        // Set the result to a custom error response or message
        context.Result = new ObjectResult("An error occurred. Please try again later.")
        {
            StatusCode = 500 // Internal Server Error
        };

        context.ExceptionHandled = true;
    }
}
