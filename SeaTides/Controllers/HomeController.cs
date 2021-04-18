using System;
using System.Diagnostics;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SeaTides.Models;

namespace SeaTides.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Gets list of stations from databse and sent them as JSON
        /// </summary>
        /// <param name="database">Singleton of database</param>
        /// <returns>JSON with stations list</returns>
        public JsonResult GetStations([FromServices] DatabaseViewModel database)
        {
            var data = database.LoadData();
            return Json(data);
        }

        public JsonResult GetUserStations([FromServices] DatabaseViewModel database)
        {
            return Json(database.LoadUserData());
        }

        [HttpPost]
        public JsonResult GetStationEventsById([FromServices] DatabaseViewModel database, string id)
        {
            return Json(database.LoadStationEventsById(id));
        }

        [HttpPost]
        public void SavePortData([FromServices] DatabaseViewModel database, string newPortData)
        {
            UsersStation station = JsonConvert.DeserializeObject<UsersStation>(newPortData);
            database.AddUserData(station);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}