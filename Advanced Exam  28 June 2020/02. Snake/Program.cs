using System;
using System.Linq;
namespace _02._Snake
{
    class Program
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            char[,] field = new char[n, n];
            int currSnakeRow = 0;
            int currSnakeCol = 0;
            int foodQuantity = 0;
            ReadMatrix(field, ref currSnakeRow, ref currSnakeCol);

            while (true)
            {
                string command = Console.ReadLine();

                if (command == "up")
                {
                    if (CheckFieldBorder(currSnakeRow - 1, currSnakeCol, field))
                    {
                        if (field[currSnakeRow - 1, currSnakeCol] == '-')
                        {
                            field[currSnakeRow, currSnakeCol] = '.';
                            currSnakeRow -= 1;
                            field[currSnakeRow, currSnakeCol] = 'S';
                        }
                        else if (field[currSnakeRow - 1, currSnakeCol] == 'B')
                        {
                            field[currSnakeRow - 1, currSnakeCol] = '.';
                            field[currSnakeRow, currSnakeCol] = '.';
                            TeleportSnakeToNextBurrow(field, ref currSnakeRow, ref currSnakeCol);
                        }
                        else if (field[currSnakeRow - 1, currSnakeCol] == '*')
                        {

                            field[currSnakeRow, currSnakeCol] = '.';
                            currSnakeRow -= 1;
                            field[currSnakeRow, currSnakeCol] = 'S';
                            foodQuantity++;
                            if (foodQuantity == 10)
                            {
                                YouWon(field, foodQuantity);
                                return;
                            }
                        }
                    }
                    else
                    {
                        field[currSnakeRow, currSnakeCol] = '.';
                        GameOver(field, foodQuantity);
                    }
                }
                else if (command == "down")
                {
                    if (CheckFieldBorder(currSnakeRow + 1, currSnakeCol, field))
                    {
                        if (field[currSnakeRow + 1, currSnakeCol] == '-')
                        {
                            field[currSnakeRow, currSnakeCol] = '.';
                            currSnakeRow += 1;
                            field[currSnakeRow, currSnakeCol] = 'S';
                        }
                        else if (field[currSnakeRow + 1, currSnakeCol] == 'B')
                        {
                            field[currSnakeRow + 1, currSnakeCol] = '.';
                            field[currSnakeRow, currSnakeCol] = '.';
                            TeleportSnakeToNextBurrow(field, ref currSnakeRow, ref currSnakeCol);
                        }
                        else if (field[currSnakeRow + 1, currSnakeCol] == '*')
                        {
                            field[currSnakeRow, currSnakeCol] = '.';
                            currSnakeRow += 1;
                            field[currSnakeRow, currSnakeCol] = 'S';
                            foodQuantity++;
                            if (foodQuantity == 10)
                            {
                                YouWon(field, foodQuantity);
                                return;
                            }
                        }
                    }
                    else
                    {
                        GameOver(field, foodQuantity);
                        field[currSnakeRow, currSnakeCol] = '.';

                    }
                }
                else if (command == "right")
                {
                    if (CheckFieldBorder(currSnakeRow, currSnakeCol + 1, field))
                    {
                        if (field[currSnakeRow, currSnakeCol + 1] == '-')
                        {
                            field[currSnakeRow, currSnakeCol] = '.';
                            currSnakeCol += 1;
                            field[currSnakeRow, currSnakeCol] = 'S';
                        }
                        else if (field[currSnakeRow, currSnakeCol + 1] == 'B')
                        {
                            field[currSnakeRow, currSnakeCol + 1] = '.';
                            field[currSnakeRow, currSnakeCol] = '.';
                            TeleportSnakeToNextBurrow(field, ref currSnakeRow, ref currSnakeCol);

                        }
                        else if (field[currSnakeRow, currSnakeCol + 1] == '*')
                        {
                            field[currSnakeRow, currSnakeCol] = '.';
                            currSnakeCol += 1;
                            field[currSnakeRow, currSnakeCol] = 'S';
                            foodQuantity++;
                            if (foodQuantity == 10)
                            {
                                YouWon(field, foodQuantity);
                                return;
                            }
                        }
                    }
                    else
                    {
                        GameOver(field, foodQuantity);
                        field[currSnakeRow, currSnakeCol] = '.';

                    }
                }
                else if (command == "left")
                {
                    if (CheckFieldBorder(currSnakeRow, currSnakeCol - 1, field))
                    {
                        if (field[currSnakeRow, currSnakeCol - 1] == '-')
                        {
                            field[currSnakeRow, currSnakeCol] = '.';
                            currSnakeCol -= 1;
                            field[currSnakeRow, currSnakeCol] = 'S';
                        }
                        else if (field[currSnakeRow, currSnakeCol - 1] == 'B')
                        {
                            field[currSnakeRow, currSnakeCol - 1] = '.';
                            field[currSnakeRow, currSnakeCol] = '.';
                            TeleportSnakeToNextBurrow(field, ref currSnakeRow, ref currSnakeCol);
                        }
                        else if (field[currSnakeRow, currSnakeCol - 1] == '*')
                        {
                            field[currSnakeRow, currSnakeCol] = '.';
                            currSnakeCol -= 1;
                            field[currSnakeRow, currSnakeCol] = 'S';
                            foodQuantity++;
                            if (foodQuantity == 10)
                            {
                                YouWon(field, foodQuantity);
                                return;
                            }
                        }
                    }
                    else
                    {
                        GameOver(field, foodQuantity);
                        field[currSnakeRow, currSnakeCol] = '.';
                    }
                }
            }

        }

        private static void YouWon(char[,] field, int foodQuantity)
        {
            Console.WriteLine($"You won! You fed the snake.");
            Console.WriteLine($"Food eaten: {foodQuantity}");
            PrintMatrix(field);
        }

        private static void GameOver(char[,] field, int foodQuantity)
        {
            Console.WriteLine("Game over!");
            Console.WriteLine($"Food eaten: {foodQuantity}");
            PrintMatrix(field);
        }

        private static void TeleportSnakeToNextBurrow(char[,] field, ref int currSnakeRow, ref int currSnakeCol)
        {
            bool isNextBurrowFound = false;
            for (int row = 0; row < field.GetLength(0); row++)
            {
                for (int col = 0; col < field.GetLength(1); col++)
                {
                    if (field[row, col] == 'B')
                    {
                        currSnakeRow = row;
                        currSnakeCol = col;
                        field[currSnakeRow, currSnakeCol] = 'S';
                        isNextBurrowFound = true;
                        break;
                    }
                }
                if (isNextBurrowFound)
                {
                    break;
                }
            }
        }

        private static void ReadMatrix(char[,] field, ref int currSnakeRow, ref int currSnakeCol)
        {
            for (int row = 0; row < field.GetLength(0); row++)
            {
                char[] currentRow = Console.ReadLine().ToCharArray();
                for (int col = 0; col < field.GetLength(1); col++)
                {
                    field[row, col] = (char)currentRow[col];
                    if (currentRow[col] == 'S')
                    {
                        currSnakeRow = row;
                        currSnakeCol = col;
                    }
                }
            }
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

        public static bool CheckFieldBorder(int row, int col, char[,] field)
        {
            return row >= 0 && row < field.GetLength(0) && col >= 0 && col < field.GetLength(1);
        }
    }
}
