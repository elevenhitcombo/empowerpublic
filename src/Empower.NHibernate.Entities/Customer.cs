using Empower.NHibernate.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.NHibernate.Entities
{
    public class Customer : Entity
    {
        public virtual Store Store { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual Address Address { get; set; }
        public virtual bool Active { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime LastUpdate { get; set; }
        public virtual IList<Rental> Rentals { get; set; }
        public virtual IList<Payment> Payments { get; set; }
    }
}
