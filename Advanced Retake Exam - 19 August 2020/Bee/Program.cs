using System;
using System.Collections.Generic;

namespace Bee
{
    class Program
    {
        static void Main()
        {
            int size = int.Parse(Console.ReadLine());
            char[,] field = new char[size, size];
            List<int> beePosition = new List<int>();
            int flowers = 0;
            ReadField(field, ref beePosition);
            int currBeeRow = beePosition[0];
            int currBeeCol = beePosition[1];
            while (true)
            {
                string command = Console.ReadLine();
                if (command == "End")
                {
                    EndProgram(flowers, field);
                    return;
                }
                MoveNext(field, command, ref currBeeRow, ref currBeeCol);
                if (CheckFieldBorder(field, currBeeRow, currBeeCol))
                {
                    if (field[currBeeRow, currBeeCol] == 'f')
                    {
                        IncreaseFlowers(ref flowers, field, ref currBeeRow, ref currBeeCol);
                        continue;
                    }
                    else if (field[currBeeRow, currBeeCol] == 'O')
                    {
                        MoveNext(field, command, ref currBeeRow, ref currBeeCol);
                        if (field[currBeeRow, currBeeCol] == 'f')
                        {
                            IncreaseFlowers(ref flowers, field, ref currBeeRow, ref currBeeCol);
                            continue;
                        }
                        else if (field[currBeeRow, currBeeCol] == '.')
                        {
                            field[currBeeRow, currBeeCol] = 'B';
                            continue;
                        }
                    }
                    else if (field[currBeeRow, currBeeCol] == '.')
                    {
                        field[currBeeRow, currBeeCol] = 'B';
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine($"The bee got lost!");
                    EndProgram(flowers, field);
                }

            }
        }

        private static void EndProgram(int flowers, char[,] field)
        {
            if (flowers >= 5) Console.WriteLine($"Great job, the bee managed to pollinate {flowers} flowers!");
            else Console.WriteLine($"The bee couldn't pollinate the flowers, she needed {5 - flowers} flowers more");
            PrintField(field);
        }

        private static void IncreaseFlowers(ref int flowers, char[,] field, ref int row, ref int col)
        {
            field[row, col] = 'B';
            flowers++;
        }

        private static void MoveNext(char[,] field, string command, ref int row, ref int col)
        {
                
            if (CheckFieldBorder(field, row, col)) field[row, col] = '.'; 

            if (command == "up") row--;
            else if (command == "down") row++;
            else if (command == "right") col++;
            else if (command == "left") col--;


        }
        private static bool CheckFieldBorder(char[,] field, int row, int col)
            => row >= 0 && row < field.GetLength(0) && col >= 0 && col < field.GetLength(1);


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
        }

        private static void ReadField(char[,] field, ref List<int> beePosition)
        {
            for (int row = 0; row < field.GetLength(0); row++)
            {
                var line = Console.ReadLine().ToCharArray();
                for (int col = 0; col < field.GetLength(1); col++)
                {
                    field[row, col] = line[col];
                    if (line[col] == 'B')
                    {
                        beePosition.Add(row);
                        beePosition.Add(col);
                    }
                }
            }
        }
    }
}
