using Microsoft.EntityFrameworkCore;
using System;
using DTO;
using Newtonsoft.Json;

namespace DAL
{
    public class TidalsDatabaseContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public TidalsDatabaseContext(DbContextOptions<TidalsDatabaseContext> context) : base(context)
        {
        }
        /// <summary>
        /// DbSet to create database, depending of relations and classes in this list
        /// </summary>
        public DbSet<StationsList> Stations { get; set; }
    }
}
