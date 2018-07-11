using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.Domain.Client.Responses
{
    public abstract class BaseResponse
    {
        public DateTime? CompletedAt { get; set; }
        public string ErrorMessage { get; set; }
        public bool HasErrored
            => !string.IsNullOrWhiteSpace(ErrorMessage);
    }
}
