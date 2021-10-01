using Ectotec.Entities.Common.Mailer;
using Ectotec.Entities.Contact;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace EctoTec.Framework.Common.Mailer
{
    public class MailerManager : IMailerManager<ContactModel>
    {
        private readonly MailConfigurationModel mailConfiguration = null;
        private readonly ILogger<MailerManager> _logger;
        public MailerManager(IOptions<MailConfigurationModel> config, ILogger<MailerManager> logger)
        {
            _logger = logger;
            mailConfiguration = config.Value;
        }
        public bool SendMail(ContactModel item)
        {            
            return Build(item);
        }

        private bool Build(ContactModel item)
        {
            bool response = false;
            try
            {
                MailMessage email = new MailMessage();
                email.From = new MailAddress(mailConfiguration.EmailManager);
                email.To.Add(item.Email);
                email.Subject = mailConfiguration.EmailSubject;
                email.IsBodyHtml = true;
                email.Body = string.Format(File.ReadAllText(Environment.CurrentDirectory +  mailConfiguration.BodyFile, Encoding.UTF8), item.FullName, item.Email, item.Country, item.ContactDate.ToString("dd-MMM-yyyy"));
                using (SmtpClient smtp = new SmtpClient(mailConfiguration.SMTPHost))
                {
                    smtp.Port = mailConfiguration.SMTPPort;
                    smtp.UseDefaultCredentials = false;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(mailConfiguration.EmailManager, mailConfiguration.PasswordManager);
                    smtp.EnableSsl = mailConfiguration.EnabledSSL;
                    email.Priority = MailPriority.Normal;
                    email.BodyEncoding = Encoding.UTF8;
                    smtp.Send(email);
                }
                response = true;
            }
            catch (Exception error)
            {
                _logger.LogError(error, error.Message, error.Data);
                response = false;
            }
            return response;
        }
    }
}
