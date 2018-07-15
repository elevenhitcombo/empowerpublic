using Empower.NHibernate.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.NHibernate.Entities
{
    public class Payment : Entity
    {
        public virtual Customer Customer { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual Rental Rental { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual DateTime PaymentDate { get; set; }
        public virtual DateTime LastUpdate { get; set; }

    }
}
