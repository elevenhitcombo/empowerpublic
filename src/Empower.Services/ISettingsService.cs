using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.Services
{
    public interface ISettingsService
    {
        string GetStringValue(string key);
        int GetIntValue(string key);
        object GetValue(string key);
    }
}
