using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.Domain.Client.Models
{
    public class Actor
    { 
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime LastUpdate { get; set; }
        public IList<Film> Films { get; set; }
    }
}
