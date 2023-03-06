using Columns;
using Columns.Menu;
using Columns.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleColumns.Menu.View
{
    /// <summary>
    /// Пункты меню отображение
    /// </summary>
    public class MenuPointView : IView
    {

        /// <summary>
        /// Жёлтый цвет
        /// </summary>
        private static readonly int YELLOW_COLOR = 0xEE;

        /// <summary>
        /// Красный цвет
        /// </summary>
        private static readonly int RED_COLOR = 0x44;

        /// <summary>
        /// Пункт меню
        /// </summary>
        private MenuPoint _menuPoint;

        /// <summary>
        /// Координаты
        /// </summary>
        private Coord _coord;

        /// <summary>
        /// Свойства для пункта меню
        /// </summary>
        public MenuPoint MenuPoint { get => _menuPoint; set => _menuPoint = value; }

        /// <summary>
        /// Свойства для координат
        /// </summary>
        public Coord Coord { get => _coord; set => _coord = value; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parMenuPoint">Пункт меню</param>
        /// <param name="parCoord">Координаты</param>
        public MenuPointView(MenuPoint parMenuPoint, Coord parCoord)
        {
            _menuPoint = parMenuPoint;
            _coord = parCoord;
        }

        /// <summary>
        /// Отрисовка пунктом меню
        /// </summary>
        public void Draw()
        {
            FastOutput fastOutput = FastOutput.GetInstance();
            string top = "".PadRight(_menuPoint.Text.Length + 2, ' ');
            string text = " " + MenuPoint.Text + " ";
            string bottom = "".PadRight(_menuPoint.Text.Length + 2, ' ');
            int _bc;
            int _fc;
            if (MenuPoint.IsSelected)
            {
                _bc = RED_COLOR;
                _fc = YELLOW_COLOR;
            }
            else
            {
                _bc = YELLOW_COLOR;
                _fc = RED_COLOR;
            }
            fastOutput.OutputString(top, _bc, _fc, (int) Coord.X, (int) Coord.Y);
            fastOutput.OutputString(text, _bc, _fc, (int)Coord.X, (int)Coord.Y + 1);
            fastOutput.OutputString(bottom, _bc, _fc, (int)Coord.X, (int)Coord.Y + 2);
        }
    }
}
