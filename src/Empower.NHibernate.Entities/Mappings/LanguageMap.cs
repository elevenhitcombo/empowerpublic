using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.NHibernate.Entities.Mappings
{
    public class LanguageMap : ClassMap<Language>
    {
        public LanguageMap()
        {
            Table("language");
            Id(x => x.Id).Column("language_id");
            Map(x => x.Name).Column("name");
            Map(x => x.LastUpdate).Column("last_update");
            HasMany(x => x.Films)
                .KeyColumn("language_id")
                .Inverse();

            HasMany(x => x.OriginalLanguageFilms)
               .KeyColumn("original_language_id")
               .Inverse();

        }
    }
}
