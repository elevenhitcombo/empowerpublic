using Empower.NHibernate.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.NHibernate.Entities
{
    public class Language : Entity
    {
        public virtual string Name { get; set; }
        public virtual DateTime LastUpdate { get; set; }
        public virtual IList<Film> Films { get; set; }
        public virtual IList<Film> OriginalLanguageFilms { get; set; }
    }
}
