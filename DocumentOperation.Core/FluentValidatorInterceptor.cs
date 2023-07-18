using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

public class FluentValidatorInterceptor : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var model = context.ActionArguments.Values.FirstOrDefault();

        if (context.ActionDescriptor.Parameters.Count != 0 && (model == null || !IsValidModel(model, context)))
        {
            context.Result = new BadRequestObjectResult("Invalid model data.");
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    private bool IsValidModel(object model, ActionExecutingContext context)
    {
        if (model == null)
            return false;

        var modelType = model.GetType();

        var validatorType = typeof(IValidator<>).MakeGenericType(modelType);

        var validator = context.HttpContext.RequestServices.GetService(validatorType);

        if (validator == null)
            return true;

        var validationResult = ((dynamic)validator).Validate((dynamic)model);

        return validationResult.IsValid;
    }
}
