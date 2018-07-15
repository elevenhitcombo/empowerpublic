using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.NHibernate.Entities.Mappings
{
    public class FilmMap : ClassMap<Film>
    {
        public FilmMap()
        {
            Table("film");
            Id(x => x.Id).Column("film_id");
            Map(x => x.Title).Column("title");
            Map(x => x.Description).Column("description");
            Map(x => x.ReleaseYear).Column("release_year");
            Map(x => x.RentalDuration).Column("rental_duration");
            Map(x => x.RentalRate).Column("rental_rate");
            Map(x => x.Length).Column("length");
            Map(x => x.ReplacementCost).Column("replacement_cost");
            Map(x => x.Rating).Column("rating");
            Map(x => x.SpecialFeatures).Column("special_features");
            Map(x => x.LastUpdate).Column("last_update");

            References(x => x.Language)
                .Column("language_id");

            References(x => x.OriginalLanguage)
                .Column("original_language_id");

            HasManyToMany(x => x.Actors)
               .Table("film_actor")
               .ChildKeyColumn("actor_id")
               .ParentKeyColumn("film_id")
               .LazyLoad();

            HasManyToMany(x => x.Categories)
              .Table("film_category")
              .ChildKeyColumn("category_id")
              .ParentKeyColumn("film_id")
              .LazyLoad();

            HasMany(x => x.Inventories)
                .Table("inventory")
                .Inverse();
        }
    }
}
