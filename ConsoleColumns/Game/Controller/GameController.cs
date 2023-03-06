using Columns.Game;
using Columns.View;
using ConsoleColumns.Game.View;
using ConsoleColumns.Menu.Controller;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleColumns.Game.Controller
{
    /// <summary>
    /// Контроллер игры
    /// </summary>
    public class GameController : IController
    {

        /// <summary>
        /// Экземпляр объекта (singleton)
        /// </summary>
        private static readonly GameController _instance = new GameController();

        /// <summary>
        /// Флаг для завершения работы контроллера
        /// </summary>
        private bool _isExit = false;

        /// <summary>
        /// Представление игры
        /// </summary>
        private GameFieldView _gameView;

        /// <summary>
        /// Поле игры
        /// </summary>
        private GameField _gameField;

        /// <summary>
        /// Экземпляр объекта (singleton)
        /// </summary>
        public static GameController Instance => _instance;

        /// <summary>
        /// Конструктор
        /// </summary>
        public GameController()
        {
        }

        /// <summary>
        /// Инициализация игры
        /// </summary>
        private void InitGame()
        {
            _gameField = new GameField();
            _gameView = new GameFieldView(_gameField);
            _gameField.OnGameOver += Stop;
        }

        /// <summary>
        /// Старт игры
        /// </summary>
        public void Start()
        {
            InitGame();
            _isExit = false;
            FastOutput.GetInstance().ClearScreen();
            _gameView.Draw();
            _gameField.Start();
            while (!_isExit)
            {
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        _gameField.Move(KeyDirection.Up);
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        _gameField.Move(KeyDirection.Down);
                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        _gameField.Move(KeyDirection.Left);
                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        _gameField.Move(KeyDirection.Right);
                        break;
                    case ConsoleKey.Escape:
                        Stop();
                        break;
                }
            }
            new InputRecordController(_gameField.Score).Start();
            FastOutput.GetInstance().ClearScreen();
        }

        /// <summary>
        /// Остановка игры
        /// </summary>
        public void Stop()
        {
            _gameField.Stop();
            _isExit = true;
        }

        /// <summary>
        /// Завершение игры
        /// </summary>
        private void GameOver()
        {
            Stop();
            _gameView.DrawGameOver();
        }
 
    }
}
