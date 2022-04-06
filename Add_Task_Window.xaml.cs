using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kalendarz
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class Add_Task_Window : Window
    {
        public string Task { get; set; } = "BRAK";
        public Add_Task_Window()
        {
            this.DataContext = this;
            InitializeComponent();
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

        public void AddNewTask(Task Task)
        {
            using (var context = new TaskContext())
            {
                context.Tasks.Add(Task);
                context.SaveChanges();
            }
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            var Task = new Task();
            Task.Nazwa = User_Text.Text;

            if (Regex.IsMatch(User_Date.Text, @"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"))
                Task.Data = User_Date.Text;
            else
                Task.Data = "";
            AddNewTask(Task);
            this.Close();
        }

        private void Decline_Click(object sender, RoutedEventArgs e)
        {
            Task = "BRAK";
            this.Close();
        }
    }
}
