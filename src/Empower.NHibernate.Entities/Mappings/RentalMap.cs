using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.NHibernate.Entities.Mappings
{
    public class RentalMap : ClassMap<Rental>
    {
        public RentalMap()
        {
            Table("rental");
            Id(x => x.Id).Column("rental_id");
            Map(x => x.RentalDate).Column("rental_date");
            Map(x => x.ReturnDate).Column("return_date");
            References(x => x.Customer)
                .Column("customer_id");
            References(x => x.Inventory)
                .Column("inventory_id");
            References(x => x.Staff)
                .Column("staff_id");
            Map(x => x.LastUpdate)
                .Column("last_update");
        }
    }
}
