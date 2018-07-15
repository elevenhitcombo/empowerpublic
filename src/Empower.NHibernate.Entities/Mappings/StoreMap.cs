using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.NHibernate.Entities.Mappings
{
    public class StoreMap : ClassMap<Store>
    {
        public StoreMap()
        {
            Table("store");
            Id(x => x.Id).Column("store_id");
            References(x => x.Manager)
                .Column("manager_staff_id");
            References(x => x.Address)
                .Column("address_id");
            Map(x => x.LastUpdate)
                .Column("last_update");
            HasMany(x => x.Staff)
                .KeyColumn("staff_id")
                .Inverse();
        }
    }
}
