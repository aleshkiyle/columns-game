using Columns.Game;
using Columns.View;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfColumns.Game.View
{
    /// <summary>
    /// Представление игры
    /// </summary>
    public class GameFieldView : IView
    {

        /// <summary>
        /// Текст завершения игры
        /// </summary>
        private const string GAME_OVER = "GAME OVER";

        /// <summary>
        /// Компонент-контейнер игрового поля
        /// </summary>
        private Canvas _canvas;
        /// <summary>
        /// Игра
        /// </summary>
        private readonly GameField _gameField;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parGame">Игра</param>
        /// <param name="parGrid">Контейнер окна</param>
        public GameFieldView(GameField parGame, Canvas parCanvas)
        {
            _gameField = parGame;
            _canvas = parCanvas;
            _gameField.OnGameOver += DrawGameOver;
            _gameField.OnRedraw += Draw;
        }

        /// <summary>
        /// Отрисовать
        /// </summary>
        public void Draw()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _canvas.Children.Clear();

                DrawField();
                DrawCells();
                DrawRecords();
                DrawdDropDownFigure();
            });
        }

        /// <summary>
        /// Отрисовать поле
        /// </summary>
        private void DrawField()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Rectangle r = new Rectangle();
                r.Width = GameField.Columns * 30;
                r.Height = GameField.Rows * 30;
                r.StrokeThickness = 1;
                r.Stroke = new SolidColorBrush(Colors.Gold);
                _canvas.Children.Add(r);
            });
        }

        /// <summary>
        /// Отрисовать ячейки
        /// </summary>
        private void DrawCells()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                for (int i = 0; i < _gameField.Fields.Length / GameField.Rows; i++)
                {
                    for (int j = 0; j < _gameField.Fields.Length / GameField.Columns; j++)
                    {
                        Brush brush = GetColor(_gameField.Fields[i, j]);
                        Ellipse ellipse = new Ellipse();
                        ellipse.Fill = brush;
                        ellipse.Height = 30;
                        ellipse.Width = 30;
                        Canvas.SetLeft(ellipse, i * 30);
                        Canvas.SetTop(ellipse, j * 30);
                        _canvas.Children.Add(ellipse);
                    }
                }
            });
        }

        /// <summary>
        /// Отрисовать рекорды
        /// </summary>

        private void DrawRecords()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = "Score: " + _gameField.Score.ToString();
                textBlock.FontSize = 18;
                textBlock.FontFamily = new FontFamily("Comic Sans MS");
                Canvas.SetLeft(textBlock, 400);
                Canvas.SetTop(textBlock, 200);
                _canvas.Children.Add(textBlock);
            });
        }

        /// <summary>
        /// Отрисовать следующую выпадающую фигуру
        /// </summary>
        private void DrawdDropDownFigure()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Brush brush1 = GetColor(_gameField.NewBlock[0]);
                Brush brush2 = GetColor(_gameField.NewBlock[1]);
                Brush brush3 = GetColor(_gameField.NewBlock[2]);
                Ellipse ellipse1 = new Ellipse();
                ellipse1.Fill = brush1;
                ellipse1.Height = 30;
                ellipse1.Width = 30;
                Ellipse ellipse2 = new Ellipse();
                ellipse2.Fill = brush2;
                ellipse2.Height = 30;
                ellipse2.Width = 30;
                Ellipse ellipse3 = new Ellipse();
                ellipse3.Fill = brush3;
                ellipse3.Height = 30;
                ellipse3.Width = 30;
                TextBlock textBlock = new TextBlock();
                textBlock.Text = "Drop-Down figure: ";
                textBlock.FontSize = 18;
                textBlock.FontFamily = new FontFamily("Comic Sans MS");
                Canvas.SetLeft(textBlock, 400);
                Canvas.SetTop(textBlock, 300);
                Canvas.SetLeft(ellipse1, 440);
                Canvas.SetTop(ellipse1, 330);
                Canvas.SetLeft(ellipse2, 440);
                Canvas.SetTop(ellipse2, 360);
                Canvas.SetLeft(ellipse3, 440);
                Canvas.SetTop(ellipse3, 390);
                _canvas.Children.Add(textBlock);
                _canvas.Children.Add(ellipse1);
                _canvas.Children.Add(ellipse2);
                _canvas.Children.Add(ellipse3);
            });
        }

        /// <summary>
        /// Вывод сообщения о том, что игра была завершена
        /// </summary>
        public void DrawGameOver()
        {
            ShowMessage(GAME_OVER);
        }

        /// <summary>
        /// Показать сообщение
        /// </summary>
        /// <param name="parMessage">Текст</param>
        private void ShowMessage(string parMessage)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = parMessage;
                textBlock.FontSize = 64;
                textBlock.Foreground = new SolidColorBrush(Colors.Red);
                textBlock.FontFamily = new FontFamily("Comic Sans MS");
                Canvas.SetLeft(textBlock, 400);
                Canvas.SetTop(textBlock, 50);
                Canvas.SetZIndex(textBlock, 10);

                _canvas.Children.Add(textBlock);
            });
        }

        /// <summary>
        /// Получение цвета фигуры
        /// </summary>
        /// <param name="parFig">Конкретная фигура</param>
        /// <returns>Цвет фигуры в ASCII</returns>
        private Brush GetColor(int parFig)
        {
            switch (parFig)
            {
                case 1:
                    return new SolidColorBrush(Colors.Red);
                case 2:
                    return new SolidColorBrush(Colors.Green);
                case 3:
                    return new SolidColorBrush(Colors.Orange);
                default:
                    return new SolidColorBrush(Colors.White);
            }
        }
    }
}
