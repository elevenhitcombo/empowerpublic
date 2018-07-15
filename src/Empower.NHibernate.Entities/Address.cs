using Empower.NHibernate.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.NHibernate.Entities
{
    public class Address : Entity
    {
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string District { get; set; }
        public virtual City City { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string Phone { get; set; }
        public virtual DateTime LastUpdate { get; set; }
        public virtual IList<Store> Stores { get; set; }
        public virtual IList<Customer> Customers { get; set; }
        
    }
}
