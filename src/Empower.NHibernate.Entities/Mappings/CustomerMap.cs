using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.NHibernate.Entities.Mappings
{
    public class CustomerMap : ClassMap<Customer>
    {
        public CustomerMap()
        {
            Table("customer");
            Id(x => x.Id).Column("customer_id");
            References(x => x.Store)
                .Column("store_id");
            Map(x => x.FirstName)
                .Column("first_name");
            Map(x => x.LastName)
                .Column("last_name");
            Map(x => x.Email)
                .Column("email");
            References(x => x.Address)
                .Column("address_id");
            Map(x => x.Active)
                .Column("active");
            Map(x => x.CreateDate)
                .Column("create_date");
            Map(x => x.LastUpdate)
                .Column("last_update");
            HasMany(x => x.Rentals)
                .KeyColumn("customer_id")
                .Inverse();
            HasMany(x => x.Payments)
                .KeyColumn("customer_id")
                .Inverse();
        }
    }
}
