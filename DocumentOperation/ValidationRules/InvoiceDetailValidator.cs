using DocumentOperation.API.Models;
using FluentValidation;

namespace DocumentOperation.API.ValidationRules
{
    public class InvoiceDetailValidator : AbstractValidator<InvoiceDetailViewModel>
    {
        public InvoiceDetailValidator()
        {
            //RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name is required.");
            //RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name is required.");
            //RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Invalid Email Address.");
        }
    }
}
