using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using E_Learning.Models;

namespace E_Learning.Controllers
{
    public class NalogController : Controller
    {
        private readonly ILogger<NalogController> _logger;

        public NalogController(ILogger<NalogController> logger)
        {
            _logger = logger;
        }

        public IActionResult Prijava()
        {
            return View();
        }

        public IActionResult Registracija()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
