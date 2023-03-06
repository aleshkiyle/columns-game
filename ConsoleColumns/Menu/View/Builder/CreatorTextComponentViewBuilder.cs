using Columns;
using Columns.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleColumns.Menu.View.Builder
{
    /// <summary>
    /// Создание отображения текстового компонента
    /// </summary>
    public class CreatorTextComponentViewBuilder : TextComponentViewBuilder
    {

        /// <summary>
        /// Экран
        /// </summary>
        private Screen _screen;


        /// <summary>
        /// Свойство для представления экрана
        /// </summary>
        public Screen Screen { get => _screen; }

        /// <summary>
        /// Установить текстовый компоненты
        /// </summary>
        /// <returns>Текстовый компонент</returns>
        public override TextComponent SetTextComponent()
        {
            return Screen.Title;
        }

        /// <summary>
        /// Установить координаты
        /// </summary>
        /// <returns>Координаты</returns>
        public override Coord SetCoord()
        {
            return new Coord(Console.WindowWidth / 2 - Screen.Title.Text.Length / 4, 3);
        }

        /// <summary>
        /// Установить цвет заднего фона 
        /// </summary>
        /// <returns>Цвет заднего фона</returns>
        public override int SetBackgroundColor()
        {
            return 1;
        }

        /// <summary>
        /// Установить цвет шрифта
        /// </summary>
        /// <returns>Цвет шрифта</returns>
        public override int SetFontColor()
        {
            return 0x44;
        }
    }
}
