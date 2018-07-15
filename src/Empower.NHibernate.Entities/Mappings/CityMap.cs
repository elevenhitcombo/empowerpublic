using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.NHibernate.Entities.Mappings
{
    public class CityMap : ClassMap<City>
    {
        public CityMap()
        {
            Table("city");
            Id(x => x.Id).Column("city_id");
            Map(x => x.Name).Column("city");
            References(x => x.Country)
                .Column("country_id");
            Map(x => x.LastUpdate).Column("last_update");
            HasMany(x => x.Addresses)
                .KeyColumn("city_id")
                .Inverse();

        }
    }
}
