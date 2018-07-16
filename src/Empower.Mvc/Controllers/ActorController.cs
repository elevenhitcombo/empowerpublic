using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Empower.Services;
using Empower.Domain.Client.Responses;
using Empower.Domain.Client.Requests;
using Empower.Domain.Client.Models;

namespace Empower.Mvc.Controllers
{
    [Produces("application/json")]
    [Route("api/Actor")]
    public class ActorController : Controller
    {
        private readonly IActorService _actorService;

        public ActorController(IActorService actorService)
        {
            _actorService = actorService;
        }

        [HttpGet]
        [Route("list/{page?}")]
        public ActorSearchResponse List(int page = 1)
        {
            return _actorService.Search(new ActorSearchRequest { PageNumber = page });
        }

        [HttpPost]
        [Route("create")]
        public ActorCreateResponse Create(ActorCreateRequest request)
        {
            ActorCreateResponse response;

            if (ModelState.IsValid)
            {
                response = _actorService.Create(request);
            }
            else
            {
                response = new ActorCreateResponse() { ErrorMessage = "Model state not valid" };
            }

            return response;
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public ActorDeleteResponse Delete(int id)
        {
            return _actorService.Delete(new ActorDeleteRequest() { Id = id });
        }

        [HttpGet]
        [Route("get/{id}")]
        public Actor Get(int id)
        {
            return _actorService.Get(id);
        }

        [HttpPut]
        [Route("update/{id}/{firstname}/{lastname}")]
        public ActorUpdateResponse Update(int id, string firstname, string lastname)
        {
            if (ModelState.IsValid)
            {
                return _actorService.Update(new ActorUpdateRequest()
                {
                    Id = id,
                    FirstName = firstname,
                    LastName = firstname
                });

            }
            else
            {
                return new ActorUpdateResponse() { ErrorMessage = "Model doesn't contain enough information" };
            }
        }
    }
}