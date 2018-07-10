using Empower.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.Settings.Services
{
    public class EmailSettingsService : IEmailSettingsService
    {
        private const string ContactPrefix = "Contact";

        private readonly ISettingsService _settingsService;

        public EmailSettingsService(ISettingsService settingService)
        {
            _settingsService = settingService;
        }

        public string SmtpHost => _settingsService.GetStringValue($"{ContactPrefix}:SmtpHost");
        
        public string SmtpPassword => _settingsService.GetStringValue($"{ContactPrefix}:SmtpPassword");
        
        public string SmtpUsername => _settingsService.GetStringValue($"{ContactPrefix}:SmtpUsername");
        public int SmtpPort => Convert.ToInt32( _settingsService.GetStringValue($"{ContactPrefix}:SmtpPort"));

        public string ContactFromEmail => _settingsService.GetStringValue($"{ContactPrefix}:FromEmail");

        public string ContactFromName => _settingsService.GetStringValue($"{ContactPrefix}:FromName");

        public string ContactToEmail => _settingsService.GetStringValue($"{ContactPrefix}:ToEmail");
        public string ContactToName => _settingsService.GetStringValue($"{ContactPrefix}:ToEmail");
        public bool EnableSsl => true;
    }
}
