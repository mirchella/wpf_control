using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeatherService;

namespace newWeatherService
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GetWeatheService _weatherService;

        ApplicationMyDB db;

        public MainWindow()
        {
            InitializeComponent();

            db = new ApplicationMyDB();

            List<DB_my> weatherList = db.weather_db.ToList();
            string str = "";
            foreach(DB_my weather in weatherList)
            {
                str += "City: " + weather.City + "; Temp: " + weather.Temperature + "\n";
            }

            logs.Text = str;

            _weatherService = new GetWeatheService();
            //var weatherServer = new GetWeatheService();
            //weatherServer.GetWeather("Almaty");
        }

        private void getWeather(object sender, RoutedEventArgs e)
        {
            try
            {
                if(cityName.Text.Trim() == "")
                    throw new Exception();
                errorMess.Text = "";
                SendRequest();

            }
            catch (Exception)
            {
                errorMess.Text = "Enter City!";
            }
            
        }

        private void saveWeather(object sender, RoutedEventArgs e)
        {
            saveMess.Text = "";
            DB_my weatherDB = new DB_my(cityValue.Text, temperatureValue.Text, feelLikeValue.Text, timeValue.Text);
            db.weather_db.Add(weatherDB);
            db.SaveChangesAsync();
            saveMess.Text = "Datas save in DB";
        }

        private void GetLogWeather(object sender, RoutedEventArgs e)
        {
            Root tempInfo = _weatherService.GetWeather(cityName.Text);
            double tempTemperature = tempInfo.main.temp - 273.15;
            double tempFeelLike = tempInfo.main.feels_like - 273.15;
            tempTemperature = Math.Ceiling(tempTemperature);
            tempFeelLike = Math.Ceiling(tempFeelLike);
            DateTime tempTime = new DateTime().AddSeconds(1617715306).AddYears(1969);


            cityValue.Text = _weatherService.GetWeather(cityName.Text).name;
            temperatureValue.Text = tempTemperature.ToString();
            feelLikeValue.Text = tempFeelLike.ToString();
            tempTime.ToShortDateString();
            tempTime.ToShortTimeString();
            timeValue.Text = tempTime.ToShortTimeString() + " " + tempTime.ToShortDateString();
        }

        private async void SendRequest()
        {

            // Нужно ассинхронное выполнение некой задачи
            // 1 - Обращаемся к статичному методу Task.Run для асинхронного выполнения
            // Все исполнение происходит в method;
            // Некая долгая работа
            Root tempInfo = _weatherService.GetWeather(cityName.Text);
            

            Task.Run(() =>
            {
                double tempTemperature = tempInfo.main.temp - 273.15;
                double tempFeelLike = tempInfo.main.feels_like - 273.15;
                tempTemperature = Math.Ceiling(tempTemperature);
                tempFeelLike = Math.Ceiling(tempFeelLike);
                DateTime tempTime = new DateTime().AddSeconds(1617715306).AddYears(1969);
                tempTime.ToShortDateString();
                tempTime.ToShortTimeString();

              

                // Присваиваем результат выполнения долгой работы элементам UI
                Dispatcher.Invoke(() =>
                {
                    cityValue.Text = _weatherService.GetWeather(cityName.Text).name;
                    temperatureValue.Text = tempTemperature.ToString();
                    feelLikeValue.Text = tempFeelLike.ToString();
                    timeValue.Text = tempTime.ToShortTimeString() + " " + tempTime.ToShortDateString();
                });
            });
        }

    }

}
