using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleColumns.Menu.Controller
{

    /// <summary>
    /// Контроллер
    /// </summary>
    public interface IController
    {

        /// <summary>
        /// Запустить контроллер
        /// </summary>
        public void Start();

        /// <summary>
        /// Остановка работы контроллера
        /// </summary>
        public void Stop();
    }
}
