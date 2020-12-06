using Microsoft.EntityFrameworkCore;
using System;
using DTO;
using Newtonsoft.Json;

namespace DAL
{
    public class TidalsDatabaseContext : DbContext
    {
        public TidalsDatabaseContext(DbContextOptions<TidalsDatabaseContext> context) : base(context)
        {
        }
        public DbSet<StationsList> Stations { get; set; }
    }
}
