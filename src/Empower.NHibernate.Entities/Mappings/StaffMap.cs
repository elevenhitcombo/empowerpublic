using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.NHibernate.Entities.Mappings
{
    public class StaffMap : ClassMap<Staff>
    {
        public StaffMap()
        {
            Table("staff");
            Id(x => x.Id)
                .Column("staff_id");
            Map(x => x.FirstName)
                .Column("first_name");
            Map(x => x.LastName)
                .Column("last_name");
            References(x => x.Address)
                .Column("address_id");
            Map(x => x.Picture)
                .Column("picture")
                .Length(Int32.MaxValue);
            Map(x => x.Email)
                .Column("email");
            References(x => x.Store)
                .Column("store_id");
            Map(x => x.Active)
                .Column("active");
            Map(x => x.Username)
                .Column("username");
            Map(x => x.Password)
                .Column("password");
            Map(x => x.LastUpdate)
                .Column("last_update");
        }
    }
}
