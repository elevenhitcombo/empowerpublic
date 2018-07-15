using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.NHibernate.Entities.Mappings
{
    public class AddressMap : ClassMap<Address>
    {
        public AddressMap()
        {
            Table("address");
            Id(x => x.Id).Column("address_id");
            Map(x => x.Address1).Column("address");
            Map(x => x.Address2).Column("address2");
            Map(x => x.District).Column("district");
            References(x => x.City)
                .Column("city_id");
            Map(x => x.PostalCode).Column("postal_code");
            Map(x => x.Phone).Column("phone");
            Map(x => x.LastUpdate).Column("last_update");
            HasMany(x => x.Stores)
                .KeyColumn("address_id")
                .Inverse();
            HasMany(x => x.Customers)
                .KeyColumn("address_id")
                .Inverse();
        }
    }
}
