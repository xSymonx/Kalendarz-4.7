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
        public static ObservableCollection<Task> Notes { get; set; } = new ObservableCollection<Task>();
        public static ObservableCollection<string> Months { get; set; } = new ObservableCollection<string>();
        int month, year;
        public string Basic_City { get; set; } = "Wroclaw";
        public static int static_month, static_year;
        public static string static_date;

        public MainWindow()
        {
            this.DataContext = this;
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
            MyWrapPanel.Children.Clear();

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

        private void Add_Task_Click(object sender, RoutedEventArgs e)
        {
            Add_Task_Window add_Task_Window = new Add_Task_Window();
            add_Task_Window.ShowDialog();
            ShowTasks();

        }


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

        private void TASKS_BUTTON_Click(object sender, RoutedEventArgs e)
        {
            ShowTasks();   
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
                    Notes.Add(item);
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
