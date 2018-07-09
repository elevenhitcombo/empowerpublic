using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Empower.Mvc.Models;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace Empower.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
           

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
                        _configuration["Contact:FromEmail"], 
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
