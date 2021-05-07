using System;
using System.Collections.Generic;

namespace Tron_Racers
{

    public class Program
    {
        public class FPlayer
        {
            public FPlayer()
            {
                Row = 0;
                Col = 0;
            }

            public int Row { get; set; } = 0;
            public int Col { get; set; } = 0;

        }

        public class SPlayer
        {
            public SPlayer()
            {
                Row = 0;
                Col = 0;
            }

            public int Row { get; set; } = 0;
            public int Col { get; set; } = 0;
        }
        public class Position
        {
            public Position()
            {
                Fplayer = new FPlayer();
                Splayer = new SPlayer();
            }

            public FPlayer Fplayer { get; set; }

            public SPlayer Splayer { get; set; } 

            public bool isSafe(int row, int col, int n)
            {
                if (row >= 0 && row < n && col >= 0 && col < n)
                    return true;
                return false;

            }
            public void FirstPlayerMoves(string cmd, char[,] matrix)
            {
                matrix[Fplayer.Row, Fplayer.Col] = 'f';
                if (cmd == "up")
                {
                    this.Fplayer.Row--;
                }
                else if (cmd == "down")
                {
                    this.Fplayer.Row++;
                }
                else if (cmd == "right")
                {
                    this.Fplayer.Col++;
                }
                else if (cmd == "left")
                {
                    this.Fplayer.Col--;
                }

            }
            public void SecondPlayerMoves(string cmd, char[,] matrix)
            {
                matrix[Splayer.Row, Splayer.Col] = 's';
                if (cmd == "up")
                {
                    this.Splayer.Row--;
                }
                else if (cmd == "down")
                {
                    this.Splayer.Row++;
                }
                else if (cmd == "right")
                {
                    this.Splayer.Col++;
                }
                else if (cmd == "left")
                {
                    this.Splayer.Col--;
                }

            }

            public void TeleportF(int n)
            {
                if (this.Fplayer.Row == -1)
                {
                    this.Fplayer.Row = n - 1;
                }
                if (this.Fplayer.Col == -1)
                {
                    this.Fplayer.Col = n - 1;
                }
                if (this.Fplayer.Col == n)
                {
                    this.Fplayer.Col = 0;
                }
                if (this.Fplayer.Row == n)
                {
                    this.Fplayer.Row = 0;
                }
            }

            public void TeleportS(int n)
            {
                if (this.Splayer.Row == -1)
                {
                    this.Splayer.Row = n - 1;
                }
                if (this.Splayer.Col == -1)
                {
                    this.Splayer.Col = n - 1;
                }
                if (this.Splayer.Col == n)
                {
                    this.Splayer.Col = 0;
                }
                if (this.Splayer.Row == n)
                {
                    this.Splayer.Row = 0;
                }
            }
        }

        public static bool isEnd = false;
        public static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            char[,] matrix = new char[n, n];
            
            Tuple<Position, Position> positions = ReadMatrix(matrix);
            Position fPosition = positions.Item1;
            Position sPosition = positions.Item2;

            while (true)
            {
                string[] commands = Console.ReadLine().Split();

                string fCommand = commands[0];
                string sCommand = commands[1];
                FirstPlayer(fCommand, matrix, fPosition, n);
                SecondPlayer(sCommand, matrix, sPosition, n);
                if (isEnd == true)
                {
                    return;
                }
            }
        }

        public static void SecondPlayer(string sCommand, char[,] matrix, Position sPosition, int n)
        {
            sPosition.SecondPlayerMoves(sCommand, matrix);
            if (sPosition.isSafe(sPosition.Splayer.Row, sPosition.Splayer.Col, n))
            {
                if (matrix[sPosition.Splayer.Row, sPosition.Splayer.Col] == 'f')
                {
                    matrix[sPosition.Splayer.Row, sPosition.Splayer.Col] = 'x';
                    PrintMatrix(matrix);
                    return;
                }

                matrix[sPosition.Splayer.Row, sPosition.Splayer.Col] = 's';
            }
            else
            {
                sPosition.TeleportS(n);

                if (matrix[sPosition.Splayer.Row, sPosition.Splayer.Col] == 'f')
                {
                    matrix[sPosition.Splayer.Row, sPosition.Splayer.Col] = 'x';
                    PrintMatrix(matrix);
                    return;
                }

                matrix[sPosition.Splayer.Row, sPosition.Splayer.Col] = 's';
            }
        }

        public static void FirstPlayer(string fCommand, char[,] matrix, Position p, int n)
        {

            p.FirstPlayerMoves(fCommand, matrix);
            if (p.isSafe(p.Fplayer.Row, p.Fplayer.Col, n))
            {
                if (matrix[p.Fplayer.Row, p.Fplayer.Col] == 's')
                {
                    matrix[p.Fplayer.Row, p.Fplayer.Col] = 'x';
                    PrintMatrix(matrix);
                    return;
                }
                matrix[p.Fplayer.Row, p.Fplayer.Col] = 'f';
            }
            else
            {
                p.TeleportF(n);

                if (matrix[p.Fplayer.Row, p.Fplayer.Col] == 's')
                {
                    matrix[p.Fplayer.Row, p.Fplayer.Col] = 'x';
                    PrintMatrix(matrix);
                    return;
                }

                matrix[p.Fplayer.Row, p.Fplayer.Col] = 'f';
            }
        }

        public static void PrintMatrix(char[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write(matrix[row, col]);
                }
                Console.WriteLine();
            }
            isEnd = true;
        }

        public static Tuple<Position, Position> ReadMatrix(char[,] matrix)
        {
            Position fPosition = new Position();
            Position sPosition = new Position();
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                var line = Console.ReadLine().ToCharArray();
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = line[col];
                    if (matrix[row, col] == 'f')
                    {
                        fPosition.Fplayer.Row = row;
                        fPosition.Fplayer.Col = col;

                    }
                    if (matrix[row, col] == 's')
                    {
                        sPosition.Splayer.Row = row;
                        sPosition.Splayer.Col = col;
                    }
                }
            }
            return new Tuple<Position, Position>(fPosition, sPosition);
        }

    }
}
