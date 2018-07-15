using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.NHibernate.Entities.Mappings
{
    public class CountryMap : ClassMap<Country>
    {
        public CountryMap()
        {
            Table("country");
            Id(x => x.Id).Column("country_id");
            Map(x => x.Name).Column("country");
            HasMany(x => x.Cities)
                .KeyColumn("country_id")
                .Inverse();
        }
    }
}
