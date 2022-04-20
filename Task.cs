using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalendarz
{
    /// <summary>
    /// Logika interakcji dla klasy Task
    /// </summary>
    public class Task
    {
        /// <summary>
        /// ID danego zadania
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Nazwa danego zadania
        /// </summary>
        public string Nazwa { get; set; }

        /// <summary>
        /// Data danego zadania
        /// </summary>
        public string Data { get; set; }
    }
}
