using System;
using System.Linq;

namespace Present_Delivery
{
    class Program
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

            public bool isSafe(int n)
            {
                if (this.Row >= 0 && this.Row < n && this.Col >= 0 && this.Col < n) return true;
                return false;
            }

            public Position Moves(string cmd, char[,] field)
            {
                field[this.Row, this.Col] = '-';
                if (cmd == "up") this.Row--;
                else if (cmd == "down") this.Row++;
                else if (cmd == "right") this.Col++;
                else if (cmd == "left") this.Col--;
                Position newPosition = new Position(this.Row, this.Col);
                if (isSafe(field.GetLength(0)))
                {
                    return newPosition;
                }
                return null;
            }


        }
        static void Main()
        {
            int presentsCount = int.Parse(Console.ReadLine());
            int currentPresentsCount = presentsCount;
            int n = int.Parse(Console.ReadLine());
            char[,] field = new char[n, n];
            int niceKids = 0;
            Position position = ReadField(field, ref niceKids);
            int currentNiceKids = niceKids;

            while (true)
            {

                string cmd = Console.ReadLine();
                if (cmd == "Christmas morning")
                {
                    PrintField(field);
                    PrintResult(currentNiceKids, niceKids);
                    return;
                }
                if (currentPresentsCount == 0)
                {
                    Console.WriteLine($"Santa ran out of presents!");
                    PrintField(field);
                    PrintResult(currentNiceKids, niceKids);
                    return;
                }
                if (position.Moves(cmd, field) != null)
                {
                    if (field[position.Row, position.Col] == 'V')
                    {
                        FindNiceKid(position.Row, position.Col, field, ref currentNiceKids, ref currentPresentsCount);
                        if (currentPresentsCount == 0)
                        {
                            Console.WriteLine($"Santa ran out of presents!");
                            PrintField(field);
                            PrintResult(currentNiceKids, niceKids);
                            return;
                        }
                    }

                    else if (field[position.Row, position.Col] == 'X')
                    {
                        field[position.Row, position.Col] = 'S';
                        continue;
                    }
                    else if (field[position.Row, position.Col] == 'C')
                    {
                        EatCookie(field, position.Row, position.Col, ref currentNiceKids, ref currentPresentsCount);
                        if (currentPresentsCount == 0)
                        {
                            Console.WriteLine($"Santa ran out of presents!");
                            PrintField(field);
                            PrintResult(currentNiceKids, niceKids);
                            return;
                        }
                    }
                    if (field[position.Row, position.Col] == '-')
                    {
                        field[position.Row, position.Col] = 'S';
                        continue;
                    }
                }

            }

        }

        private static void EatCookie(char[,] field, int row, int col, ref int currentNiceKids, ref int currentPresentsCount)
        {
            Position firstPosition = new Position(row, col);
            int currentRow = firstPosition.Row;
            int currentCol = firstPosition.Col;
            int n = field.GetLength(0);
            Position currPosition = new Position(row, col);
            currPosition.Row--;
            if (currPosition.isSafe(n))
            {
                if (field[currPosition.Row, currPosition.Col] == 'V')
                {
                    FindNiceKid(row, col, field, ref currentNiceKids, ref currentPresentsCount);

                }
                if (field[currPosition.Row, currPosition.Col] == 'X')
                {
                    currentPresentsCount--;
                    if (currentPresentsCount==0)
                    {
                        return;
                    }
                }
                field[currPosition.Row, currPosition.Col] = '-';

            }
            currPosition.Row = currentRow;
            currPosition.Col = currentCol;
            currPosition.Row++;
            if (currPosition.isSafe(n))
            {
                if (field[currPosition.Row, currPosition.Col] == 'V')
                {
                    FindNiceKid(row, col, field, ref currentNiceKids, ref currentPresentsCount);
                }
                if (field[currPosition.Row, currPosition.Col] == 'X')
                {
                    currentPresentsCount--;
                    if (currentPresentsCount == 0)
                    {
                        return;
                    }
                }
                field[currPosition.Row, currPosition.Col] = '-';
            }
            currPosition.Row = currentRow;
            currPosition.Col = currentCol;
            currPosition.Col--;
            if (currPosition.isSafe(n))
            {
                if (field[currPosition.Row, currPosition.Col] == 'V')
                {
                    FindNiceKid(row, col, field, ref currentNiceKids, ref currentPresentsCount);
                }
                if (field[currPosition.Row, currPosition.Col] == 'X')
                {
                    currentPresentsCount--;
                    if (currentPresentsCount == 0)
                    {
                        return;
                    }
                }
                field[currPosition.Row, currPosition.Col] = '-';
            }
            currPosition.Row = currentRow;
            currPosition.Col = currentCol;
            currPosition.Col++;
            if (currPosition.isSafe(n))
            {
                if (field[currPosition.Row, currPosition.Col] == 'V')
                {
                    FindNiceKid(row, col, field, ref currentNiceKids, ref currentPresentsCount);
                }
                if (field[currPosition.Row, currPosition.Col] == 'X')
                {
                    currentPresentsCount--;
                    if (currentPresentsCount == 0)
                    {
                        return;
                    }
                }
                field[currPosition.Row, currPosition.Col] = '-';
            }
            currPosition = firstPosition;
            field[currPosition.Row, currPosition.Col] = 'S';
        }

        private static void FindNiceKid(int row, int col, char[,] field, ref int currentNiceKids, ref int currentPresentsCount)
        {
            field[row, col] = 'S';
            currentNiceKids--;
            currentPresentsCount--;
        }

        private static void PrintResult(int currentNiceKids, int niceKids)
        {
            if (currentNiceKids == 0)
            {
                Console.WriteLine($"Good job, Santa! {niceKids} happy nice kid/s.");
                return;
            }
            else
            {
                Console.WriteLine($"No presents for {currentNiceKids} nice kid/s.");
                return;
            }
        }

        private static void PrintField(char[,] field)
        {
            for (int row = 0; row < field.GetLength(0); row++)
            {
                for (int col = 0; col < field.GetLength(1); col++)
                {
                    Console.Write(field[row, col]+" ");
                }
                Console.WriteLine();
            }
        }

        private static Position ReadField(char[,] neighborhood, ref int niceKCount)
        {

            Position santaPosition = new Position(0, 0);
            for (int row = 0; row < neighborhood.GetLength(0); row++)
            {
                var input = Console.ReadLine().Split().ToArray();
                for (int col = 0; col < neighborhood.GetLength(1); col++)
                {
                    neighborhood[row, col] = char.Parse(input[col]);
                    if (neighborhood[row, col] == 'S')
                    {
                        santaPosition = new Position(row, col);
                    }
                    if (neighborhood[row, col] == 'V')
                    {
                        niceKCount++;
                    }
                }
            }
            return santaPosition;
        }
    }
}
