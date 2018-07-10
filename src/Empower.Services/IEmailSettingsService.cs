using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.Services
{
    public interface IEmailSettingsService
    {
        // We do not want to *CHANGE* any of these elements
        // These are read only
        string SmtpHost { get;  }
        string SmtpPassword { get;  }
        string SmtpUsername { get;  }
        int    SmtpPort { get;  }
        string ContactFromEmail { get;  }
        string ContactFromName { get;  }
        string ContactToEmail { get;  }
        string ContactToName { get;  }
        bool EnableSsl { get;  }
    }
}
