using Empower.Domain.Client.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.Domain.Client.Responses
{
    public class ActorUpdateResponse : BaseResponse
    {
        public Actor Actor { get; set; }
    }
}
