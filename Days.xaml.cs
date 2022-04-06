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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kalendarz
{
    /// <summary>
    /// Logika interakcji dla klasy Days.xaml
    /// </summary>
    public partial class Days : UserControl
    {
        public static string static_day;
        public Days()
        {
            InitializeComponent();
        }
        public void days(int numday)
        {
            mybtn.Content = numday + "";
        }
        public void ShowTasks()
        {
            string temp_date;
            
            temp_date = Days.static_day + "/";
            if (temp_date.Length != 3)
                temp_date = "0" + temp_date;

            if (MainWindow.static_month<10)
                temp_date = temp_date + "0" + MainWindow.static_month + "/";
            else
                temp_date = temp_date + MainWindow.static_month + "/";

            MainWindow.static_date = temp_date + MainWindow.static_year;

            MainWindow.Notes.Clear();
            using (var context = new TaskContext())
            {
                IQueryable<Task> query = context.Tasks.Where(T => T.Data == MainWindow.static_date);
                foreach (var item in query)
                {
                    //string text = "• " + item.Data + " " + item.Nazwa + " ID:" + item.ID + "\n";
                    string text = "• " + item.Data + " " + item.Nazwa + " ID:" + MainWindow.static_date + "\n";
                    MainWindow.Notes.Add(item);
                }

            }
        }
        private void mybtn_Click(object sender, RoutedEventArgs e)
        {
            static_day = (string)mybtn.Content;
            ShowTasks();
        }
    }
}
