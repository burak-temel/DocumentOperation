using DocumentOperation.Core;
using DocumentOperation.ServiceContracts;
using DocumentOperation.Services;
using Quartz;
using Serilog;

public class DocumentProcessingJob : IJob
{
    private readonly IInvoiceService _documentService;
    private readonly IEmailService _emailService;

    public DocumentProcessingJob(IInvoiceService documentService, IEmailService emailService)
    {
        _documentService = documentService;
        _emailService = emailService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            // Query the database or document storage to retrieve the unprocessed documents
            var unprocessedInvoices = await _documentService.GetUnprocessedDocuments();

            foreach (var invoice in unprocessedInvoices)
            {
                // Update the status of the invoice to "Processing"
                await _documentService.UpdateDocumentStatus(invoice.Id, DocumentStatus.Processing);

                // Prepare the email notification
                var emailContent = $"The invoice {invoice.InvoiceId} containing {invoice.InvoiceLine.Count} items has been successfully processed";

                //Send the email notification using the email service or library
               await _emailService.SendEmail(invoice.InvoiceHeader.Email, "Invoice Processing Notification", emailContent);

                // Update the status of the invoice to "Processed"
                await _documentService.UpdateDocumentStatus(invoice.Id, DocumentStatus.Processed);
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during the job execution
            Log.Error(ex, "An error occurred during document processing job execution.");
        }

        await Task.CompletedTask;
    }
}
