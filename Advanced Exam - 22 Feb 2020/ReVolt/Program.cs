using System;

namespace ReVolt
{
    public class Program
    {
        public class Position
        {
            public int Row { get; set; }
            public int Col { get; set; }
            public Position(int row, int col)
            {
                this.Row = row;
                this.Col = col;
            }

           
            public bool isSafe(int row, int col, int n)
            {
                if (row < 0 || col < 0)
                {
                    return false;
                }
                if (row >= n || col >= n)
                {
                    return false;
                }
                return true;
            }
            public void MoveNext(string command, int n, char[,] field)
            {
                if (command == "up")
                {

                    if (field[this.Row, this.Col] != 'B' && field[this.Row, this.Col] != 'T')
                    {
                        field[this.Row, this.Col] = '-';
                    }
                    this.Row--;
                    if (!isSafe(this.Row, this.Col, n))
                    {
                        Teleport(this.Row, this.Col, n);
                    }
                    if (field[this.Row,this.Col]=='T')
                    {
                        this.Row++;
                        field[this.Row, this.Col] = 'f';
                    }
                }
                else if (command == "down")
                {
                    if (field[this.Row, this.Col] != 'B' && field[this.Row, this.Col] != 'T')
                    {
                        field[this.Row, this.Col] = '-';
                    }
                    this.Row++;
                    if (!isSafe(this.Row, this.Col, n))
                    {
                        Teleport(this.Row, this.Col, n);
                    }
                    if (field[this.Row, this.Col] == 'T')
                    {
                        this.Row--;
                        field[this.Row, this.Col] = 'f';
                    }
                }
                else if (command == "left")
                {
                    if (field[this.Row, this.Col] != 'B' && field[this.Row, this.Col] != 'T')
                    {
                        field[this.Row, this.Col] = '-';
                    }
                    this.Col--; ;
                    if (!isSafe(this.Row, this.Col, n))
                    {
                        Teleport(this.Row, this.Col, n);
                    }
                    if (field[this.Row, this.Col] == 'T')
                    {
                        this.Col++;
                        field[this.Row, this.Col] = 'f';
                    }
                }
                else if (command == "right")
                {
                    if (field[this.Row, this.Col] != 'B' && field[this.Row, this.Col] != 'T')
                    {
                        field[this.Row, this.Col] = '-';
                    }
                    this.Col++;
                    if (!isSafe(this.Row, this.Col, n))
                    {
                        Teleport(this.Row, this.Col, n);
                    }
                    if (field[this.Row, this.Col] == 'T')
                    {
                        this.Col--;
                        field[this.Row, this.Col] = 'f';
                    }
                }
            }

            public void Teleport(int row, int col, int n)
            {
                if (row == n) this.Row = 0;
                if (row == -1) this.Row = n - 1;
                if (col == n)this.Col = 0;
                if (col == -1)this.Col = n - 1;
            }

        }
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            int commandsCount = int.Parse(Console.ReadLine());
            char[,] field = new char[n, n];
            Position position = ReadMatrix(field);

            for (int i = 0; i < commandsCount; i++)
            {
                string command = Console.ReadLine();
                position.MoveNext(command, n, field);
                if (field[position.Row, position.Col] == 'B')
                {
                    position.MoveNext(command, n, field);
                    field[position.Row, position.Col] = 'f';

                }
                if (field[position.Row, position.Col] == 'T')
                {
                    continue;
                }
                if (field[position.Row, position.Col] == 'F')
                {
                    Console.WriteLine($"Player won!");
                    field[position.Row, position.Col] = 'f';
                    PrintMatrix(field);
                    return;
                }


            }
            Console.WriteLine($"Player lost!");
            field[position.Row, position.Col] = 'f';
            PrintMatrix(field);

        }

        private static void PrintMatrix(char[,] field)
        {
            for (int row = 0; row < field.GetLength(0); row++)
            {
                for (int col = 0; col < field.GetLength(1); col++)
                {
                    Console.Write(field[row, col]);
                }
                Console.WriteLine();
            }
        }

        private static Position ReadMatrix(char[,] field)
        {
            Position position = new Position(-1, -1);
            for (int row = 0; row < field.GetLength(0); row++)
            {
                var currLine = Console.ReadLine().ToCharArray();
                for (int col = 0; col < field.GetLength(1); col++)
                {
                    field[row, col] = currLine[col];
                    if (field[row, col] == 'f')
                    {
                        position = new Position(row, col);
                    }
                }
            }
            return position;
        }
    }
}
