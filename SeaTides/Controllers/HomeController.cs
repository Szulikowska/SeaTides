using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SeaTides.Models;
using UKTidalAPISerwis;

namespace SeaTides.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private readonly string key;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            key = _config.GetSection("API-Key").Value;
        }

        public async Task<IActionResult> Index()
        {
            //var req = new Requests();

            //await req.GetStationsList(key);
            //await req.GetStation(key, "0001");
            //await req.GetTidalEvents(key, "0001", 6);
            return View();
        }

        /// <summary>
        /// Gets list of stations from databse and sent them as JSON
        /// </summary>
        /// <param name="database">Singleton of database</param>
        /// <returns>JSON with stations list</returns>
        public JsonResult GetStations([FromServices] DatabaseViewModel database)
        {
            return Json(database.LoadData());
        }

        public IActionResult Privacy()
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
