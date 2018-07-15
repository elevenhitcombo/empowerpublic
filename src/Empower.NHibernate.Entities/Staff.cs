using Empower.NHibernate.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.NHibernate.Entities
{
    public class Staff : Entity
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual Address Address { get; set; }
        public virtual byte[] Picture { get; set; }
        public virtual string Email { get; set; }
        public virtual Store Store { get; set; }
        public virtual bool Active { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual DateTime LastUpdate { get; set; }
    }
}
