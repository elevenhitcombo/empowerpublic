using Empower.Domain.Client.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.Domain.Client.Responses
{
    public class CategoryCreateResponse : BaseResponse
    {
        public Category Category { get; set; }
    }
}
