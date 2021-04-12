using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherService
{
    public class DB_my
    {
        public int id {set; get;}
        private string city, timeInsert, temperature, feelLike;
        public string City
        {
            get { return city; }
            set { city = value; }
        }
        public string TimeInsert
        {
            get { return timeInsert; }
            set { timeInsert = value; }
        }
        public string FeelLike
        {
            get { return feelLike; }
            set { feelLike = value; }
        }
        public string Temperature
        {
            get { return temperature; }
            set { temperature = value; }
        }
        public DB_my() { }
        public DB_my(string city, string temperature, string feellike, string timeInsert) {
            this.city = city;
            this.temperature = temperature;
            this.feelLike = feellike;
            this.timeInsert = timeInsert;

        }
        
    }
}
