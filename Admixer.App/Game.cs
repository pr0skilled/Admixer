namespace Admixer.App
{
    public class Game
    {
        public int[,] Matrix { get; private set; }
        public int[,] Indexes { get; private set; }
        public int RowsCount { get; private set; }
        public int ColumnsCount { get; private set; }
        public bool IsOver { get; private set; }
        private static readonly Random s_random = new();

        public Game(int[,] matrix)
        {
            Matrix = matrix;
            RowsCount = matrix.GetLength(0);
            ColumnsCount = matrix.GetLength(1);
            Indexes = new int[RowsCount, ColumnsCount];
        }

        public Game(int rows, int columns)
        {
            Initialize(rows, columns);
            RowsCount = rows;
            ColumnsCount = columns;
        }

        public void Update()
        {
            GetIndexesToDelete();
            LogMatrices();
            if (IsOver)
            {
                using StreamWriter file = new("../../../logs.txt", append: true);
                file.WriteLine("End of game. Time is " + DateTime.Now.ToString());
                return;
            }
            for (int i = 0; i < RowsCount; i++)
            {
                for (int j = 0; j < ColumnsCount; j++)
                {
                    if (Indexes[i, j] == 7)
                    {
                        for (int k = 0; k < i; k++)
                        {
                            if (i == 0)
                                break;
                            else
                            {
                                Matrix[i - k, j] = Matrix[i - k - 1, j];
                            }
                        }
                        Matrix[0, j] = s_random.Next(0, 4);
                    }
                }
            }
        }

        public static void DisplayMatrix(int[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                Console.Write("|");
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.ForegroundColor = arr[i, j] switch
                    {
                        0 => ConsoleColor.Red,
                        1 => ConsoleColor.Green,
                        2 => ConsoleColor.Blue,
                        3 => ConsoleColor.Yellow,
                        _ => ConsoleColor.DarkMagenta,
                    };
                    Console.Write(arr[i, j]);
                    Console.ResetColor();
                    Console.Write("|");
                }
                Console.WriteLine();
            }
            Console.WriteLine(new string('-', arr.GetLength(0) + arr.GetLength(1) + 1));
        }

        private void GetIndexesToDelete()
        {
            Array.Clear(Indexes, 0, Indexes.Length);
            int elementsToDelCount = 0;
            for (int i = 0; i < RowsCount; i++)
            {
                int current = Matrix[i, 0];
                int counter = 1;
                for (int j = 1; j < ColumnsCount; j++)
                {
                    if (current == Matrix[i, j])
                    {
                        if (++counter >= 3)
                        {
                            for (int k = 0; k < counter; k++)
                            {
                                Indexes[i, j - k] = 7;
                                elementsToDelCount++;
                            }
                        }
                    }
                    else
                    {
                        current = Matrix[i, j];
                        counter = 1;
                    }
                }
            }
            for (int i = 0; i < RowsCount; i++)
            {
                int current = Matrix[0, i];
                int counter = 1;
                for (int j = 1; j < ColumnsCount; j++)
                {
                    if (current == Matrix[j, i])
                    {
                        if (++counter >= 3)
                        {
                            for (int k = 0; k < counter; k++)
                            {
                                Indexes[j - k, i] = 7;
                                elementsToDelCount++;
                            }
                        }
                    }
                    else
                    {
                        current = Matrix[j, i];
                        counter = 1;
                    }
                }
            }
            if (elementsToDelCount == 0)
                IsOver = true;
        }

        private void Initialize(int rows, int columns)
        {
            if (rows < 3 || columns < 3)
                throw new ArgumentException("Matrix should be at least 3x3");
            Matrix = new int[rows, columns];
            Indexes = new int[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Matrix[i, j] = s_random.Next(0, 4);
                }
            }
        }

        private void LogMatrices()
        {
            using StreamWriter file = new("../../../logs.txt", append: true);
            file.WriteLine(new string('-', Indexes.GetLength(0) + Indexes.GetLength(1) + 1));
            file.WriteLine("Matrix");
            for (int i = 0; i < RowsCount; i++)
            {
                for (int j = 0; j < ColumnsCount; j++)
                {
                    file.Write(Matrix[i, j] + "|");
                }
                file.WriteLine();
            }
            file.WriteLine(new string('-', Matrix.GetLength(0) + Matrix.GetLength(1) + 1));

            file.WriteLine("Indexes");
            for (int i = 0; i < RowsCount; i++)
            {
                for (int j = 0; j < ColumnsCount; j++)
                {
                    file.Write(Indexes[i, j] + "|");
                }
                file.WriteLine();
            }
            file.WriteLine(new string('-', Indexes.GetLength(0) + Indexes.GetLength(1) + 1));
        }
    }
}