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

namespace Empower.Mvc.Controllers
{
    public class HomeController : Controller
    {
        // These are to allow HomeController to access
        // IConfiguration or ISettingsService functionality
        private readonly IConfiguration _configuration;
        private readonly ISettingsService _settingsService;

        // This is a constructor acting as a recipe.
        // It contains all the ingredients that HomeController
        // needs to do its job.
        //
        public HomeController(
            IConfiguration configuration,
            ISettingsService settingsService
        )
        {
            _configuration = configuration;
            _settingsService = settingsService;
        }

        public IActionResult Index()
        {
            List<Actor> actors = new List<Actor>
            {
                new Actor
                {
                    Firstname = "Meryl",
                    Lastname = "Streep",
                    Films = 20
                },
                new Actor
                {
                    Firstname = "Bobcat",
                    Lastname = "Goldthwaite",
                    Films = 2
                },
                new Actor
                {
                    Firstname = "Steve",
                    Lastname = "Guttenberg",
                    Films = 5
                }
            };

            var vm = new ActorListViewModel
            {
                Actors = actors
            };
            ViewData["actors"] = actors;
            // return View(vm);
            return View();
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
                // Do something
                var message = new MailMessage();
                message.From = 
                    new MailAddress(
                       _settingsService.GetStringValue("Contact:FromEmail"), 
                        _configuration["Contact:FromName"]);

                // Subject
                message.Subject = "New contact message";
                // To
                message.To.Add(new MailAddress(
                   _configuration["Contact:ToEmail"] ,
                   _configuration["Contact:ToName"]
                ));

                // Message
                message.Body = $"New contact from {viewModel.Name} ({viewModel.Email}) " +
                    Environment.NewLine +
                    viewModel.Message;
                // Set up a new SmtpClient
                var mailClient = new SmtpClient(
                    _configuration["Contact:SmtpHost"],
                    Convert.ToInt32(_configuration["Contact:SmtpPort"]));

                mailClient.UseDefaultCredentials = false;
              
                mailClient.Credentials = new System.Net.NetworkCredential(
                  _configuration["Contact:SmtpUsername"],
                  _configuration["Contact:SmtpPassword"]
                );

                mailClient.EnableSsl = true;

                try
                {
                    mailClient.Send(message);
                    viewModel.CompletedAt = DateTime.UtcNow;
                }
                catch(Exception ex)
                {
                    viewModel.ErrorMessage = "Oops.  Something went wrong";
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
