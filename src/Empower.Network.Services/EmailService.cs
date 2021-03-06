﻿using Empower.Domain.Client.Requests;
using Empower.Domain.Client.Responses;
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

        public SendEmailResponse SendContactEmail(string name, string email, string message)
        {
            return SendContactEmail(new SendEmailRequest
            {
                Subject = "Overload",
                Name = name,
                Email = email,
                Message = message
            });
        }

        public SendEmailResponse SendContactEmail(SendEmailRequest request)
        {
            var response = new SendEmailResponse();

            // Do something
            var message = new MailMessage();
            message.From =
                new MailAddress(
                   _emailSettingsService.ContactFromEmail,
                   _emailSettingsService.ContactToEmail);

            // Subject
            message.Subject = request.Subject ?? "New contact message";
            // To
            message.To.Add(new MailAddress(
               _emailSettingsService.ContactToEmail,
               _emailSettingsService.ContactToName
            ));

            // Message
            message.Body = $"New contact from {request.Name} ({request.Email}) " +
                Environment.NewLine +
                request.Message;

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
                response.CompletedAt = DateTime.UtcNow;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = 
                    $"Oops! We did it again! ({ex.Message})";


            }

            // SendEmailResponse
            return response;
        }
    }
}
