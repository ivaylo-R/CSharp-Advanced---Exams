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
            int flowerBloomLevel = 1;
            SetMatrix(field);
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
                    Flower flower = new Flower(row, col,flowerBloomLevel);
                    flower.Add(flower);
                    flowerBloomLevel++;
                }
                else
                {
                    Console.WriteLine("Invalid coordinates.");
                    continue;
                }

            }

            
        }

        

        private static bool IsSafe(int n, int m, int row, int col)
        {
            return row >= 0 && col >= 0 && row < n && col < m;
        }

        public static void SetMatrix(int [,] field)
        {
            for (int row = 0; row < field.GetLength(0); row++)
            {
                for (int col = 0; col < field.GetLength(1); col++)
                {
                    field[row, col] = 0;
                }
            }
        }

        public static void PrintMatrix(char[,] field)
        {
            for (int row = 0; row < field.GetLength(0); row++)
            {
                for (int col = 0; col < field.GetLength(1); col++)
                {
                    Console.Write(field[row,col]);
                }
                Console.WriteLine();
            }
        }

        public class Position 
        {

        }

        public class Flower
        {
            public Flower(int row, int col,int bloomLvl)
            {
                flowers = new List<Flower>();
                Row = row;
                Col = col;
                BloomLevel = bloomLvl;
            }

            public List<Flower> flowers { get; set; }
            public int Row { get; set; }
            public int Col { get; set; }

            public int BloomLevel { get; set; }

            public void Add(Flower flower)
            {
                flowers.Add(flower);
            }

            public void Bloom(int [,] field)
            {
                for (int row = 0; row < field.GetLength(0); row++)
                {

                }
            }
        }
    }
}
