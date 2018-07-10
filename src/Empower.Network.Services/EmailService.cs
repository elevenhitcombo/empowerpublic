using Empower.Services;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Empower.Network.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailSettingsService _emailSettingsService;

        public EmailService(IEmailSettingsService emailSettingsService)
        {
            // Great!  We have a lovely email service.
            // We need to put it somewhere so that other methods
            // in the class can use the injected service
            _emailSettingsService = emailSettingsService;
        }

        public DateTime? SendContactEmail(string name, string email, string msg)
        {
            DateTime? completedAt = null;

            // Do something
            var message = new MailMessage();
            message.From =
                new MailAddress(
                   _emailSettingsService.ContactFromEmail,
                   _emailSettingsService.ContactToEmail);

            // Subject
            message.Subject = "New contact message";
            // To
            message.To.Add(new MailAddress(
               _emailSettingsService.ContactToEmail,
               _emailSettingsService.ContactToName
            ));

            // Message
            message.Body = $"New contact from {name} ({email}) " +
                Environment.NewLine +
                msg;

            // Set up a new SmtpClient
            var mailClient = new SmtpClient(
                _emailSettingsService.SmtpHost,
                _emailSettingsService.SmtpPort);

            mailClient.UseDefaultCredentials = false;

            mailClient.Credentials = new System.Net.NetworkCredential(
              _emailSettingsService.SmtpUsername,
              _emailSettingsService.SmtpPassword
            );

            mailClient.EnableSsl = _emailSettingsService.EnableSsl;

            try
            {
                mailClient.Send(message);
                completedAt = DateTime.UtcNow;
            }
            catch (Exception ex)
            {
                
            }

            return completedAt;
        }
    }
}
