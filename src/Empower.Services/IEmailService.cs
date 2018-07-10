using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.Services
{
    public interface IEmailService
    {
        DateTime? SendContactEmail(string name, string email, string message);
    }
}
