using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.Domain.Client.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastUpdate { get; set; }
        public IList<Film> Films { get; set; }
    }
}
