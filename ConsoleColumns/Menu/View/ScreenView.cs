using Columns;
using Columns.Menu;
using Columns.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleColumns.Menu.View
{
    /// <summary>
    /// Предсталвние экрана
    /// </summary>
    public class ScreenView: IView
    {

        /// <summary>
        /// Экран
        /// </summary>
        private Screen _screen;

        /// <summary>
        /// Список текстовых компонентов
        /// </summary>
        private List<TextComponentView> _textComponentViews;

        /// <summary>
        /// Свойство для представления экрана
        /// </summary>
        public Screen Screen { get => _screen; set => _screen = value; }

        /// <summary>
        /// Свойство для списка текстовых компонентов
        /// </summary>
        public List<TextComponentView> TextComponentViews { get => _textComponentViews; set => _textComponentViews = value; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parScreen">Экран</param>
        public ScreenView(Screen parScreen)
        {
            _screen = parScreen;
            _textComponentViews = new List<TextComponentView>();
            _textComponentViews.Add(new TextComponentView(Screen.Title, 
                new Coord(Console.WindowWidth / 2 - Screen.Title.Text.Length / 4, 3), 1, 0x44));
            for (int i = 0; i < Screen.TextComponents.Count; i++)
            {
                TextComponentViews.Add(new TextComponentView(Screen.TextComponents[i], new Coord(10, 3 + (i + 1) * 4), Screen.TextComponents[i].Text.Length, 3));
            }
        }

        /// <summary>
        /// Отрисовка
        /// </summary>
        public virtual void Draw()
        {
            TextComponentViews.ForEach(tc => tc.Draw());
        }
    }
}
