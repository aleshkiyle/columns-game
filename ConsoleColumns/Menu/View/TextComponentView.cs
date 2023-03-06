using Columns;
using Columns.Menu;
using Columns.View;
using ConsoleColumns.Menu.View.Builder;

namespace ConsoleColumns.Menu.View
{
    /// <summary>
    /// Вид текстового компонента
    /// </summary>
    public class TextComponentView : IView
    {

        /// <summary>
        /// Цвет заднего фона
        /// </summary>
        private int _backgroundColor = 0xEE;

        /// <summary>
        /// Цвет шрифта
        /// </summary>
        private int _fontColor = 0x44;

        /// <summary>
        /// Текстовый компонент
        /// </summary>
        private TextComponent _textComponent;

        /// <summary>
        /// Координаты
        /// </summary>
        private Coord _coord;


        /// <summary>
        /// Свойство для текстового компонента
        /// </summary>
        public TextComponent TextComponent { get => _textComponent; set => _textComponent = value; }

        /// <summary>
        /// Свойство для координаты
        /// </summary>
        public Coord Coord { get => _coord; set => _coord = value; }

        /// <summary>
        /// Свойство для цвета заднего фона
        /// </summary>
        public int BackgroundColor { get => _backgroundColor; set => _backgroundColor = value; }

        /// <summary>
        /// Свойство для цвета шрифта
        /// </summary>
        public int FontColor { get => _fontColor; set => _fontColor = value; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parTextComponent">Текстовый компонент</param>
        /// <param name="parCoord">Координаты</param>
        /// <param name="parBackgroundColor">Цвет заднего фона</param>
        /// <param name="parFontColor">Цвет шрифта</param>
        public TextComponentView(TextComponent parTextComponent, Coord parCoord, int parBackgroundColor, int parFontColor)
        {
            _textComponent = parTextComponent;
            _coord = parCoord;
            _backgroundColor = parBackgroundColor;
            _fontColor = parFontColor;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parTextComponent">Текстовый компонент</param>
        /// <param name="parCoord">Координаты</param>
        public TextComponentView(TextComponent parTextComponent, Coord parCoord)
        {
            _textComponent = parTextComponent;
            _coord = parCoord;
        }

        /// <summary>
        /// Построение отображение текстового компонента
        /// </summary>
        /// <returns>Представление текстового компонента</returns>
        public static TextComponentView BuilderTextComponentView()
        {
            CreatorTextComponentViewBuilder creatorTextComponentViewBuilder = new();
            creatorTextComponentViewBuilder.CreateTextComponentView();
            creatorTextComponentViewBuilder.SetTextComponent();
            creatorTextComponentViewBuilder.SetCoord();
            creatorTextComponentViewBuilder.SetFontColor();
            creatorTextComponentViewBuilder.SetBackgroundColor();
            return creatorTextComponentViewBuilder.TextComponentView;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public TextComponentView()
        {
        }

        /// <summary>
        /// Отрисовка
        /// </summary>
        public void Draw()
        {
            FastOutput fs = FastOutput.GetInstance();
            string back = " " + "".PadRight(_textComponent.Text.Length, ' ') + " ";
            string text = " " + _textComponent.Text + " ";
            fs.OutputString(text, 0, _fontColor, (int)_coord.X, (int)_coord.Y + 1);
        }
    }
}
