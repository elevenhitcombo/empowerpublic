using Empower.NHibernate.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.NHibernate.Entities
{
    public class Store : Entity
    {
        public virtual Staff Manager { get; set; }
        public virtual Address Address { get; set; }
        public virtual DateTime? LastUpdate { get; set; }
        public virtual IList<Staff> Staff { get; set; }
        public virtual IList<Inventory> Inventory { get; set; }

    }
}
