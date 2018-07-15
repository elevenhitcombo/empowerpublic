using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Empower.Domain.Client.Requests
{
    public class CategoryCreateRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
