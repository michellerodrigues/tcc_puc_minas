using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DescarteService.Models;
using Microsoft.AspNetCore.Mvc;

namespace DescarteService.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Descrição sobre Aplicação";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Entre em Contato";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
