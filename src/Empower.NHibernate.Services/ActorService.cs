using System;
using System.Collections.Generic;
using System.Text;
using nh = NHibernate;
using en = Empower.NHibernate.Entities;
using Empower.NHibernate.Interfaces;
using Empower.Domain.Client.Models;
using Empower.Services;
using Empower.Domain.Client.Requests;
using Empower.Domain.Client.Responses;
using System.Linq;
using NHibernate.Transform;

namespace Empower.NHibernate.Services
{
    public class ActorService : IActorService
    {
        private readonly nh.ISession _session;
        private readonly IRepository<en.Actor> _actorRepository;

        public ActorService(
            nh.ISession session,
            IRepository<en.Actor> actorRepository
        )
        {
            _session = session;
            _actorRepository = actorRepository;
        }

        public ActorCreateResponse Create(ActorCreateRequest request)
        {
            var response = new ActorCreateResponse();

            var repoActor = new en.Actor
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                LastUpdate = DateTime.Now
            };

            _session.Save(repoActor);

            response.Actor = new Actor()
            {
                Id = repoActor.Id,
                FirstName = repoActor.FirstName,
                LastName = repoActor.LastName,
                LastUpdate = repoActor.LastUpdate
            };

            return response;
        }

        public ActorDeleteResponse Delete(ActorDeleteRequest request)
        {
            var response = new ActorDeleteResponse();
            var repoActor = _actorRepository.Get(request.Id);

            try
            {
                _actorRepository.Delete(repoActor);
                response.CompletedAt = DateTime.UtcNow;
                response.RowCount = _actorRepository.GetQueryOver().RowCount();
            }
            catch(Exception ex)
            {
                response.ErrorMessage = $"Row couldn't be deleted {ex.Message}";
            }

            return response;
        }

        public Actor Get(int id)
        {
            var actor = (Actor)null;
            var actorAlias = (en.Actor)null;
            var filmAlias = (en.Film)null;
            

            var result = (Film)null;

            var repoActor = _actorRepository.Get(id);

            if (repoActor != null)
            {
                actor = new Actor
                {
                    FirstName = repoActor.FirstName,
                    LastName = repoActor.LastName,
                    Films = new List<Film>(),
                    LastUpdate = repoActor.LastUpdate,
                    Id = repoActor.Id
                };

                actor.Films = 
                    _session.QueryOver<en.Actor>(() => actorAlias)
                        .JoinAlias(ac => ac.Films, () => filmAlias)
                        .Where(c => c.Id == id)
                        .SelectList(list => list
                            .Select(() => filmAlias.Id).WithAlias(() => result.Id)
                            .Select(() => filmAlias.Title).WithAlias(() => result.Title)
                            .Select(() => filmAlias.ReleaseYear).WithAlias(() => result.ReleaseYear)
                            .Select(() => filmAlias.RentalDuration).WithAlias(() => result.RentalDuration)
                            .Select(() => filmAlias.Length).WithAlias(() => result.Length)
                            .Select(() => filmAlias.RentalRate).WithAlias(() => result.RentalRate)
                            .Select(() => filmAlias.Rating).WithAlias(() => result.Rating)
                            .Select(() => filmAlias.Description).WithAlias(() => result.Description)
                            .Select(() => filmAlias.LastUpdate).WithAlias(() => result.LastUpdate)
                            .Select(() => filmAlias.ReleaseYear).WithAlias(() => result.ReleaseYear)
                            .Select(() => filmAlias.ReplacementCost).WithAlias(() => result.ReplacementCost)
                            .Select(() => filmAlias.SpecialFeatures).WithAlias(() => result.SpecialFeatures)
                        )
                        .TransformUsing(Transformers.AliasToBean<Film>())
                        .List<Film>();


            }

            return actor;
        }

        public ActorSearchResponse Search(ActorSearchRequest request)
        {
            var response = new ActorSearchResponse();
            en.Actor actorAlias = null;
            Actor result = null;

            var queryOver =
                _session.QueryOver<en.Actor>(() => actorAlias);

            if (!string.IsNullOrWhiteSpace(request.FirstName))
            {
                queryOver.WhereRestrictionOn(c => c.FirstName).IsInsensitiveLike(request.FirstName.Trim(),
                    nh.Criterion.MatchMode.Anywhere);
            }

            if (!string.IsNullOrWhiteSpace(request.LastName))
            {
                queryOver.WhereRestrictionOn(c => c.LastName).IsInsensitiveLike(request.LastName.Trim(),
                    nh.Criterion.MatchMode.Anywhere);
            }

            response.Items =
                queryOver
                    .SelectList(list => list
                        .Select(() => actorAlias.Id).WithAlias(() => result.Id)
                        .Select(() => actorAlias.FirstName).WithAlias(() => result.FirstName)
                        .Select(() => actorAlias.LastName).WithAlias(() => result.LastName)
                    )
                    .TransformUsing(Transformers.AliasToBean<Actor>())
                    .Skip((request.PageNumber - 1) * request.ItemsPerPage)
                    .Take(request.ItemsPerPage)
                    .List<Actor>();

            return response;
        }

        public ActorUpdateResponse Update(ActorUpdateRequest request)
        {
            var response = new ActorUpdateResponse();
            var repoActor = _session.Get<en.Actor>(request.Id);

            repoActor.FirstName = request.FirstName;
            repoActor.LastName = request.LastName;
            repoActor.LastUpdate = DateTime.UtcNow;
    
            try
            {
                _actorRepository.Update(repoActor);
                response.CompletedAt = DateTime.UtcNow;
            }
            catch(Exception ex)
            {
                response.ErrorMessage = $"Could not update Actor {ex.Message}";
            }

            return response;
        }
    }
}
