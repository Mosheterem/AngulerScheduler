using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace webAppAnguler.Controllers
{
    public class BCardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    }
}