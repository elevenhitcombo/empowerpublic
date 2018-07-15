using Empower.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Empower.Domain.Client.Requests;
using Empower.Domain.Client.Responses;
using en = Empower.NHibernate.Entities;
using Empower.NHibernate.Interfaces;
using nh = NHibernate;
using Empower.Domain.Client.Models;
using NHibernate.Transform;
using System.Linq;

namespace Empower.NHibernate.Services
{
    public class FilmService : IFilmService
    {
        private readonly IRepository<en.Film> _filmRepository;
        private readonly IRepository<en.Language> _languageRepository;
        private readonly nh.ISession _session;

        public FilmService(
            nh.ISession session,
            IRepository<en.Film> filmRepository,
            IRepository<en.Language> languageRepository)
        {
            _filmRepository = filmRepository;
            _languageRepository = languageRepository;
            _session = session;
        }

        public FilmCreateResponse Add(FilmCreateRequest request)
        {
            var response = new FilmCreateResponse();

            // Get language
            var lang = _languageRepository.Get(request.Film.LanguageId);
            var origLang = _languageRepository.Get(request.Film.OriginalLanguageId ?? 0);

            var film = new en.Film
            {
                Language = lang,
                OriginalLanguage = origLang,
                Title = request.Film.Title,
                Description = request.Film.Description,
                ReleaseYear = request.Film.ReleaseYear,
                RentalDuration = request.Film.RentalDuration,
                Length = request.Film.Length,
                RentalRate = request.Film.RentalRate,
                Rating = request.Film.Rating,
                SpecialFeatures = request.Film.SpecialFeatures,
                ReplacementCost = request.Film.ReplacementCost,
                LastUpdate = DateTime.Now
            };

            _session.Save(film);
            _session.Flush();
            response.Film = request.Film;
            response.Film.Id = film.Id;
            response.Film.LastUpdate = film.LastUpdate;

            return response;
        }

        public Film Get(int id)
        {
            var repoFilm = _filmRepository.Get(id);

            var film = (repoFilm != null) 
                ? new Film
                {
                    Id = repoFilm.Id,
                    Title = repoFilm.Title,
                    RentalRate = repoFilm.RentalRate,
                    RentalDuration = repoFilm.RentalDuration,
                    Length = repoFilm.Length,
                    Language = repoFilm.Language.Name,
                    OriginalLanguage = repoFilm.OriginalLanguage?.Name,
                    Rating = repoFilm.Rating,
                    Description = repoFilm.Description,
                    LastUpdate = repoFilm.LastUpdate,
                    ReleaseYear = repoFilm.ReleaseYear,
                    ReplacementCost = repoFilm.ReplacementCost,
                    SpecialFeatures = repoFilm.SpecialFeatures,
                    LanguageId = repoFilm.Language.Id,
                    OriginalLanguageId = repoFilm.OriginalLanguage?.Id
                }
                : (Film)null;

            PopulateFilmDetails(film);
            return film;
        }

        private void PopulateFilmDetails(Film film)
        {
            if (film?.Id == null)
                return;

            Actor actorResult = null;
            Category categoryResult = null;
            en.Actor actorAlias = null;
            en.Film filmAlias = null;
            en.Category categoryAlias = null;


            var actors = _session.QueryOver<en.Actor>(() => actorAlias)
                .JoinAlias(fi => fi.Films, () => filmAlias)
                .Where(fa => filmAlias.Id == film.Id)
                .SelectList
                (
                    list => list
                        .Select(() => actorAlias.Id).WithAlias(() => actorResult.Id)
                        .Select(() => actorAlias.FirstName).WithAlias(() => actorResult.FirstName)
                        .Select(() => actorAlias.LastName).WithAlias(() => actorResult.LastName)
                        .Select(() => actorAlias.LastUpdate).WithAlias(() => actorResult.LastUpdate)
                )
                .TransformUsing(Transformers.AliasToBean<Actor>())
                .Future<Actor>();

            var categories = _session.QueryOver<en.Category>(() => categoryAlias)
               .JoinAlias(fi => fi.Films, () => filmAlias)
               .Where(fa => filmAlias.Id == film.Id)
               .SelectList
               (
                   list => list
                       .Select(() => categoryAlias.Id).WithAlias(() => categoryResult.Id)
                       .Select(() => categoryAlias.Name).WithAlias(() => categoryResult.Name)
                       .Select(() => categoryAlias.LastUpdate).WithAlias(() => categoryResult.LastUpdate)
               )
               .TransformUsing(Transformers.AliasToBean<Category>())
               .List<Category>();

            film.Actors = actors.ToList();
            film.Categories = categories.ToList();

        }

        public FilmSearchResponse Search(FilmSearchRequest request)
        {
            var response = new FilmSearchResponse();

            Film result= null;
            en.Film filmAlias = null;
            en.Language languageAlias = null;
            en.Language originalLanguageAlias = null;

            var queryOver =
                _session.QueryOver<en.Film>(() => filmAlias)
                    .JoinAlias(la => la.Language, () => languageAlias)
                    .JoinAlias(la => la.OriginalLanguage,
                        () => originalLanguageAlias,
                        nh.SqlCommand.JoinType.LeftOuterJoin);

            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                queryOver.WhereRestrictionOn(c => c.Title)
                    .IsInsensitiveLike(request.Title, nh.Criterion.MatchMode.Anywhere);
            }

            var rowCountQuery =
                queryOver.Clone()
                .ToRowCountQuery()
                .FutureValue<int>();
            
            response.Items =
                queryOver
                    .SelectList(list => list
                        .Select(() => filmAlias.Id).WithAlias(() => result.Id)
                        .Select(() => filmAlias.Title).WithAlias(() => result.Title)
                        .Select(() => filmAlias.ReleaseYear).WithAlias(() => result.ReleaseYear)
                        .Select(() => filmAlias.RentalDuration).WithAlias(() => result.RentalDuration)
                        .Select(() => filmAlias.Length).WithAlias(() => result.Length)
                        .Select(() => languageAlias.Name).WithAlias(() => result.Language)
                        .Select(() => originalLanguageAlias.Name).WithAlias(() => result.OriginalLanguage)
                        .Select(() => filmAlias.RentalRate).WithAlias(() => result.RentalRate)
                        .Select(() => filmAlias.Rating).WithAlias(() => result.Rating)
                        .Select(() => filmAlias.Description).WithAlias(() => result.Description)
                        .Select(() => filmAlias.LastUpdate).WithAlias(() => result.LastUpdate)
                        .Select(() => filmAlias.ReleaseYear).WithAlias(() => result.ReleaseYear)
                        .Select(() => filmAlias.ReplacementCost).WithAlias(() => result.ReplacementCost)
                        .Select(() => filmAlias.SpecialFeatures).WithAlias(() => result.SpecialFeatures)
                    )
                    .TransformUsing(Transformers.AliasToBean<Film>())
                    .Skip((request.PageNumber - 1) * request.ItemsPerPage)
                    .Take(request.ItemsPerPage)
                    .List<Film>();

            response.TotalItems = rowCountQuery.Value;

            return response;
        }
    }
}
