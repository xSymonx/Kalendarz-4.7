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
    /// Logika interakcji dla klasy Add_Task_Window.xaml
    /// </summary>
    public partial class Add_Task_Window : Window
    {
        /// <summary>
        /// Konstruktor klasy Add_Task_Window
        /// </summary>
        public Add_Task_Window()
        {
            this.DataContext = this;
            InitializeComponent();
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
        /// Dodanie zadania do bazy danych
        /// </summary>
        /// <param name="Task"></param>
        public void AddNewTask(Task Task)
        {
            using (var context = new TaskContext())
            {
                context.Tasks.Add(Task);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Zatwierdzenie i obsługa danych podanych przez użytkowanika
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Anulowanie dodania zadania
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Decline_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
