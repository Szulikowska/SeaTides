using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UKTidalAPISerwis;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace SeaTides.Models
{
    public class DatabaseViewModel
    {
        private readonly IConfiguration _config;
        private readonly TidalsDatabaseContext _context;
        private readonly string key;

        public DatabaseViewModel(IConfiguration configuration, TidalsDatabaseContext context)
        {
            _config = configuration;
            _context = context;
            key = _config.GetSection("API-Key").Value;
        }

        public async void AddData()
        {
            var req = new Requests();
            var stations = await req.GetStationsList(key);
            _context.AddRange(stations);
            _context.SaveChanges();
        }
    }
}
