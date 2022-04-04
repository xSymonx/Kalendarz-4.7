using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.Net;

namespace Kalendarz
{
    /// <summary>
    /// Logika interakcji dla klasy Weather_Window.xaml
    /// </summary>
    public partial class Weather_Window : Window
    {
        public string Basic_City { get; set; } = "Wroclaw";
        public Weather_Window()
        {
            InitializeComponent();
            Update_Weather();
        }

        public void Update_Weather()
        {
            using (WebClient web = new WebClient())
            {
                //string url = String.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid=35b19726b000ef946b6c90c401ad576e", Basic_City);
                string url_localization = String.Format("http://api.openweathermap.org/geo/1.0/direct?q={0}&limit=1&appid=35b19726b000ef946b6c90c401ad576e", Basic_City);
                string url_string = web.DownloadString(url_localization);
                string lat = "lat";
                string lon = "lon";

                int start = url_string.IndexOf(lat) + lat.Length + 2;
                int end = url_string.IndexOf(",", start);
                string lat_number = url_string.Substring(start, end - start);

                start = url_string.IndexOf(lon) + lon.Length + 2;
                end = url_string.IndexOf(",", start);
                string lon_number = url_string.Substring(start, end - start);

                //string url = String.Format("https://api.openweathermap.org/data/2.5/weather?lat=51.1089776&lon=17.0326689&appid=35b19726b000ef946b6c90c401ad576e&units=metric");
                string url = String.Format("https://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&appid=35b19726b000ef946b6c90c401ad576e&units=metric", lat_number, lon_number);
                var json = web.DownloadString(url);
                WeatherApi.WeatherInfo Info = JsonConvert.DeserializeObject<WeatherApi.WeatherInfo>(json);

                Description.Text = Info.weather[0].description;
                City.Text = Basic_City + ", " + Info.sys.country;

                Uri Image_Source = new Uri("https://openweathermap.org/img/w/" + Info.weather[0].icon + ".png", UriKind.RelativeOrAbsolute);
                Image.Source = new BitmapImage(Image_Source);


                Temperature.Text = Info.main.temp.ToString() + "°C";
                feels_like.Text = Info.main.feels_like.ToString() + "°C";
                temp_min.Text = Info.main.temp_min.ToString() + "°C";
                temp_max.Text = Info.main.temp_max.ToString() + "°C";

                pressure.Text = Info.main.pressure.ToString() + "hPa";
                humidity.Text = Info.main.humidity.ToString() + "%";

                wind_speed.Text = Info.wind.speed.ToString() + "m/s";

                //country.Text = Info.sys.country;

                DateTime dateTime_sunrise = UnixTimeStampToDateTime(Info.sys.sunrise);
                DateTime dateTime_sunset = UnixTimeStampToDateTime(Info.sys.sunset);

                sunrise.Text = dateTime_sunrise.ToString("HH:mm");
                sunset.Text = dateTime_sunset.ToString("HH:mm");





            }
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            Basic_City = User_Text.Text;
            Update_Weather();

        }
        private void Bar_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                this.DragMove();
                e.Handled = true;
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
