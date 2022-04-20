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
        /// <summary>
        /// Nazwa miasta dla którego pobierana jest aktualna pogoda
        /// </summary>
        private string Basic_City { get; set; } = "Wroclaw";

        /// <summary>
        /// Kontruktor klasy Weather_Window
        /// </summary>
        public Weather_Window()
        {
            InitializeComponent();
            Update_Weather();
        }

        /// <summary>
        /// Metoda obsługująca API oraz aktualizująca informacje o pogodzie 
        /// </summary>
        public void Update_Weather()
        {
            using (WebClient web = new WebClient())
            {
                //Brak podania miasta przez użytkowania anuluje akutalizacje pogody
                if (Basic_City == "")
                   return;

                //Pobranie geolokalizacji podanego miasta
                string url_localization = String.Format("http://api.openweathermap.org/geo/1.0/direct?q={0}&limit=1&appid=35b19726b000ef946b6c90c401ad576e", Basic_City);
                string url_string = web.DownloadString(url_localization);
                string lat = "lat";
                string lon = "lon";
                int start = url_string.IndexOf(lat) + lat.Length + 2;

                //Podanie nieprawidłowej nazwy miasta anuluje akutalizacje pogody
                if (start == 4)
                  return;

                int end = url_string.IndexOf(",", start);
                string lat_number = url_string.Substring(start, end - start);

                start = url_string.IndexOf(lon) + lon.Length + 2;
                end = url_string.IndexOf(",", start);
                string lon_number = url_string.Substring(start, end - start);

                //Pobranie informacji o pogodzie dla danych danych geolokalizacyjnych
                string url = String.Format("https://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&appid=35b19726b000ef946b6c90c401ad576e&units=metric", lat_number, lon_number);
                var json = web.DownloadString(url);
                WeatherApi.WeatherInfo Info = JsonConvert.DeserializeObject<WeatherApi.WeatherInfo>(json);

                Description.Text = Info.weather[0].description;
                City.Text = Basic_City + ", " + Info.sys.country;

                Uri Image_Source = new Uri("https://openweathermap.org/img/w/" + Info.weather[0].icon + ".png", UriKind.RelativeOrAbsolute);
                Image.Source = new BitmapImage(Image_Source);

                //Wczytanie informacji pogodowych
                Temperature.Text = Info.main.temp.ToString() + "°C";
                feels_like.Text = Info.main.feels_like.ToString() + "°C";
                temp_min.Text = Info.main.temp_min.ToString() + "°C";
                temp_max.Text = Info.main.temp_max.ToString() + "°C";

                pressure.Text = Info.main.pressure.ToString() + "hPa";
                humidity.Text = Info.main.humidity.ToString() + "%";

                wind_speed.Text = Info.wind.speed.ToString() + "m/s";

                DateTime dateTime_sunrise = UnixTimeStampToDateTime(Info.sys.sunrise);
                DateTime dateTime_sunset = UnixTimeStampToDateTime(Info.sys.sunset);

                sunrise.Text = dateTime_sunrise.ToString("HH:mm");
                sunset.Text = dateTime_sunset.ToString("HH:mm");
            }
        }

        /// <summary>
        /// Konwersja czasu unixowego do czasu datowego
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        /// <summary>
        /// Aktualizacja danych pogodowych dla podanego miasta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            Basic_City = User_Text.Text;
            Update_Weather();
        }

        /// <summary>
        /// Zmiana pozycji okna na ekranie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bar_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                this.DragMove();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Zamknięcie okna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Basic_City = Basic_City;
            this.Close();
        }
    }
}
