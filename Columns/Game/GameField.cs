using System.Diagnostics.Contracts;

namespace Columns.Game
{
    /// <summary>
    /// Игровое поле
    /// </summary>
    public class GameField
    {
        /// <summary>
        /// Количество колонок в массиве
        /// </summary>
        public static int Columns { get; set; } = 8;

        /// <summary>
        /// Количество строк в массиве
        /// </summary>
        public static int Rows { get; set; } = 16;

        /// <summary>
        /// Основной массив со всеми данными для вывода на форму
        /// </summary>
        private int[,] _fields = new int[Columns, Rows];

        /// <summary>
        /// Вспомогательный массив, используемый для удаления сгоревших элементов
        /// </summary>
        private int[,] _newFields = new int[Columns, Rows];

        /// <summary>
        /// Падающий блок
        /// </summary>
        private int[] _newBlock = new int[3];

        /// <summary>
        /// Начальная колонка движения блока
        /// </summary>
        private int _column = 4;

        /// <summary>
        /// Флаг на последний шаг
        /// </summary>
        private bool _lastMove = false;

        /// <summary>
        /// Направление нажатия
        /// </summary>
        private KeyDirection _keyDirection;

        /// <summary>
        /// Флаг для нажатия кнопки
        /// </summary>
        private bool _ifButtonPressed = false;

        /// <summary>
        /// Флаг завершения игры
        /// </summary>
        private bool _isGameEnd;

        /// <summary>
        /// Объект блокировки
        /// </summary>
        private Object _lock = new();

        /// <summary>
        /// Поток
        /// </summary>
        private Thread _thread;

        /// <summary>
        /// Свойство 
        /// </summary>
        public bool IsFieldsUpdated { get; set; } = true; // Флаг, отвечающий за повторный запуск метода поиска рядом стоящих блоков, в случае, если блоки были удалены - проставляется true

        /// <summary>
        /// Свойство, показывающее что игра не закочена
        /// </summary>
        public bool IsPlaying { get; set; } = true; // флаг, показывающий, что игра не закончена

        /// <summary>
        /// Свойство для запуска нового блока
        /// </summary>
        public bool StartNewBlockAllow { get; set; } = true; // флаг для запуска нового блока

        /// <summary>
        /// Свойство для обращения к основному массиву
        /// </summary>

        /// <summary>
        /// Cвойство для обращения к падающему блоку
        /// </summary>
        public int[] NewBlock { get => _newBlock; }

        /// <summary>
        /// Свойство для очков, набранных в процессе игры
        /// </summary>
        public int Score { get; set; } = 0; // очки, набираемые в процессе игры

        /// <summary>
        /// Свойство для направления нажатия
        /// </summary>
        public KeyDirection Key { get => _keyDirection; set => _keyDirection = value; }

        /// <summary>
        /// Свойства для флага нажатия кнопки
        /// </summary>
        public bool IfButtonPressed { get => _ifButtonPressed; set => _ifButtonPressed = value; }

        /// <summary>
        /// Завершение игры
        /// </summary>
        public bool IsGameEnd { get => _isGameEnd; set => _isGameEnd = value; }

        /// <summary>
        /// Основной массив со всеми данными для вывода на форму
        /// </summary>
        public int[,] Fields { get => _fields; set => _fields = value; }

        /// <summary>
        ///  Начальная колонка движения блока
        /// </summary>
        public int Column { get => _column; set => _column = value; }

        /// <summary>
        /// Вспомогательный массив, используемый для удаления сгоревших элементов
        /// </summary>
        public int[,] NewFields { get => _newFields; set => _newFields = value; }

        /// <summary>
        /// Делегат перерисовки
        /// </summary>
        public delegate void dRedraw();

        /// <summary>
        /// Делегат завершения игры
        /// </summary>
        public delegate void dGameOver();

        /// <summary>
        /// Событие перерисовки
        /// </summary>
        public event dRedraw OnRedraw;

        /// <summary>
        /// Событие завершения игры
        /// </summary>
        public event dGameOver OnGameOver;


        /// <summary>
        /// Конструктор
        /// </summary>
        public GameField()
        {
        }

        /// <summary>
        /// Старт игры
        /// </summary>
        public void Start()
        {
            IsGameEnd = false;
            _thread = new Thread(DoGame);
            _thread.Start();
        }

        /// <summary>
        /// Остановка игры
        /// </summary>
        public void Stop()
        {
            IsGameEnd = true;
        }

        /// <summary>
        /// Цикл игры
        /// </summary>
        public void DoGame()
        {
            GenerateBlock();
            while (!CheckGameOver() && !IsGameEnd)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                OnRedraw.Invoke();
                if (!_lastMove)
                {
                    if (StartNewBlockAllow)
                    {
                        StartRunBlock();
                        GenerateBlock();
                    }
                    lock (_lock)
                    {
                        IsPlaying = RunBlock();
                    }
                    _lastMove = IsBlockMovementEnd(GetIndexOfLastClearFieldBeforeBlock());
                }
                else
                {
                    DeleteEqualsFields();
                    _lastMove = IsFieldsUpdated;
                    IsFieldsUpdated = false;
                }
                while (watch.ElapsedMilliseconds < 666) { }
                watch.Stop();
            }
            OnGameOver.Invoke();
        }

        /// <summary>
        /// Поиск и удаление рядом стоящих блоков
        /// </summary>
        public void DeleteEqualsFields()
        {
            UpdateNewFieldsFromFields();
            SearchEqualsFields(); 
            Scoring();
            DeleteEqualsFieldsInNewFields();
            UpdateFieldsFromNewFields();
            StartNewBlockAllow = true;
        }

        /// <summary>
        /// Поиск элементов для удаления
        /// </summary>
        private void SearchEqualsFields()
        {
            for (int i = 1; i < _fields.Length / Rows - 1; i++)
            {
                for (int j = 1; j < _fields.Length / Columns - 2; j++)
                {
                    if (_fields[i, j] == _fields[i + 1, j] && _fields[i, j] != 0)
                        TagEqualsFieldsInNewFields(i, j, i + 1, j);

                    if (_fields[i, j] == _fields[i - 1, j] && _fields[i, j] != 0)
                        TagEqualsFieldsInNewFields(i, j, i - 1, j);

                    if (_fields[i, j] == _fields[i, j + 1] && _fields[i, j] != 0)
                        TagEqualsFieldsInNewFields(i, j, i, j + 1);

                    if (_fields[i, j] == _fields[i, j - 1] && _fields[i, j] != 0)
                        TagEqualsFieldsInNewFields(i, j, i, j - 1);

                    if (_fields[i, j] == _fields[i + 1, j + 1] && _fields[i, j] != 0)
                        TagEqualsFieldsInNewFields(i, j, i + 1, j + 1);

                    if (_fields[i, j] == _fields[i + 1, j - 1] && _fields[i, j] != 0)
                        TagEqualsFieldsInNewFields(i, j, i + 1, j - 1);

                    if (_fields[i, j] == _fields[i - 1, j + 1] && _fields[i, j] != 0)
                        TagEqualsFieldsInNewFields(i, j, i - 1, j + 1);

                    if (_fields[i, j] == _fields[i - 1, j - 1] && _fields[i, j] != 0)
                        TagEqualsFieldsInNewFields(i, j, i - 1, j - 1);
                }
            }
        }

        /// <summary>
        /// Запуск нового блока
        /// </summary>
        public void StartRunBlock()
        {
            Column = 4;
            _fields[Column, _fields.Length / Columns - 3] = NewBlock[0];
            _fields[Column, _fields.Length / Columns - 2] = NewBlock[1];
            _fields[Column, _fields.Length / Columns - 1] = NewBlock[2];
            StartNewBlockAllow = false;
        }

        /// <summary>
        /// Набор количества очков
        /// </summary>
        public void Scoring()
        {
            for (int i = 1; i < _fields.Length / Rows - 1; i++)
            {
                for (int j = 1; j < _fields.Length / Columns - 1; j++)
                {
                    if (NewFields[i, j] == 9)
                    {
                        Score++;
                    }
                }
            }
        }

        /// <summary>
        /// Пометка блоков для удаления, включение флага о том, что изменения в массиве были произведены
        /// </summary>
        /// <param name="parFirstI">Координата i первого элемента</param>
        /// <param name="parFirstJ">Координата j первого элемента</param>
        /// <param name="parSecondI">Координата i второго элемента</param>
        /// <param name="parSecondJ">Координата j второго элемента</param>
        private void TagEqualsFieldsInNewFields(int parFirstI, int parFirstJ, int parSecondI, int parSecondJ)
        {
            int thirdI = parSecondI - parFirstI + parSecondI;
            int thirdJ = parSecondJ - parFirstJ + parSecondJ;
            if (_fields[parFirstI, parFirstJ] == _fields[thirdI, thirdJ])
            {
                NewFields[parFirstI, parFirstJ] = 9;
                NewFields[parSecondI, parSecondJ] = 9;
                NewFields[thirdI, thirdJ] = 9;
                IsFieldsUpdated = true;
            }
        }

        /// <summary>
        /// Обновление основного массива из вспомогательного
        /// </summary>
        public void UpdateFieldsFromNewFields()
        {
            for (int i = 1; i < _fields.Length / Rows - 1; i++)
            {
                for (int j = 1; j < _fields.Length / Columns - 2; j++)
                {
                    _fields[i, j] = NewFields[i, j];
                }
            }
        }

        /// <summary>
        /// Обновление вспомогательного массива из основного
        /// </summary>
        public void UpdateNewFieldsFromFields()
        {
            for (int i = 1; i < _fields.Length / Rows - 1; i++)
            {
                for (int j = 1; j < _fields.Length / Columns - 2; j++)
                {
                    NewFields[i, j] = _fields[i, j];
                }
            }
        }

        /// <summary>
        /// Удаление одинаковых блоков из вспомогательного массива
        /// </summary>
        public void DeleteEqualsFieldsInNewFields()
        {
            for (int i = 1; i < _fields.Length / Rows - 1; i++)
            {
                for (int j = 1; j < _fields.Length / Columns - 2; j++)
                {
                    if (NewFields[i, j] == 9)
                    {
                        NewFields[i, j] = NewFields[i, j + 1] + NewFields[i, j];
                        NewFields[i, j + 1] = NewFields[i, j] - NewFields[i, j + 1];
                        NewFields[i, j] = NewFields[i, j] - NewFields[i, j + 1];
                    }
                }
                if (NewFields[i, 14] == 9)
                {
                    NewFields[i, 14] = 0;
                    i -= 1;
                }
            }
        }

        /// <summary>
        /// Движения блока вниз
        /// </summary>
        /// <returns>Движение блока вниз произошло</returns>
        public bool RunBlock()
        {
            int index = 1;

            if (!IsBlockMovementEnd(GetIndexOfLastClearFieldBeforeBlock()))
            {
                index = GetIndexOfLastClearFieldBeforeBlock();
                MoveBlock(index);
                if (_ifButtonPressed)
                {
                    switch (_keyDirection)
                    {
                        case KeyDirection.Down:
                            GoDown();
                            break;
                        case KeyDirection.Up:
                            ChangeBlockColors();
                            break;
                        case KeyDirection.Right:
                            MoveToTheRight();
                            break;
                        case KeyDirection.Left:
                            MoveToTheLeft();
                            break;
                        default:
                            break;
                    }
                    _ifButtonPressed = false;
                }
            }
            return true;
        }

        /// <summary>
        /// Проверка на окончание движения блока
        /// </summary>
        /// <param name="parIndex">Индекс</param>
        /// <returns>Закончено движение или нет</returns>
        public bool IsBlockMovementEnd(int parIndex)
        {
            if (parIndex == 0)
                return true;
            if (_fields[Column, parIndex] != 0)
                return true;
            return false;
        }

        /// <summary>
        /// Получение индекса элемента, находящегося под падающим блоком
        /// </summary>
        /// <returns>Индекс элемента</returns>
        public int GetIndexOfLastClearFieldBeforeBlock()
        {
            int index = 0;
            for (int i = 1; i < _fields.Length / Columns - 2; i++)
            {
                if (_fields[Column, i] == 0 && (i - index == 1 || i - index == i))
                {
                    index = i;
                }
            }

            if (index == 13)
                index = 0;
            return index;
        }


        /// <summary>
        /// Проверка заврешения игры
        /// </summary>
        /// <returns>Игра завершена или нет</returns>
        public bool CheckGameOver()
        {
            for (int i = 1; i < _fields.Length / Columns - 1; i++)
            {
                if (_fields[Column, i] == 0)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Генерация нового блока для падения
        /// </summary>
        public void GenerateBlock()
        {
            List<int> numbers = new List<int>
            {
                1,
                2,
                3
            };
            Random random = new Random();
            NewBlock[0] = random.Next(1, 4);
            NewBlock[1] = random.Next(1, 4);
            NewBlock[2] = random.Next(1, 4);
        }

        /// <summary>
        /// Один шаг блока вниз
        /// </summary>
        /// <param name="parIndex">Индекс</param>
        private void MoveBlock(int parIndex)
        {
            for (int i = parIndex; i < _fields.Length / Columns - 1; i++)
            {
                _fields[Column, i] = _fields[Column, i + 1] + _fields[Column, i];
                _fields[Column, i + 1] = _fields[Column, i] - _fields[Column, i + 1];
                _fields[Column, i] = _fields[Column, i] - _fields[Column, i + 1];
            }
        }

        /// <summary>
        /// Один шаг блока влево
        /// </summary>
        public void MoveToTheLeft()
        {
            if (Column == 1)
            {
                return;
            }
            for (int j = 1; j < Columns - 1; j++)
            {
                for (int i = 1; i < Rows - 2; i++)
                {
                    if (_fields[j, i] != 0 && i != 1 && _fields[j, i - 1] == 0)
                    {
                        _fields[j - 1, i] = _fields[j, i];
                        _fields[j - 1, i + 1] = _fields[j, i + 1];
                        _fields[j - 1, i + 2] = _fields[j, i + 2];
                        _fields[j, i] = 0;
                        _fields[j, i + 1] = 0;
                        _fields[j, i + 2] = 0;
                    }
                }
            }
            Column--;
            ClearCurrentBlock();
        }

        /// <summary>
        /// Один шаг блока вправо
        /// </summary>
        public void MoveToTheRight()
        {

            if (Column == 6)
            {
                return;
            }
            for (int j = 1; j < Columns - 1; j++)
            {
                for (int i = 1; i < Rows - 2; i++)
                {
                    if (_fields[j, i] != 0 && i != 1 && _fields[j, i - 1] == 0)
                    {
                        _fields[j + 1, i] = _fields[j, i];
                        _fields[j + 1, i + 1] = _fields[j, i + 1];
                        _fields[j + 1, i + 2] = _fields[j, i + 2];
                        _fields[j, i] = 0;
                        _fields[j, i + 1] = 0;
                        _fields[j, i + 2] = 0;
                        Column++;
                        ClearCurrentBlock();
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Мгновенное опускание блока вниз
        /// </summary>
        public void GoDown()
        {
            for (int j = 1; j < Columns - 1; j++)
            {
                for (int i = 1; i < Rows - 2; i++)
                {
                    if (_fields[j, i] != 0 && i != 1 && _fields[j, i - 1] == 0)
                    {

                        for (int k = 1; k < Rows - 2; k++)
                        {
                            if (_fields[j, k] == 0)
                            {
                                if (_fields[j, i] != 0)
                                {
                                    _fields[j, k] = _fields[j, i];
                                    _fields[j, i] = 0;
                                }

                                if (_fields[j, i + 1] != 0)
                                {
                                    _fields[j, k + 1] = _fields[j, i + 1];
                                    _fields[j, i + 1] = 0;
                                }

                                if (_fields[j, i + 2] != 0)
                                {
                                    _fields[j, k + 2] = _fields[j, i + 2];
                                    _fields[j, i + 2] = 0;
                                }
                                return;
                            }
                        }

                    }
                }
            }

        }


        /// <summary>
        /// Смена цветов в падающем блоке
        /// </summary>
        public void ChangeBlockColors()
        {
            for (int j = 1; j < Columns - 1; j++)
            {
                for (int i = 1; i < Rows - 2; i++)
                {
                    if (_fields[j, i] != 0 && i != 1 && _fields[j, i - 1] == 0)
                    {
                        int topColor = _fields[j, i + 2];
                        _fields[j, i + 2] = _fields[j, i + 1];
                        _fields[j, i + 1] = _fields[j, i];
                        _fields[j, i] = topColor;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Очистка фрагмента массива после сдвига падающего блока в одну из сторон (вправо, влево)
        /// </summary>
        public void ClearCurrentBlock()
        {
            for (int j = 1; j < Columns - 1; j++)
            {
                for (int i = 1; i < Rows - 2; i++)
                {
                    if (_fields[j, i] != 0 && i != 1)
                    {
                        if (_fields[j, i - 1] == 0 && j != Column)
                        {
                            _fields[j, i] = 0;
                            _fields[j, i + 1] = 0;
                            _fields[j, i + 2] = 0;
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Перемещение
        /// </summary>
        /// <param name="parKeyDirection">Направление нажатия</param>
        public void Move(KeyDirection parKeyDirection)
        {
            lock (_lock)
            {
                _ifButtonPressed = true;
                _keyDirection = parKeyDirection;
            }
        }
    }
}
