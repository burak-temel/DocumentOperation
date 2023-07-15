using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DocumentOperation.Email.Models
{
    public class EmailMessage
    {
        [Required]
        public string Sender { get; set; }

        [Required]
        public string Recipient { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        public List<string> Attachments { get; set; }

        // Additional properties can be added based on your requirements

        public void SendEmail()
        {
            // Implementation for sending the email
            // Use your preferred email service or library to send the email
        }
    }
}
