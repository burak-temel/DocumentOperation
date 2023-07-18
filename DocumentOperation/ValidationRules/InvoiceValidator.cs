using DocumentOperation.API.Models;
using DocumentOperation.Data.Entities;
using FluentValidation;

namespace DocumentOperation.API.ValidationRules
{
    public class InvoiceValidator : AbstractValidator<InvoiceViewModel>
    {
        public InvoiceValidator()
        {
            RuleFor(x => x.InvoiceHeader).NotNull().WithMessage("InvoiceHeader is required.");
            RuleFor(x => x.InvoiceHeader).SetValidator(new InvoiceHeaderValidator()); // Use the InvoiceHeaderValidator here

            // Use RuleForEach to validate each InvoiceDetail in InvoiceLines using InvoiceDetailValidator
            RuleForEach(x => x.InvoiceLine)
                .SetValidator(new InvoiceDetailValidator());
        }
    }
}

