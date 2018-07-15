using Empower.NHibernate.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.NHibernate.Entities
{
    public class City : Entity
    {
        public virtual string Name { get; set; }
        public virtual Country Country { get; set; }
        public virtual DateTime LastUpdate { get; set; }
        public virtual IList<Address> Addresses { get; set; }
    }
}
