using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

public class GeneralExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        Log.Error(context.Exception, "An unhandled exception occurred.");

        context.Result = new ObjectResult("An error occurred. Please try again later.")
        {
            StatusCode = 500
        };

        context.ExceptionHandled = true;
    }
}
