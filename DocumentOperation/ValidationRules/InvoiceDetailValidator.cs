using DocumentOperation.API.Models;
using FluentValidation;

namespace DocumentOperation.API.ValidationRules
{
    public class InvoiceDetailValidator : AbstractValidator<InvoiceDetailViewModel>
    {
        public InvoiceDetailValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.UnitCode).NotEmpty().WithMessage("UnitCode is required.");
        }
    }
}
