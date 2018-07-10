using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Empower.Mvc.Controllers
{
    public class NavController : Controller
    {
        public IActionResult TopMenu()
        {
            return View();
        }
    }
}