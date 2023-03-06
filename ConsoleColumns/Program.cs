using ConsoleColumns.Menu.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleColumns
{
    /// <summary>
    /// Консольное приложение
    /// </summary>
    public class Program
    {

        /// <summary>
        /// Точка входа в программу
        /// </summary>
        /// <param name="args">Аргументы консольного приложения</param>
        public static void Main(string[] args)
        {       
            MenuScreenController.Instance.Start();
        }
    }
}
