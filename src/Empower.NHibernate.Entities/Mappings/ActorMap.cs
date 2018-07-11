using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.NHibernate.Entities.Mappings
{
    public class ActorMap : ClassMap<Actor>
    {
        public ActorMap()
        {
            Table("actor");
            Id(x => x.Id).Column("actor_id");
            Map(x => x.FirstName).Column("first_name");
            Map(x => x.LastName).Column("last_name");
            Map(x => x.LastUpdate).Column("last_update");
            HasManyToMany(x => x.Films)
               .Table("film_actor")
               .ChildKeyColumn("film_id")
               .ParentKeyColumn("actor_id")
               .LazyLoad();
        }
    }
}
