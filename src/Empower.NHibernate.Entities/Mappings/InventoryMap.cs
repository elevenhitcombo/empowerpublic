using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.NHibernate.Entities.Mappings
{
    public class InventoryMap : ClassMap<Inventory>
    {
        public InventoryMap()
        {
            Table("inventory");
            Id(x => x.Id)
                .Column("inventory_id");
            References(x => x.Film)
                .Column("film_id");
            References(x => x.Store)
                .Column("store_id");
            Map(x => x.LastUpdate)
                .Column("last_update");
            HasMany(x => x.Rentals)
                .KeyColumn("inventory_id")
                .Inverse();
        }
    }
}
