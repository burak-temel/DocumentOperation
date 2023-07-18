using DocumentOperation.API.Models;
using DocumentOperation.Data.Entities;
using FluentValidation;

namespace DocumentOperation.API.ValidationRules
{
    public class InvoiceHeaderValidator : AbstractValidator<InvoiceHeaderViewModel>
    {
        public InvoiceHeaderValidator()
        {
            RuleFor(x => x.SenderTitle).NotEmpty().WithMessage("SenderTitle is required.");
            RuleFor(x => x.ReceiverTitle).NotEmpty().WithMessage("ReceiverTitle is required.");
            RuleFor(x => x.Date).NotEmpty().WithMessage("Date is required.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.").EmailAddress().WithMessage("Invalid email format.");
        }
    }
}
