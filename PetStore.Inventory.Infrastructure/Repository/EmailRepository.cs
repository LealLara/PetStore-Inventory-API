using PetStore.Inventory.Domain.Entities;
using PetStore.Inventory.Domain.Utils.Constants;
using PetStore.Inventory.Domain.Utils.Enums;
using PetStore.Inventory.Domain.Utils.StringTools;
using System.Net;
using System.Net.Mail;

namespace PetStore.Inventory.Infrastructure.Repository
{
    public class EmailRepository : IEmailRepository
    {
        public async Task<bool> SendAsync(EmailEntity body)
        {
            try
            {
                string emailSender = EEmailSender.PetStore.GetDescription();
                string password = EmailSenderPassword.PetStorePassword;

                SmtpClient smtp = new(EmailConfiguration.SmtpConfigurationHost, EmailConfiguration.SmtpConfigurationPort)
                {
                    Credentials = new NetworkCredential(
                       emailSender,
                       password
                    ),
                    EnableSsl = true
                };

                string title = EAppTitle.PetStore.GetDescription();

                var mail = new MailMessage
                {
                    From = new MailAddress(emailSender, title),
                    Subject = body.Header,
                    Body = body.EmailBody,
                    IsBodyHtml = true
                };

                mail.To.Add(body.EmailAddress);
                await smtp.SendMailAsync(mail);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao enviar Email {ex.Message}");
            }
        }
    }
}