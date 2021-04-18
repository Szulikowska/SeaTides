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
using DTO;

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
        readonly IServiceScopeFactory _serviceScopeFactory;

        /// <summary>
        /// Class constructor, gets api key from config
        /// </summary>
        /// <param name="configuration">instance of IConfiguration</param>
        /// <param name="context">instance of TidalsDatabaseContext</param>
        public DatabaseViewModel(IConfiguration configuration, TidalsDatabaseContext context, IServiceScopeFactory scopeFactory)
        {
            _config = configuration;
            _context = context;
            _serviceScopeFactory = scopeFactory;
            key = _config.GetSection("API-Key").Value;
        }

        /// <summary>
        /// Adds data to database
        /// </summary>
        public async void AddData()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var databaseContext = scope.ServiceProvider.GetRequiredService<TidalsDatabaseContext>();
            var req = new Requests();
            var stations = await req.GetStationsList(key);
            databaseContext.Stations.AddRange(stations);
            databaseContext.SaveChanges();
        }

        /// <summary>
        /// Adds data do database, received from users
        /// </summary>
        public async void AddUserData(UsersStation newPortData)
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            var databaseContext = scope.ServiceProvider.GetRequiredService<TidalsDatabaseContext>();
            await databaseContext.UserData.AddRangeAsync(newPortData);
            databaseContext.SaveChanges();
        }

        /// <summary>
        /// Loads data from database
        /// </summary>
        /// <returns></returns>
        public List<DTO.StationsList> LoadData()
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            var databaseContext = scope.ServiceProvider.GetRequiredService<TidalsDatabaseContext>();
            var data = databaseContext.Stations.Include(x => x.Features).ThenInclude(sg => sg.Geometry).Include(x => x.Features).ThenInclude(se => se.Events).Include(x => x.Features).ThenInclude(sp => sp.Properties).ToList();
            foreach (var collection in data)
            {
                foreach(var feature in collection.Features)
                {
                    feature.Events.Sort((x, y) => DateTime.Compare(x.DateTime, y.DateTime));
                }

            }
            return data;
        }

        /// <summary>
        /// Loads data from database
        /// </summary>
        /// <returns></returns>
        public List<DTO.UsersStation> LoadUserData()
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            var databaseContext = scope.ServiceProvider.GetRequiredService<TidalsDatabaseContext>();
            var data = databaseContext.UserData.ToList();
            return data;
        }

        /// <summary>
        /// Loads data from database
        /// </summary>
        /// <returns></returns>
        public List<DTO.Events> LoadStationEventsById(string Id)
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            var databaseContext = scope.ServiceProvider.GetRequiredService<TidalsDatabaseContext>();
            //var data = databaseContext.Stations.Select(x => x.Features.Where(y => y.Properties.Id == Id)).ToList();
            var data = databaseContext.Stations.SelectMany(x => x.Features.Where(y=>y.Properties.Id == Id).Select(y=> y.Events)).ToList();
            foreach (var feature in data)
            {
                feature.Sort((x, y) => DateTime.Compare(x.DateTime, y.DateTime));
            }
            return data[0];
        }

        /// <summary>
        /// Deletes all data from database
        /// </summary>
        public void DeleteAllData()
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            var databaseContext = scope.ServiceProvider.GetRequiredService<TidalsDatabaseContext>();
            var data = databaseContext.Stations.Include(x => x.Features).ThenInclude(sg => sg.Geometry).Include(x => x.Features).ThenInclude(se => se.Events).Include(x => x.Features).ThenInclude(sp => sp.Properties).ToList();
            var station = data[0].Features;
            foreach (var item in station)
            {
                databaseContext.RemoveRange(item.Events);
                databaseContext.RemoveRange(item.Geometry);
                databaseContext.RemoveRange(item.Properties);
            }
            databaseContext.RemoveRange(station);
            databaseContext.Stations.RemoveRange(data);
            databaseContext.SaveChanges();
        }

        /// <summary>
        /// Updates data in database
        /// </summary>
        public void UpdateData()
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            var databaseContext = scope.ServiceProvider.GetRequiredService<TidalsDatabaseContext>();
            var data = databaseContext.Stations.Include(x => x.Features).ThenInclude(sg => sg.Geometry).Include(x => x.Features).ThenInclude(se => se.Events).Include(x => x.Features).ThenInclude(sp => sp.Properties).ToList();
            var station = data[0].Features;
            
            foreach (var item in station)
            {
                databaseContext.RemoveRange(item.Events);
                databaseContext.RemoveRange(item.Geometry);
                databaseContext.RemoveRange(item.Properties);
            }
            databaseContext.RemoveRange(station);
            databaseContext.Stations.RemoveRange(data);
            AddData();
            databaseContext.SaveChanges();
        }
    }
}
