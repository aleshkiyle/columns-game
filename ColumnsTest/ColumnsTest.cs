using Columns.Game;
using System.Diagnostics;
using System.Xml.Linq;

namespace ColumnsTest
{
    /// <summary>
    /// Тесты для игры
    /// </summary>
    [TestClass]
    public class ColumnsTest
    {

        /// <summary>
        /// Инициализация игры
        /// </summary>
        /// <returns></returns>
        private GameField InitColumns()
        {
            GameField gameField = new GameField();
            gameField.OnGameOver += () => { };
            gameField.OnRedraw += () => { };
            return gameField;
        }


        /// <summary>
        /// Проверка флага окончания игры на false при инициализации игры
        /// </summary>
        [TestMethod]
        public void TestInitGameAndFalseFlafIsGameEnd()
        {
            GameField gameField = InitColumns();
            Assert.IsFalse(gameField.IsGameEnd);
        }

        /// <summary>
        /// Проверка флага обновления полей на true при инициализации игры
        /// </summary>
        [TestMethod]
        public void TestInitGameAndTrueFlagIsFieldsUpdated()
        {
            GameField gameField = InitColumns();
            Assert.IsTrue(gameField.IsFieldsUpdated);
        }

        /// <summary>
        /// Проверка флага на завершение игры на false при старте игры
        /// </summary>
        [TestMethod]
        public void TestStartGameAndFlagIsGameEndFalse()
        {
            GameField gameField = InitColumns();
            gameField.Start();
            Assert.AreEqual(false, gameField.IsGameEnd);
        }

        /// <summary>
        /// Проверка флага для запуска нового блока на несоотвествие true при запуске нового блока
        /// </summary>
        [TestMethod]
        public void TestStartRunBlokcGameAndNotEqualFlagStartNewBlockAllow()
        {
            GameField gameField = InitColumns();
            gameField.StartRunBlock();
            Assert.AreNotEqual(true, gameField.StartNewBlockAllow);
        }

        /// <summary>
        /// Проверка флага окончания игры на несоответствие true при инициализации игры
        /// </summary>
        [TestMethod]
        public void TestFlagIsGameEndAreNotEqual()
        {
            GameField gameField = InitColumns();
            Assert.AreNotEqual(true, gameField.IsGameEnd);
        }


        /// <summary>
        /// Проверка количества очков равно шести при наличии в одной строке шести ячеек одинакового цвета
        /// </summary>
        [TestMethod]
        public void TestScoreqWhenBurningSixCellsIsOneColumn()
        {
            GameField gameField = InitColumns();
            int[,] cells = new int[GameField.Columns, GameField.Rows];
            cells[1, 1] = 1;
            cells[1, 2] = 1;
            cells[1, 3] = 1;
            cells[1, 4] = 1;
            cells[1, 5] = 1;
            cells[1, 6] = 1;
            gameField.Fields = cells;
            gameField.DeleteEqualsFields();
            Assert.AreEqual(6, gameField.Score);
        }

        /// <summary>
        /// Проверка на несоответствие количества набранных очков, равное четырём при ячейках в первой строке разного цвета
        /// </summary>
        [TestMethod]
        public void TestAreNotEqualScoreWhenInOneLineFiguresDifferentColors()
        {
            GameField gameField = InitColumns();
            int[,] cells = new int[GameField.Columns, GameField.Rows];
            cells[1, 1] = 1;
            cells[1, 2] = 2;
            cells[1, 3] = 3;
            cells[1, 4] = 1;
            cells[1, 5] = 1;
            cells[1, 6] = 1;
            gameField.Fields = cells;
            gameField.DeleteEqualsFields();
            Assert.AreNotEqual(4, gameField.Score);
        }

        /// <summary>
        /// Проверка на сгорание ячеек из трех строк разных цветов, но попарно одинаковых и набираение количества девяти очков
        /// </summary>
        [TestMethod]
        public void TestCheckBurningCellFirstWayAndGetingNineScore()
        {
            GameField gameField = InitColumns();
            int[,] cells = new int[GameField.Columns, GameField.Rows];
            cells[1, 1] = 1;
            cells[1, 2] = 2;
            cells[1, 3] = 3;
            cells[2, 1] = 1;
            cells[2, 2] = 2;
            cells[2, 3] = 3;
            cells[3, 1] = 1;
            cells[3, 2] = 2;
            cells[3, 3] = 3;
            gameField.Fields = cells;
            gameField.DeleteEqualsFields();
            Assert.AreEqual(9, gameField.Score);
        }

        /// <summary>
        /// Проверка на сгорание ячеек из трех строк одинакового цвета и набираение количества девяти очков
        /// </summary>
        [TestMethod]
        public void TestCheckBurningSecondWayAndGetingNineScore()
        {
            GameField gameField = InitColumns();
            int[,] cells = new int[GameField.Columns, GameField.Rows];
            cells[1, 1] = 1;
            cells[1, 2] = 1;
            cells[1, 3] = 1;
            cells[2, 1] = 1;
            cells[2, 2] = 1;
            cells[2, 3] = 1;
            cells[3, 1] = 1;
            cells[3, 2] = 1;
            cells[3, 3] = 1;
            gameField.Fields = cells;
            gameField.DeleteEqualsFields();
            Assert.AreEqual(9, gameField.Score);
        }


        /// <summary>
        /// Проверка на сгорание двух блоков из трёх ячеек одинакового цвета и получение значений основного поля равных нулю
        /// </summary>
        [TestMethod]
        public void TestCheckerNullValuesInFieldsWhenFirstRowWithBlocksOfTheSameColor()
        {
            GameField gameField = InitColumns();
            int[,] cells = new int[GameField.Columns, GameField.Rows];
            cells[1, 1] = 1;
            cells[1, 2] = 1;
            cells[1, 3] = 1;
            cells[1, 4] = 1;
            cells[1, 5] = 1;
            cells[1, 6] = 1;
            gameField.Fields = cells;
            gameField.DeleteEqualsFields();
            Assert.IsTrue(checkDoubleArrayInNullValues(gameField.Fields, GameField.Columns, GameField.Rows));
        }

        /// <summary>
        /// Проверка сгорания в одной строке ячеек разных цветов
        /// </summary>
        [TestMethod]
        public void TestCheckerNullValuesInFieldsWhenFirstRowWithBlocksOfDifferentColors()
        {
            GameField gameField = InitColumns();
            int[,] cells = new int[GameField.Columns, GameField.Rows];
            cells[1, 1] = 1;
            cells[1, 2] = 2;
            cells[1, 3] = 1;
            cells[1, 4] = 1;
            cells[1, 5] = 3;
            cells[1, 6] = 1;
            gameField.Fields = cells;
            gameField.DeleteEqualsFields();
            Assert.IsFalse(checkDoubleArrayInNullValues(gameField.Fields, GameField.Columns, GameField.Rows));
        }

        /// <summary>
        /// Проверка сгорания блоков из трех рядов и получения значений массива основного поля
        /// </summary>
        [TestMethod]
        public void TestCheckerNullValuesWhenThreeRowsFilledWithCellsOfDifferentColorOnPairs()
        {
            GameField gameField = InitColumns();
            int[,] cells = new int[GameField.Columns, GameField.Rows];
            cells[1, 1] = 1;
            cells[1, 2] = 2;
            cells[1, 3] = 3;
            cells[2, 1] = 1;
            cells[2, 2] = 2;
            cells[2, 3] = 3;
            cells[3, 1] = 1;
            cells[3, 2] = 2;
            cells[3, 3] = 3;
            gameField.Fields = cells;
            gameField.DeleteEqualsFields();
            Assert.IsTrue(checkDoubleArrayInNullValues(gameField.Fields, GameField.Columns, GameField.Rows));
        }

        /// <summary>
        /// Проверка сгорания блоков, стоящих друг под другом разных цветов и получения значений массива основного поля
        /// </summary>
        [TestMethod]
        public void TestCheckerCorrectsDeletingFieldsAndCheckerNullValues()
        {
            GameField gameField = InitColumns();
            int[,] cells = new int[GameField.Columns, GameField.Rows];
            cells[1, 1] = 1;
            cells[1, 2] = 2;
            cells[1, 3] = 3;
            cells[2, 1] = 1;
            cells[2, 2] = 2;
            cells[2, 3] = 3;
            cells[3, 1] = 1;
            cells[3, 2] = 2;
            cells[3, 3] = 3;
            gameField.Fields = cells;
            gameField.DeleteEqualsFields();
            Assert.IsTrue(checkDoubleArrayInNullValues(gameField.Fields, GameField.Columns, GameField.Rows));
        }

        /// <summary>
        /// Проверка корректного перемещения блока влева и изменения значения его колонки движения
        /// </summary>
        [TestMethod]
        public void TestCheckerCorrectMethodMoveToTheLeft()
        {
            GameField gameField = InitColumns();
            gameField.MoveToTheLeft();
            Assert.AreEqual(3, gameField.Column);
        }

        /// <summary>
        /// Проверка правильности работы метода обновления основного массива из вспомогательного
        /// </summary>
        [TestMethod]
        public void TestCheckerTheCorrectnessMethodUpdateNFieldsFromNewFields()
        {
            GameField gameField = InitColumns();
            int[,] cells = new int[GameField.Columns, GameField.Rows];
            cells[1, 1] = 1;
            cells[1, 2] = 2;
            cells[1, 3] = 3;
            gameField.Fields = cells;
            gameField.UpdateFieldsFromNewFields();
            Assert.AreEqual(gameField.Fields.ToString(), gameField.NewFields.ToString());
        }


        /// <summary>
        /// Проверка правильности работы метода обновления вспомогательного массива из основного
        /// </summary>
        [TestMethod]
        public void TestCheckerTheCorrectnessMethodUpdateNewFieldsFromFields()
        {
            GameField gameField = InitColumns();
            int[,] cells = new int[GameField.Columns, GameField.Rows];
            cells[1, 1] = 3;
            cells[1, 2] = 2;
            cells[1, 3] = 1;
            gameField.NewFields = cells;
            gameField.UpdateNewFieldsFromFields();
            Assert.AreEqual(gameField.NewFields.ToString(), gameField.Fields.ToString());
        }

        /// <summary>
        /// Проверка изменения вспомогательного массива и удаления элементов на соответствие значений вспомогательного массива нулю
        /// </summary>
        [TestMethod]
        public void TestCheckerSeveralNewFiledsAndCheckingTheirValuesAreEqualNull()
        {
            GameField gameField = InitColumns();
            int[,] cells = new int[GameField.Columns, GameField.Rows];
            cells[1, 1] = 3;
            cells[1, 2] = 2;
            cells[1, 3] = 1;
            gameField.NewFields = cells;
            int[,] cells1 = new int[GameField.Columns, GameField.Rows];
            cells[2, 1] = 3;
            cells[2, 2] = 2;
            cells[2, 3] = 1;
            gameField.NewFields = cells1;
            int[,] cells2 = new int[GameField.Columns, GameField.Rows];
            cells[3, 1] = 3;
            cells[3, 2] = 2;
            cells[3, 3] = 1;
            gameField.NewFields = cells2;
            gameField.DeleteEqualsFieldsInNewFields();
            Assert.AreEqual(true, checkDoubleArrayInNullValues(gameField.NewFields, GameField.Columns, GameField.Rows));
        }

        /// <summary>
        /// Проверка генерации блока и соответствие значению false метода проверки массива на нулевые значения
        /// </summary>
        [TestMethod]
        public void TestGenerateBlocAndNotNullValuesNewBlockGeneration()
        {
            GameField gameField = InitColumns();
            gameField.GenerateBlock();
            Assert.IsFalse(checkArrayInNullValues(gameField.NewBlock));
        }

        /// <summary>
        /// Проверка корректности изменения флага для нажатия кнопки при изменения движения влево
        /// </summary>

        [TestMethod]
        public void TestCorrectFlagIfButtonPressedAtKeyDirectionLeft()
        {
            GameField gameField = InitColumns();
            gameField.Move(KeyDirection.Left);
            Assert.IsTrue(gameField.IfButtonPressed);
        }


        /// <summary>
        /// Проверка метода перемещения вправо и соответствия изменения свойства по направлению нажатия
        /// </summary>
        [TestMethod]
        public void TestMoveCorrectDirectionRight()
        {
            GameField gameField = InitColumns();
            gameField.Move(KeyDirection.Right);
            Assert.AreEqual(KeyDirection.Right, gameField.Key);
        }

        /// <summary>
        /// Проверка всех значений значений массива на равенство нулю
        /// </summary>
        /// <param name="parNewBlock">Новый блок</param>
        /// <returns>Заполнен ли массив нулями либо нет</returns>
        private bool checkArrayInNullValues(int[] parNewBlock)
        {
            for (int i = 0; i < parNewBlock.Length; i++)
            {
                if (parNewBlock[i] == 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Проверка всех значений двухмерного массива на равенство нулю
        /// </summary>
        /// <param name="parArray">Входной массив</param>
        /// <param name="parColumns">Столбцы</param>
        /// <param name="parRows">Ряды</param>
        /// <returns>Заполнен ли двухмерный массив нулями либо нет</returns>
        private bool checkDoubleArrayInNullValues
            (int[,] parArray, int parColumns, int parRows)
        {
            for (int i = 0; i < parArray.Length / parRows; i++)
            {
                for (int j = 0; j < parArray.Length / parColumns; j++)
                {
                    if (parArray[i, j] != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}