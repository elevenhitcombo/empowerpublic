using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.Domain.Client.Requests
{
    public class ActorSearchRequest : SearchRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
