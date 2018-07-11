using Empower.NHibernate.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.NHibernate.Entities
{
    public class Film : Entity
    {
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string ReleaseYear { get; set; }
        public virtual Language Language { get; set; }
        public virtual Language OriginalLanguage { get; set; }
        public virtual byte RentalDuration { get; set; }
        public virtual decimal RentalRate { get; set; }
        public virtual short? Length { get; set; }
        public virtual decimal ReplacementCost { get; set; }
        public virtual string Rating { get; set; }
        public virtual string SpecialFeatures { get; set; }
        public virtual DateTime LastUpdate { get; set; }
        public virtual IList<Actor> Actors { get; set; }
        
    }
}
