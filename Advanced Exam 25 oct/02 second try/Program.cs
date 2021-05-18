using System;
using System.Collections.Generic;
using System.Linq;

namespace _02
{
    public class Program
    {

        static void Main()
        {
            int[] input = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int n = input[0];
            int m = input[1];
            int[,] field = new int[n, m];
            SetMatrix(field);
            List<Flower> flowers = new List<Flower>();
            while (true)
            {
                string[] flowerInfo = Console.ReadLine().Split();
                if (flowerInfo.Length == 3 && flowerInfo[0] == "Bloom")
                {
                    break;
                }
                int row = int.Parse(flowerInfo[0]);
                int col = int.Parse(flowerInfo[1]);
                if (IsSafe(n, m, row, col))
                {
                    Flower flower = new Flower(row, col);
                    flowers.Add(flower);
                }
                else
                {
                    Console.WriteLine("Invalid coordinates.");
                    continue;
                }

            }

            for (int i = 0; i < flowers.Count; i++)
            {
                Flower flower = flowers[i];
                Bloom(field, flower);
            }
            PrintMatrix(field);
        }

        public static void Bloom(int[,] field, Flower flower)
        {
            BloomUp(field, flower);
            BloomDown(field, flower);
            BloomRight(field, flower);
            BloomLeft(field, flower);
        }

        private static void BloomLeft(int[,] field, Flower flower)
        {
            int moveLeftIndex = flower.Col - 1;
            while (moveLeftIndex >=0)
            {
                field[flower.Row, moveLeftIndex]++;
                moveLeftIndex--;
            }
        }

        private static void BloomRight(int[,] field, Flower flower)
        {
            int moveRightIndex = flower.Col+1;
            while (moveRightIndex<=field.GetLength(1)-1)
            {
                field[flower.Row, moveRightIndex]++;
                moveRightIndex++;
            }
        }

        private static void BloomDown(int[,] field, Flower flower)
        {
            int moveDownIndex = flower.Row+1;
            while (moveDownIndex <= field.GetLength(0)-1)
            {
                field[moveDownIndex, flower.Col]++;
                moveDownIndex++;
            }
        }

        private static void BloomUp(int[,] field, Flower flower)
        {
            int moveUpIndex = flower.Row;
            while (moveUpIndex>=0)
            {
                field[moveUpIndex, flower.Col]++;
                moveUpIndex--;
            }
        }

        private static bool IsSafe(int n, int m, int row, int col)
        {
            return row >= 0 && col >= 0 && row < n && col < m;
        }

        public static void SetMatrix(int[,] field)
        {
            for (int row = 0; row < field.GetLength(0); row++)
            {
                for (int col = 0; col < field.GetLength(1); col++)
                {
                    field[row, col] = 0;
                }
            }
        }

        public static void PrintMatrix(int[,] field)
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

        public class Flower
        {
            public Flower(int row, int col)
            {
                Row = row;
                Col = col;
            }

            public int Row { get; set; }
            public int Col { get; set; }
        }
    }
}