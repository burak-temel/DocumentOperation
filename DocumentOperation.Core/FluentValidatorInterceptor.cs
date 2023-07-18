using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

public class FluentValidatorInterceptor : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var model = context.ActionArguments.Values.FirstOrDefault();

        if (context.ActionDescriptor.Parameters.Count == 0)
        {
            return;
        }

        if (model == null || !IsValidModel(model, context))
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

        // Get the model's type
        var modelType = model.GetType();

        // Get the validator type for the model
        var validatorType = typeof(IValidator<>).MakeGenericType(modelType);

        // Resolve the validator using the DI container
        var validator = context.HttpContext.RequestServices.GetService(validatorType);

        // If the validator is not found, return true (consider model as valid)
        if (validator == null)
            return true;

        // Invoke the Validate method on the validator
        var validationResult = ((dynamic)validator).Validate((dynamic)model);

        // Check if the validation result is valid
        return validationResult.IsValid;
    }
}
