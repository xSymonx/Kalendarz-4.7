﻿using System;
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
        public int Task_Index { get; set; } = 0;
        public Delete_Task_Window()
        {
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

        public void DeleteTask(Task task)
        {
            using (var context = new TaskContext())
            {
                context.Tasks.Attach(task);
                context.Entry(task).State = System.Data.Entity.EntityState.Deleted;
                //context.Tasks.Remove(task);
                context.SaveChanges();
            }
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            int id = Int32.Parse(User_Text.Text);
            using (var context = new TaskContext())
            {
                IQueryable<Task> query = context.Tasks;
                foreach (var item in query)
                {  
                    if(item.ID == id)
                    DeleteTask(item);
                }

            }
            this.Close();
        }

        private void Decline_Click(object sender, RoutedEventArgs e)
        {
            Task_Index = 0;
            this.Close();
        }
    }
}
