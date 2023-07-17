namespace DocumentOperation.ServiceContracts
{
    public interface IEmailService
    {
        Task SendEmail(string recipientEmail, string subject, string body);
    }
}
