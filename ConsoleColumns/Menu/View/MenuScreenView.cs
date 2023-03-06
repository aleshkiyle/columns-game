using Columns;
using Columns.Menu;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleColumns.Menu.View
{

    /// <summary>
    /// Представление меню экрана
    /// </summary>
    public class MenuScreenView : ScreenView
    {
        /// <summary>
        /// Представления пунктов меню
        /// </summary>
        private readonly List<MenuPointView> _menuPointViews;

        /// <summary>
        /// Свойство представлений пунктов меню
        /// </summary>
        public List<MenuPointView> MenuPointViews => _menuPointViews;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parMenuScreen">Экран меню</param>
        public MenuScreenView(MenuScreen parMenuScreen) : base(parMenuScreen)
        {
            _menuPointViews = new List<MenuPointView>();
            for (int i = 0; i < parMenuScreen.Points.Count; i++)
            {
                _menuPointViews.Add(new MenuPointView(parMenuScreen.Points[i],
                    new Coord(Console.WindowWidth / 2 - 8, 5 + (i + 1) * 5)));
            }
            parMenuScreen.Drawer += Draw;
        }

        /// <summary>
        /// Отрисовка экрана
        /// </summary>
        public override void Draw()
        {
            MenuPointViews.ForEach(mpv => mpv.Draw());
        }
    }
}
