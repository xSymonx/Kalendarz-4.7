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
        public int Month_Index { get; set; }
        public static ObservableCollection<string> Notes { get; set; } = new ObservableCollection<string>();
        public static ObservableCollection<string> Months { get; set; } = new ObservableCollection<string>();
        int month, year;
        public string Basic_City { get; set; } = "Wroclaw";
        public static int static_month, static_year;
        public static string static_date;

        public MainWindow()
        {
            this.DataContext = this;


            //Notes.Add("• mycie zębów\n");
            //Notes.Add("• pisanie programu\n");
            //Notes.Add("• Obiad\n");

            //Months.Add("March 2022");
            //Months.Add("April 2022");
            //Months.Add("May 2022");
            //Months.Add("June 2022");
            //Months.Add("July 2022");
            //Months.Add("August 2022");
            //Months.Add("September 2022");

            InitializeComponent();

            Update_Weather();
            ShowTasks();
            displayDays();
        }

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


            // get first day of month.
            DateTime startofthemonth = new DateTime(year, month, 1);
            // get the count of the days of the month
            int days = DateTime.DaysInMonth(year, month);
            // convert the startofthemonth to int
            int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d"));
            // first create a blank usercontrol
            if (dayoftheweek == 0)
            {
                dayoftheweek = 7;
            }
            int i = (dayoftheweek - 1);
            int idx = 1;

            for (int j = 3; j < 8; j++)
            {
                for (int k = i; k < 7; k++)
                {
                    Days ucDays = new Days();
                    ucDays.days(idx);
                    Grid.SetColumn(ucDays, k);
                    Grid.SetRow(ucDays, j);
                    MyGrid.Children.Add(ucDays);
                    if (idx == days)
                        break;
                    idx++;
                }
                i = 0;
                if (idx == days)
                    break;
            }
        }

        private void Left_Arrow_Click(object sender, RoutedEventArgs e)
        {
           /* if (Month_Index > 0)
            {
                Month_Index--;
                Month.Text = Months[Month_Index];
            }*/
        }

        private void Add_Task_Click(object sender, RoutedEventArgs e)
        {
            Add_Task_Window add_Task_Window = new Add_Task_Window();
            add_Task_Window.ShowDialog();
            ShowTasks();

        }


        private void Delete_Task_Click(object sender, RoutedEventArgs e)
        {
            Delete_Task_Window delete_Task_Window = new Delete_Task_Window();
            delete_Task_Window.ShowDialog();
            ShowTasks();

            if (delete_Task_Window.Task_Index == 0) return;
            else
            {
                Notes.RemoveAt(delete_Task_Window.Task_Index - 1);
            }

        }

        public void Update_Weather()
        {
            using (WebClient web = new WebClient())
            {
                string url = String.Format("https://api.openweathermap.org/data/2.5/weather?lat=51.1089776&lon=17.0326689&appid=35b19726b000ef946b6c90c401ad576e&units=metric");
                var json = web.DownloadString(url);
                WeatherApi.WeatherInfo Info = JsonConvert.DeserializeObject<WeatherApi.WeatherInfo>(json);

                Description.Text = Info.weather[0].description;
                City.Text = Basic_City;
                double Temperature_double = Math.Round(Info.main.temp, 1);
                Temperature.Text = Temperature_double.ToString() + "°C";


                Uri Image_Source = new Uri("https://openweathermap.org/img/w/" + Info.weather[0].icon + ".png", UriKind.RelativeOrAbsolute);
                Image.Source = new BitmapImage(Image_Source);
            }
        }

        private void Refresh_Weather_Click(object sender, RoutedEventArgs e)
        {
            Update_Weather();
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            Weather_Window weather_Window = new Weather_Window();
            weather_Window.Show();
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
        public void ShowTasks()
        {
            //MainWindow.static_date = Days.static_day + "/" + MainWindow.static_month + "/" + MainWindow.static_year;
            Notes.Clear();
            using (var context = new TaskContext())
            {
                IQueryable<Task> query = context.Tasks; //.Where(T => T.Data == MainWindow.static_date);
                foreach (var item in query)
                {
                    string text = "• " + item.Data + " " + item.Nazwa + " ID:" + item.ID + "\n";
                    Notes.Add(text);
                }

            }
        }
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

            // get first day of month.
            DateTime startofthemonth = new DateTime(year, month, 1);
            // get the count of the days of the month
            int days = DateTime.DaysInMonth(year, month);
            // convert the startofthemonth to int
            int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d"));
            // first create a blank usercontrol
            if (dayoftheweek == 0)
            {
                dayoftheweek = 7;
            }
            int i = (dayoftheweek - 1);
            int idx = 1;

            for (int j = 3; j < 8; j++)
            {
                for (int k = i; k < 7; k++)
                {
                    Days ucDays = new Days();
                    ucDays.days(idx);
                    Grid.SetColumn(ucDays, k);
                    Grid.SetRow(ucDays, j);
                    MyGrid.Children.Add(ucDays);
                    if (idx == days)
                        break;
                    idx++;
                }
                i = 0;
                if(idx == days)
                  break;
            }
        }
    }
}
