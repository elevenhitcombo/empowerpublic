using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.NHibernate.Entities.Mappings
{
    public class PaymentMap : ClassMap<Payment>
    {
        public PaymentMap()
        {
            Table("payment");
            Id(x => x.Id).Column("payment_id");
            References(x => x.Customer)
                .Column("customer_id");
            References(x => x.Staff)
                .Column("staff_id");
            References(x => x.Rental)
                .Column("rental_id");
            Map(x => x.Amount)
                .Column("amount");
            Map(x => x.PaymentDate)
                .Column("payment_date");
            Map(x => x.LastUpdate)
                .Column("last_update");
        }
    }
}
