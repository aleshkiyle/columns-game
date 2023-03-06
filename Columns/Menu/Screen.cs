namespace Columns.Menu
{
    /// <summary>
    /// Экран
    /// </summary>
    public class Screen
    {

        /// <summary>
        /// Заголовок
        /// </summary>
        private TextComponent _title;

        /// <summary>
        /// Список текстовых компонентов
        /// </summary>
        private List<TextComponent> _textComponents;

        /// <summary>
        /// Свойство заголовка
        /// </summary>
        public TextComponent Title { get => _title; set => _title = value; }

        /// <summary>
        /// Свойство списка текстовых компонентов
        /// </summary>
        public List<TextComponent> TextComponents { get => _textComponents; set => _textComponents = value; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parTitle">Заголовок</param>
        public Screen(TextComponent parTitle)
        {
            _title = parTitle;
            _textComponents = new List<TextComponent>();
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parTitle">Заголовок</param>
        /// <param name="parTextComponents">Текстовые компоненты</param>
        public Screen(TextComponent parTitle, List<TextComponent> parTextComponents)
        {
            _title = parTitle;
            _textComponents = parTextComponents;
        }
    }
}
