using Empower.Domain.Client.Requests;
using Empower.Domain.Client.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.Services
{
    public interface IEmailService
    {
        SendEmailResponse SendContactEmail(SendEmailRequest request);
    }
}
