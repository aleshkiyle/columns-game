using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Columns
{
    /// <summary>
    /// Координаты
    /// </summary>
    public class Coord
    {
        /// <summary>
        /// Координата по оси x
        /// </summary>
        private double _x;

        /// <summary>
        /// Координата по оси y
        /// </summary>
        private double _y;

        /// <summary>
        /// Свойство координаты по оси x
        /// </summary>
        public double X { get => _x; set => _x = value; }

        /// <summary>
        /// Свойство координаты по оси y
        /// </summary>
        public double Y { get => _y; set => _y = value; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parX">Координата x</param>
        /// <param name="parY">Координата y</param>
        public Coord(double parX, double parY)
        {
            _x = parX;
            _y = parY;
        }
    }
}
