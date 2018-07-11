using Empower.Domain.Client.Requests;
using Empower.Domain.Client.Responses;


namespace Empower.Services
{
    public interface IEmailService
    {
        SendEmailResponse SendContactEmail(SendEmailRequest request);
        SendEmailResponse SendContactEmail(string name, string email, string message);
    }
}
