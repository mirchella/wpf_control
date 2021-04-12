using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WeatherService
{
    public class ApplicationMyDB : DbContext
    {
        public DbSet<DB_my> weather_db { get; set; }
        public ApplicationMyDB() : base("DefaultConnection") { }
    }
}
