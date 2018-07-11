using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Empower.Mvc.Models;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Empower.Services;
using NHibernate;
using NHibernate.Criterion;

namespace Empower.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private IEmailService _emailService;
        private ISession _session;

        // This is a constructor acting as a recipe.
        // It contains all the ingredients that HomeController
        // needs to do its job.
        //
        public HomeController(
           IEmailService emailService,
           ISession session
        )
        {
            _emailService = emailService;
            _session = session;
        }

        public IActionResult Index()
        {
            //var actors = _session.CreateCriteria<Empower.NHibernate.Entities.Actor>()
            //    .Add(Restrictions.InsensitiveLike("LastName", "W", MatchMode.Start))
            //    .List<Empower.NHibernate.Entities.Actor>();
            var actors = (from a in _session.Query<Empower.NHibernate.Entities.Actor>()
                          where a.LastName.StartsWith("w")
                          select a).ToList();
            var vm = new ActorListViewModel
            {
                Actors = actors
            };
          
            // return View(vm);
            return View(vm);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            // New up a ContactViewModel
            var contact = new ContactViewModel();


            return View(contact);
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.CompletedAt = _emailService.SendContactEmail
                (
                    viewModel.Name,
                    viewModel.Email,
                    viewModel.Message
                );

                if (!viewModel.CompletedAt.HasValue)
                {
                    viewModel.ErrorMessage = "Oops.  We have a problem";
                }
            }
            return View(viewModel);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
