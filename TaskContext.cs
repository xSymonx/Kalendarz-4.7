using System;
using System.Data.Entity;
using System.Linq;

namespace Kalendarz
{
    /// <summary>
    /// Klasa tworzaca most miedzy aplikacja a baza danych
    /// </summary>
    public class TaskContext : DbContext
    {
        // Your context has been configured to use a 'TaskContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Kalendarz.TaskContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'TaskContext' 
        // connection string in the application configuration file.


        /// <summary>
        /// Konstruktor klasy kontekstu
        /// </summary>
        public TaskContext()
            : base("name=TaskContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        /// <summary>
        /// Opis tabeli znajdujacej siê w bazie danych
        /// </summary>
        public DbSet<Task> Tasks { get; set; }
    }
}