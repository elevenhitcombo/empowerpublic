using Empower.NHibernate.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.NHibernate.Entities
{
    public class Inventory : Entity
    {
        public virtual Film Film { get; set; }
        public virtual Store Store { get; set; }
        public virtual DateTime LastUpdate { get; set; }
        public virtual IList<Rental> Rentals { get; set; }
    }
}
