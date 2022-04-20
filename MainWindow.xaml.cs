using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.Net;
using System.Globalization;

namespace Kalendarz
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// ObservableCollection zawierające zadania
        /// </summary>
        public static ObservableCollection<Task> Notes { get; set; } = new ObservableCollection<Task>();

        /// <summary>
        /// ObservableCollection zawierające miesiące
        /// </summary>
        public static ObservableCollection<string> Months { get; set; } = new ObservableCollection<string>();

        /// <summary>
        /// Numer miesiąca
        /// </summary>
        private int month { get; set; }

        /// <summary>
        /// numer roku
        /// </summary>
        private int year { get; set; }

        /// <summary>
        /// Nazwa miasta dla którego pobierana jest aktualna pogoda
        /// </summary>
        public static string Basic_City { get; set; } = "Wroclaw";

        /// <summary>
        /// Statyczny numer miesiąca
        /// </summary>
        public static int static_month { get; set; }

        /// <summary>
        /// statyczny rok
        /// </summary>
        public static int static_year { get; set; }

        /// <summary>
        /// statyczna data
        /// </summary>
        public static string static_date { get; set; } 

        /// <summary>
        /// Konstruktor klasy MainWindow
        /// </summary>
        public MainWindow()
        {
            this.DataContext = this;
            InitializeComponent();
            Update_Weather();
            ShowTasks();
            displayDays();
        }

        /// <summary>
        /// Zamiana aktualnie wyświetlanego miesiąca na kolejny
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Right_Arrow_Click(object sender, RoutedEventArgs e)
        {
            month++;
            if (month > 12)
            {
                month = 1;
                year++;
            }
            static_month = month;
            static_year = year;
            string monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            Months.Clear();
            Months.Add(monthname + " " + year);
            MyWrapPanel.Children.Clear();

            // pobierz pierwszy dzień miesiąca
            DateTime startofthemonth = new DateTime(year, month, 1);
            // pobierz ilość dni w miesiącu
            int days = DateTime.DaysInMonth(year, month);
            // konwersja do int
            int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d"));
            // stworzenie blank usercontrol
            if (dayoftheweek == 0)
            {
                dayoftheweek = 7;
            }

            for (int j = 1; j < dayoftheweek; j++)
            {
                Blank blank = new Blank();
                MyWrapPanel.Children.Add(blank);
            }

            for (int j = 1; j <= days; j++)
            {
                Days ucDays2 = new Days();
                ucDays2.days(j);
                MyWrapPanel.Children.Add(ucDays2);
            }
        }

        /// <summary>
        /// Zamiana aktualnie wyświetlanego miesiąca na poprzedni
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Left_Arrow_Click(object sender, RoutedEventArgs e)
        {
            month--;
            if (month < 1)
            {
                month = 12;
                year--;
            }
            static_month = month;
            static_year = year;
            string monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            Months.Clear();
            Months.Add(monthname + " " + year);
            MyWrapPanel.Children.Clear();

            // pobierz pierwszy dzień miesiąca
            DateTime startofthemonth = new DateTime(year, month, 1);
            // pobierz ilość dni w miesiącu
            int days = DateTime.DaysInMonth(year, month);
            // konwersja do int
            int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d"));
            // stworzenie blank usercontrol
            if (dayoftheweek == 0)
            {
                dayoftheweek = 7;
            }

            for (int j = 1; j < dayoftheweek; j++)
            {
                Blank blank = new Blank();
                MyWrapPanel.Children.Add(blank);
            }

            for (int j = 1; j <= days; j++)
            {
                Days ucDays2 = new Days();
                ucDays2.days(j);
                MyWrapPanel.Children.Add(ucDays2);
            }
        }

        /// <summary>
        /// Uruchomienie okna add_Task_Window oraz aktualizacja zadań
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Task_Click(object sender, RoutedEventArgs e)
        {
            Add_Task_Window add_Task_Window = new Add_Task_Window();
            add_Task_Window.ShowDialog();
            ShowTasks();
        }

        /// <summary>
        /// Uruchomienie okna delete_Task_Window oraz aktualizacja zadań
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Task_Click(object sender, RoutedEventArgs e)
        {
            Delete_Task_Window delete_Task_Window = new Delete_Task_Window();
            Task temp = (Task)Notes_List.SelectedItem;
            if (temp != null)
            {
                delete_Task_Window.Task_Index = temp.ID;
                delete_Task_Window.ShowDialog();
                ShowTasks();
            }
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

                //Wczytanie informacji pogodowych
                Description.Text = Info.weather[0].description;
                City.Text = Basic_City;
                double Temperature_double = Math.Round(Info.main.temp, 1);
                Temperature.Text = Temperature_double.ToString() + "°C";

                Uri Image_Source = new Uri("https://openweathermap.org/img/w/" + Info.weather[0].icon + ".png", UriKind.RelativeOrAbsolute);
                Image.Source = new BitmapImage(Image_Source);
            }
        }

        /// <summary>
        /// Obsługa przycisku do aktualizacji pogody
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Refresh_Weather_Click(object sender, RoutedEventArgs e)
        {
            Update_Weather();
        }

        /// <summary>
        /// Obsługa przycisku do wyświetalania szczegółów pogody
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Details_Click(object sender, RoutedEventArgs e)
        {
            Weather_Window weather_Window = new Weather_Window();
            weather_Window.Show();
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
            this.Close();
        }

        /// <summary>
        /// Obsługa przycisku do wyświetalania wszystkich zadań z bazy danych
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TASKS_BUTTON_Click(object sender, RoutedEventArgs e)
        {
            ShowTasks();   
        }

        /// <summary>
        /// Wyświetlanie zadań z bazy danych
        /// </summary>
        public void ShowTasks()
        {
            Notes.Clear();
            using (var context = new TaskContext())
            {
                IQueryable<Task> query = context.Tasks;
                foreach (var item in query)
                {
                    string text = "• " + item.Data + " " + item.Nazwa + " ID:" + item.ID + "\n";
                    Notes.Add(item);
                }

            }
        }

        /// <summary>
        /// Wyświetlanie odpowiednich numerów dni w kalendarzu
        /// </summary>
        private void displayDays()
        {
            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;

            string monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            Months.Clear();
            Months.Add(monthname + " " + year);

            static_month = month;
            static_year = year;

            // pobierz pierwszy dzień miesiąca
            DateTime startofthemonth = new DateTime(year, month, 1);
            // pobierz ilość dni w miesiącu
            int days = DateTime.DaysInMonth(year, month);
            // konwersja do int
            int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d"));
            // stworzenie blank usercontrol
            if (dayoftheweek == 0)
            {
                dayoftheweek = 7;
            }

            for (int j = 1; j < dayoftheweek; j++)
            {
                Blank blank = new Blank();
                MyWrapPanel.Children.Add(blank);
            }

            for (int j = 1; j <= days; j++)
            {
                Days ucDays2 = new Days();
                ucDays2.days(j);
                MyWrapPanel.Children.Add(ucDays2);
            }
        }
    }
}
