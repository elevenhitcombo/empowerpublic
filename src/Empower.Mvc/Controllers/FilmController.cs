using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Empower.Services;
using Empower.Domain.Client.Responses;
using Empower.Domain.Client.Models;
using Empower.Domain.Client.Requests;

namespace Empower.Mvc.Controllers
{
    [Produces("application/json")]
    [Route("api/film")]
    public class FilmController : Controller
    {
        private IFilmService _filmService;

        public FilmController(IFilmService filmService)
        {
            _filmService = filmService;
        }

        [HttpGet]
        [Route("list/{page?}")]
        public FilmSearchResponse List(int page = 1)
        {
            var vm = _filmService.Search(new Domain.Client.Requests.FilmSearchRequest
            {
                PageNumber = page
            });

            return vm;
        }

        

        [HttpGet]
        [Route("get/{id}")]
        public Film Get(int id)
        {
            return _filmService.Get(id);
        }

        [HttpPost]
        [Route("create")]
        public FilmCreateResponse Create(FilmCreateRequest request)
        {
            return (ModelState.IsValid) ? _filmService.Add(request) :
                new FilmCreateResponse() { ErrorMessage = "Please correct invalid data" };
        }
    }
}