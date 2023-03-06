using Columns.Game;
using System.Diagnostics;
using System.Xml.Linq;

namespace ColumnsTest
{
    /// <summary>
    /// ����� ��� ����
    /// </summary>
    [TestClass]
    public class ColumnsTest
    {

        /// <summary>
        /// ������������� ����
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
        /// �������� ����� ��������� ���� �� false ��� ������������� ����
        /// </summary>
        [TestMethod]
        public void TestInitGameAndFalseFlafIsGameEnd()
        {
            GameField gameField = InitColumns();
            Assert.IsFalse(gameField.IsGameEnd);
        }

        /// <summary>
        /// �������� ����� ���������� ����� �� true ��� ������������� ����
        /// </summary>
        [TestMethod]
        public void TestInitGameAndTrueFlagIsFieldsUpdated()
        {
            GameField gameField = InitColumns();
            Assert.IsTrue(gameField.IsFieldsUpdated);
        }

        /// <summary>
        /// �������� ����� �� ���������� ���� �� false ��� ������ ����
        /// </summary>
        [TestMethod]
        public void TestStartGameAndFlagIsGameEndFalse()
        {
            GameField gameField = InitColumns();
            gameField.Start();
            Assert.AreEqual(false, gameField.IsGameEnd);
        }

        /// <summary>
        /// �������� ����� ��� ������� ������ ����� �� ������������� true ��� ������� ������ �����
        /// </summary>
        [TestMethod]
        public void TestStartRunBlokcGameAndNotEqualFlagStartNewBlockAllow()
        {
            GameField gameField = InitColumns();
            gameField.StartRunBlock();
            Assert.AreNotEqual(true, gameField.StartNewBlockAllow);
        }

        /// <summary>
        /// �������� ����� ��������� ���� �� �������������� true ��� ������������� ����
        /// </summary>
        [TestMethod]
        public void TestFlagIsGameEndAreNotEqual()
        {
            GameField gameField = InitColumns();
            Assert.AreNotEqual(true, gameField.IsGameEnd);
        }


        /// <summary>
        /// �������� ���������� ����� ����� ����� ��� ������� � ����� ������ ����� ����� ����������� �����
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
        /// �������� �� �������������� ���������� ��������� �����, ������ ������ ��� ������� � ������ ������ ������� �����
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
        /// �������� �� �������� ����� �� ���� ����� ������ ������, �� ������� ���������� � ���������� ���������� ������ �����
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
        /// �������� �� �������� ����� �� ���� ����� ����������� ����� � ���������� ���������� ������ �����
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
        /// �������� �� �������� ���� ������ �� ��� ����� ����������� ����� � ��������� �������� ��������� ���� ������ ����
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
        /// �������� �������� � ����� ������ ����� ������ ������
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
        /// �������� �������� ������ �� ���� ����� � ��������� �������� ������� ��������� ����
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
        /// �������� �������� ������, ������� ���� ��� ������ ������ ������ � ��������� �������� ������� ��������� ����
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
        /// �������� ����������� ����������� ����� ����� � ��������� �������� ��� ������� ��������
        /// </summary>
        [TestMethod]
        public void TestCheckerCorrectMethodMoveToTheLeft()
        {
            GameField gameField = InitColumns();
            gameField.MoveToTheLeft();
            Assert.AreEqual(3, gameField.Column);
        }

        /// <summary>
        /// �������� ������������ ������ ������ ���������� ��������� ������� �� ����������������
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
        /// �������� ������������ ������ ������ ���������� ���������������� ������� �� ���������
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
        /// �������� ��������� ���������������� ������� � �������� ��������� �� ������������ �������� ���������������� ������� ����
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
        /// �������� ��������� ����� � ������������ �������� false ������ �������� ������� �� ������� ��������
        /// </summary>
        [TestMethod]
        public void TestGenerateBlocAndNotNullValuesNewBlockGeneration()
        {
            GameField gameField = InitColumns();
            gameField.GenerateBlock();
            Assert.IsFalse(checkArrayInNullValues(gameField.NewBlock));
        }

        /// <summary>
        /// �������� ������������ ��������� ����� ��� ������� ������ ��� ��������� �������� �����
        /// </summary>

        [TestMethod]
        public void TestCorrectFlagIfButtonPressedAtKeyDirectionLeft()
        {
            GameField gameField = InitColumns();
            gameField.Move(KeyDirection.Left);
            Assert.IsTrue(gameField.IfButtonPressed);
        }


        /// <summary>
        /// �������� ������ ����������� ������ � ������������ ��������� �������� �� ����������� �������
        /// </summary>
        [TestMethod]
        public void TestMoveCorrectDirectionRight()
        {
            GameField gameField = InitColumns();
            gameField.Move(KeyDirection.Right);
            Assert.AreEqual(KeyDirection.Right, gameField.Key);
        }

        /// <summary>
        /// �������� ���� �������� �������� ������� �� ��������� ����
        /// </summary>
        /// <param name="parNewBlock">����� ����</param>
        /// <returns>�������� �� ������ ������ ���� ���</returns>
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
        /// �������� ���� �������� ����������� ������� �� ��������� ����
        /// </summary>
        /// <param name="parArray">������� ������</param>
        /// <param name="parColumns">�������</param>
        /// <param name="parRows">����</param>
        /// <returns>�������� �� ���������� ������ ������ ���� ���</returns>
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