using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.Controllers
{
    public class NotUpisanController : Controller
    {
        public IActionResult NijeUpisan()
        {
            return View();
        }

        public IActionResult NijePrijavljen()
        {
            return View();
        }
    }
}