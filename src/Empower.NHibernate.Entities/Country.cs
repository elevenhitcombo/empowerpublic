using Empower.NHibernate.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.NHibernate.Entities
{
    public class Country : Entity
    {
        public virtual string Name { get; set; }
        public virtual IList<City> Cities { get; set; }
        public virtual DateTime LastUpdate { get; set; }
    }
}
