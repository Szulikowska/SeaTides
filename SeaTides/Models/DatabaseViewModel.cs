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
        /// <summary>
        /// Context of the databse
        /// </summary>
        public readonly TidalsDatabaseContext _context;
        private readonly string key;

        /// <summary>
        /// Class constructor, gets api key from config
        /// </summary>
        /// <param name="configuration">instance of IConfiguration</param>
        /// <param name="context">instance of TidalsDatabaseContext</param>
        public DatabaseViewModel(IConfiguration configuration, TidalsDatabaseContext context)
        {
            _config = configuration;
            _context = context;
            key = _config.GetSection("API-Key").Value;
        }

        /// <summary>
        /// Adds data to database
        /// </summary>
        public async void AddData()
        {
            var req = new Requests();
            var stations = await req.GetStationsList(key);
            _context.AddRange(stations);
            _context.SaveChanges();
        }

        /// <summary>
        /// Loads data from database
        /// </summary>
        /// <returns></returns>
        public List<DTO.StationsList> LoadData()
        {
            var data = _context.Stations.Include(x => x.Features).ThenInclude(sg => sg.Geometry).Include(x => x.Features).ThenInclude(se => se.Events).Include(x => x.Features).ThenInclude(sp => sp.Properties).ToList();
            return data;
        }

        /// <summary>
        /// Deletes all data from database
        /// </summary>
        public void DeleteAllData()
        {
            var data = _context.Stations.Include(x => x.Features).ThenInclude(sg => sg.Geometry).Include(x => x.Features).ThenInclude(se => se.Events).Include(x => x.Features).ThenInclude(sp => sp.Properties).ToList();
            var station = data[0].Features;
            foreach (var item in station)
            {
                _context.RemoveRange(item.Events);
                _context.RemoveRange(item.Geometry);
                _context.RemoveRange(item.Properties);
            }
            _context.RemoveRange(station);
            _context.Stations.RemoveRange(data);
            _context.SaveChanges();
        }

        /// <summary>
        /// Updates data in database
        /// </summary>
        public void UpdateData()
        {
            var data = _context.Stations.Include(x => x.Features).ThenInclude(sg => sg.Geometry).Include(x => x.Features).ThenInclude(se => se.Events).Include(x => x.Features).ThenInclude(sp => sp.Properties).ToList();
            var station = data[0].Features;
            
            foreach (var item in station)
            {
                _context.RemoveRange(item.Events);
                _context.RemoveRange(item.Geometry);
                _context.RemoveRange(item.Properties);
            }
            _context.RemoveRange(station);
            _context.Stations.RemoveRange(data);
            AddData();
            _context.SaveChanges();
        }
    }
}
