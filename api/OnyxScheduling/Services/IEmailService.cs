using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
namespace OnyxScheduling.Services
{
    public interface IEmailService
    {
        void SendEmail(string to, string subject, string htmlContent);
        void SendEmailWithPdf(string to, string subject, string htmlContent, byte[] pdfContent);
    }

    public class EmailService : IEmailService
    {
        public void SendEmail(string to, string subject, string htmlContent)
        {
            // SMTP configuration
            var smtpClient = new SmtpClient("smtp-relay.brevo.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("7a1f5a001@smtp-brevo.com", "QZ8btA39TmaHUrC5"),
                EnableSsl = true,
            };

            // Email message configuration
            var mailMessage = new MailMessage
            {
                //From = new MailAddress("7a1f5a001@smtp-brevo.com", "Your Company Name"),
                From = new MailAddress("solution.onyx@gmail.com", "Onyx Solutions"),
                Subject = subject,
                Body = htmlContent,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(to);

            try
            {
                smtpClient.Send(mailMessage);
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught in SendEmail(): {0}", e.ToString());
            }
        }

        public void SendEmailWithPdf(string to, string subject, string htmlContent, byte[] pdfContent)
        {
            var apiInstance = new TransactionalEmailsApi();

            // Convert PDF byte array to Base64 string
            string base64Pdf = Convert.ToBase64String(pdfContent);

            // Create the email with the PDF attachment
            var sendSmtpEmail = new SendSmtpEmail(
                to: new List<SendSmtpEmailTo> { new SendSmtpEmailTo(to, "Recipient Name") },
                sender: new SendSmtpEmailSender("your-email@domain.com", "Your Name"),
                subject: subject,
                htmlContent: htmlContent,
                attachment: new List<SendSmtpEmailAttachment>
                {
                new SendSmtpEmailAttachment(
                    name: "invoice",
                    content: pdfContent
                )
                }
            );

            try
            {
                // Send the email with the attachment
                CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling TransactionalEmailsApi.SendTransacEmail: " + e.Message);
            }
        }
    }
}
