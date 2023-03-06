using Columns.Game;
using Columns.Menu;
using Columns.View;
using ConsoleColumns.Menu.Controller;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;
using WpfColumns.Game.View;
using WpfColumns.Menu.Controller;

namespace WpfColumns.Game.Controller
{
    /// <summary>
    /// Игровой контроллер
    /// </summary>
    public class GameController : IController
    {

        /// <summary>
        /// Экземпляр объекта (singletone)
        /// </summary>
        private static readonly GameController _instance = new GameController();

        /// <summary>
        /// Игра
        /// </summary>
        private GameField _gameField;

        /// <summary>
        /// Представление игры
        /// </summary>
        private GameFieldView _gameScreenView;

        /// <summary>
        /// Окно приложения
        /// </summary>
        private Window _window;

        /// <summary>
        /// Экземпляр объекта (singletone)
        /// </summary>
        public static GameController Instance => _instance;

        /// <summary>
        /// Конструктор
        /// </summary>
        private GameController()
        {
            _window = Program.Window;
        }

        /// <summary>
        /// Инициализация
        /// </summary>
        private void InitGame()
        {
            _gameField = new GameField();
            _gameScreenView = new GameFieldView(_gameField, (Canvas) _window.Content);
        }


        /// <summary>
        /// Запуск контроллера
        /// </summary>
        public void Start()
        {
            InitGame();
            _gameField.Start();
            _gameScreenView.Draw();
            _window.KeyDown += KeyDown;
        }

        /// <summary>
        /// Остановка контроллера
        /// </summary>
        public void Stop()
        {
            _gameField.Stop();
            _window.KeyDown -= KeyDown;
            _window.KeyDown += ExitKeyDown;
        }

        /// <summary>
        /// Обработка нажатия кнопки для завершения игры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitKeyDown(object sender, KeyEventArgs e)
        {
            _window.KeyDown -= ExitKeyDown;
            Screen screen = new Screen(new TextComponent("Save record"), new List<TextComponent>());
            new InputRecordsController(_gameField.Score).Start();
        }

        /// <summary>
        /// Обработка нажатия кнопок во время игры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                case Key.Up:
                    _gameField.Move(KeyDirection.Up);
                    break;
                case Key.S:
                case Key.Down:
                    _gameField.Move(KeyDirection.Down);
                    break;
                case Key.D:
                case Key.Right:
                    _gameField.Move(KeyDirection.Right);
                    break;
                case Key.A:
                case Key.Left:
                    _gameField.Move(KeyDirection.Left);
                    break;
                case Key.Escape:
                    Stop();
                    break;
            }
        }
    }
}
