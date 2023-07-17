using DocumentOperation.Data.Entities;
using FluentValidation;
using System.Reflection.Metadata;

namespace DocumentOperation.API.ValidationRules
{
    public class InvoiceValidator: AbstractValidator<Document>
    {
        public InvoiceValidator()
        {
            //RuleFor(model => model.Property1).NotEmpty().WithMessage("Property1 is required.");
            //RuleFor(model => model.Property2).Length(1, 10).WithMessage("Property2 must be between 1 and 10 characters.");
            // Add more validation rules as needed
        }
    }
}
