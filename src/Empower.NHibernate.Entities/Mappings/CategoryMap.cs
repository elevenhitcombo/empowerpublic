using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.NHibernate.Entities.Mappings
{
    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Table("category");
            Id(x => x.Id).Column("category_id");
            Map(x => x.Name).Column("name");
            Map(x => x.LastUpdate).Column("last_update");

            HasManyToMany(x => x.Films)
              .Table("film_category")
              .ChildKeyColumn("film_id")
              .ParentKeyColumn("category_id")
              .LazyLoad();
        }
    }
}
