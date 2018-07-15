using Empower.NHibernate.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.NHibernate.Entities
{
    public class Rental : Entity
    {
        public virtual DateTime RentalDate { get; set; }
        public virtual Inventory Inventory { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual DateTime? ReturnDate { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual DateTime LastUpdate { get; set; }
    }
}
