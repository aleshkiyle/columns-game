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
    /// Абстрактный класс для построения отображения текстового компонента
    /// </summary>
    public abstract class TextComponentViewBuilder
    {
        /// <summary>
        /// Отображение текстового компонента
        /// </summary>
        public TextComponentView TextComponentView { get; private set; }

        /// <summary>
        /// Создание отображения текстового компонента
        /// </summary>
        public void CreateTextComponentView()
        {
            TextComponentView = new TextComponentView();
        }

        /// <summary>
        /// Установить текстовый компоненты
        /// </summary>
        /// <returns>Текстовый компонент</returns>
        public abstract TextComponent SetTextComponent();

        /// <summary>
        /// Установить координаты
        /// </summary>
        /// <returns>Координаты</returns>
        public abstract Coord SetCoord();

        /// <summary>
        /// Установить цвет заднего фона 
        /// </summary>
        /// <returns>Цвет заднего фона</returns>
        public abstract int SetBackgroundColor();

        /// <summary>
        /// Установить цвет шрифта
        /// </summary>
        /// <returns>Цвет шрифта</returns>
        public abstract int SetFontColor();
    }
}
