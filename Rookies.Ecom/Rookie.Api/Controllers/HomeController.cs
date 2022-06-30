using Microsoft.AspNetCore.Mvc;
using Rookie.Api.Models;
using System.Diagnostics;

namespace Rookie.Api.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return Ok();
        }

    }
}