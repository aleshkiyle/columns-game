using Columns;
using Columns.Game;
using Columns.View;

namespace ConsoleColumns.Game.View
{
    /// <summary>
    /// Представление игры
    /// </summary>
    public class GameFieldView : IView
    {

        /// <summary>
        /// Текст в случае завершение игры
        /// </summary>
        private const string GAME_OVER = "GAME OVER!";

        /// <summary>
        /// Зеленый цвет
        /// </summary>
        private const int GREEN_COLOR = 0x42;

        /// <summary>
        /// Красный цвет
        /// </summary>
        private const int RED_COLOR = 0x44;

        /// <summary>
        /// Оранжевый цвет
        /// </summary>
        private const int ORANGE_COLOR = 0x46;

        /// <summary>
        /// Символ фигуры
        /// </summary>
        private const char FIGURE = '█';

        /// <summary>
        /// Позиция для текста завершения игры
        /// </summary>
        private static readonly Coord GAME_OVER_TEXT_POSITION = new(4.0, 5.0);

        /// <summary>
        /// Цвет фона текста завршения игры
        /// </summary>
        private static readonly int BACKGROUND_COLOR = 0;

        /// <summary>
        /// Игра
        /// </summary>
        private GameField _game;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parGame">Игра</param>
        public GameFieldView(GameField parGame)
        {
            _game = parGame;
            _game.OnGameOver += DrawGameOver;
            _game.OnRedraw += Draw;
        }

        /// <summary>
        /// Отрисова игрового поля
        /// </summary>
        public void Draw()
        {
            FastOutput fs = FastOutput.GetInstance();
            fs.ClearScreen();

            for (int i = 0; i < _game.Fields.Length / GameField.Rows; i++)
            {
                for (int j = 0; j < _game.Fields.Length / GameField.Columns; j++)
                {
                    int colorSymbol = GetColor(_game.Fields[i,j]);

                    if (i == 0 || i == GameField.Columns - 1 || j == 0 || j == GameField.Rows - 1)
                    {
                        colorSymbol = RED_COLOR;
                    }
                    fs.OutputCharacter(FIGURE, 0, colorSymbol, i, j, 1, 1);
                }
            }
            fs.OutputString("Score: " + _game.Score.ToString(), 0, RED_COLOR, 25, 5);
            fs.OutputCharacter(FIGURE, 0, GetColor(_game.NewBlock[0]), 25, 10, 1, 1);
            fs.OutputCharacter(FIGURE, 0, GetColor(_game.NewBlock[1]), 25, 11, 1, 1);
            fs.OutputCharacter(FIGURE, 0, GetColor(_game.NewBlock[2]), 25, 12, 1, 1);
        }

        /// <summary>
        /// Отрисовка завершения игры
        /// </summary>
        public void DrawGameOver()
        {
            FastOutput fs = FastOutput.GetInstance();

            string back = " " + "".PadRight(GAME_OVER.Length, ' ') + " ";
            string text = " " + GAME_OVER + " ";
            fs.OutputString(back, 0, 0, 40, 5);
            fs.OutputString(text, 0, 0x44, 40, 5);
        }

        /// <summary>
        /// Получение цвета фигуры
        /// </summary>
        /// <param name="parFig">Конкретная фигура</param>
        /// <returns>Цвет фигуры в ASCII</returns>
        private int GetColor(int parFig)
        {
            switch (parFig)
            {
                case 1:
                    return RED_COLOR;
                case 2:
                    return GREEN_COLOR;
                case 3:
                    return ORANGE_COLOR;
                default:
                    return 0;
            }
        }
    }
}
