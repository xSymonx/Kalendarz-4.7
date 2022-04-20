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

namespace Kalendarz
{
    /// <summary>
    /// Logika interakcji dla klasy Delete_Task_Window.xaml
    /// </summary>
    public partial class Delete_Task_Window : Window
    {
        /// <summary>
        /// Indeks wybranego zadania do usunięcia
        /// </summary>
        public int Task_Index { get; set; } = 0;

        /// <summary>
        /// Konstruktor klasy Delete_Task_Window
        /// </summary>
        public Delete_Task_Window()
        {
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
        /// Usunięcie danego zadania z bazy danych
        /// </summary>
        /// <param name="task"></param>
        public void DeleteTask(Task task)
        {
            using (var context = new TaskContext())
            {
                context.Tasks.Attach(task);
                context.Entry(task).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Znalezienie i usunięcie danego zadania
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new TaskContext())
            {
                IQueryable<Task> query = context.Tasks;
                foreach (var item in query)
                {  
                    if(item.ID == Task_Index)
                    DeleteTask(item);
                }
            }
            this.Close();
        }

        /// <summary>
        /// Anulowanie usunięcia danego zadania
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Decline_Click(object sender, RoutedEventArgs e)
        {
            Task_Index = 0;
            this.Close();
        }
    }
}
