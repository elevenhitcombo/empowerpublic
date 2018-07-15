using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.Domain.Client.Requests
{
    public class CategorySearchRequest : SearchRequest
    {
        public string Name { get; set; }
    }
}
