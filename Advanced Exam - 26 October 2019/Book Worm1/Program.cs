using System;
using System.Text;

namespace Book_Worm
{
    public class Program
    {
        public class Position
        {
            public Position(int row, int col)
            {
                Row = row;
                Col = col;
            }

            public int Row { get; set; }
            public int Col { get; set; }



            public void MoveNext(string cmd, char[,] field)
            {
                field[Row, Col] = '-';
                if (cmd == "up")
                {
                    this.Row--;
                }
                else if (cmd == "right")
                {
                    this.Col++;
                }
                else if (cmd == "left")
                {
                    this.Col--;
                }
                else if (cmd == "down")
                {
                    this.Row++;
                }
            }

            public bool isSafe(int row, int col, int n)
            {
                if (row >= 0 && col >= 0 && col < n && row < n) return true;
                return false;
            }

            public void GoesOutOfTheField(ref StringBuilder result, char[,] field, int previousRow, int previousCol)
            {
                result.Remove(result.Length - 1, 1);
                if (isSafe(previousRow, previousCol, field.GetLength(1)))
                {
                    field[previousRow, previousCol] = 'P';
                }
            }
            public void GoesOutOfTheField(char[,] field, int previousRow, int previousCol)
            {
                if (isSafe(previousRow, previousCol, field.GetLength(1)))
                {
                    field[previousRow, previousCol] = 'P';
                }
            }
        }
        static void Main()
        {
            string initial = Console.ReadLine();
            var result = new StringBuilder(initial);
            int n = int.Parse(Console.ReadLine());
            char[,] field = new char[n, n];
            Position player = ReadMatrix(field);

            while (true)
            {
                string command = Console.ReadLine();
                if (command == "end")
                {
                    Console.WriteLine(result.ToString());
                    PrintField(field);
                    return;
                }

                int previousRow = player.Row;
                int previousCol = player.Col;

                if (player.isSafe(player.Row, player.Col, n)) player.MoveNext(command, field);
                if (player.isSafe(player.Row, player.Col, n))
                {
                    if (Char.IsLetter(field[player.Row, player.Col]))
                    {
                        result.Append(field[player.Row, player.Col]);
                    }

                    field[player.Row, player.Col] = 'P';
                }

                else
                {
                    if (result.Length > 0)
                    {
                        if (player.isSafe(previousRow, previousCol, n))
                        {
                            player.GoesOutOfTheField(ref result, field, previousRow, previousCol);
                        }

                    }

                    else
                    {
                        if (player.isSafe(previousRow, previousCol, n))
                        {
                            player.GoesOutOfTheField(field, previousRow, previousCol);
                        }
                    }
                }
            }
        }

        private static void PrintField(char[,] field)
        {
            for (int row = 0; row < field.GetLength(0); row++)
            {
                for (int col = 0; col < field.GetLength(1); col++)
                {
                    Console.Write(field[row, col]);
                }
                Console.WriteLine();
            }
            return;
        }

        private static Position ReadMatrix(char[,] field)
        {
            Position player = new Position(0, 0);
            for (int row = 0; row < field.GetLength(0); row++)
            {
                var line = Console.ReadLine().ToCharArray();
                for (int col = 0; col < field.GetLength(1); col++)
                {
                    field[row, col] = line[col];
                    if (field[row, col] == 'P')
                    {
                        player.Row = row;
                        player.Col = col;
                    }
                }
            }
            return player;
        }
    }
}
