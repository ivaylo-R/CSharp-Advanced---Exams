using System;
using System.Collections.Generic;
using System.Linq;

namespace Santa_s_Present_Factory
{
    class Program
    {
        static void Main()
        {

            int[] inputMats = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int[] inputMagic = Console.ReadLine().Split().Select(int.Parse).ToArray();

            Stack<int> mats = new Stack<int>(inputMats);
            Queue<int> magic = new Queue<int>(inputMagic);
            var presents = new SortedDictionary<string, int>();
            int dollCount = 0;
            int woodenTrainCount = 0;
            int teddyBearCount = 0;
            int bicycleCount = 0;

            while (true)
            {
                if (!mats.Any())
                {
                    if (teddyBearCount > 0 && bicycleCount > 0)
                    {
                        Console.WriteLine("The presents are crafted! Merry Christmas!");
                        PrintResult(mats, magic, presents);
                        return;
                    }

                    if (dollCount > 0 && woodenTrainCount > 0)
                    {
                        Console.WriteLine("The presents are crafted! Merry Christmas!");
                        PrintResult(mats, magic, presents);
                        return;
                    }
                    NoPresentsForTheChristmas(mats, magic, presents);
                    return;
                }
                if (!magic.Any())
                {
                    if (teddyBearCount > 0 && bicycleCount > 0)
                    {
                        Console.WriteLine("The presents are crafted! Merry Christmas!");
                        PrintResult(mats, magic, presents);
                        return;
                    }

                    if (dollCount > 0 && woodenTrainCount > 0)
                    {
                        Console.WriteLine("The presents are crafted! Merry Christmas!");
                        PrintResult(mats, magic, presents);
                        return;
                    }
                    NoPresentsForTheChristmas(mats, magic, presents);
                    return;
                }

                if (magic.Peek() == 0)
                {
                    magic.Dequeue();
                    continue;
                }

                if (mats.Peek() == 0)
                {
                    mats.Pop();
                    continue;
                }

                if (mats.Any() && magic.Any())
                {
                    int multiply = mats.Peek() * magic.Peek();
                    if (CheckTypeOfPresent(multiply, ref dollCount, ref woodenTrainCount, ref teddyBearCount, ref bicycleCount, presents))
                    {
                        mats.Pop();
                        magic.Dequeue();
                    }

                    else if (multiply > 0)
                    {
                        magic.Dequeue();
                        mats.Push(mats.Pop() + 15);
                    }

                    else if (multiply < 0)
                    {
                        int result = mats.Peek() + magic.Peek();
                        mats.Pop();
                        magic.Dequeue();
                        mats.Push(result);
                    }

                    
                }
            }

        }

        private static void PrintResult(Stack<int> mats, Queue<int> magic, SortedDictionary<string, int> presents)
        {
            if (mats.Count > 0)
            {
                Console.WriteLine($"Materials left: {string.Join(", ", mats)}");
            }
            if (magic.Count > 0)
            {
                Console.WriteLine($"Magic left: {string.Join(", ", magic)}");
            }
            foreach (var present in presents)
            {
                Console.WriteLine($"{present.Key}: {present.Value}");
            }
        }

        private static void NoPresentsForTheChristmas(Stack<int> mats, Queue<int> magic, SortedDictionary<string, int> presents)
        {
            Console.WriteLine($"No presents this Christmas!");
            PrintResult(mats, magic, presents);
        }

        public static bool CheckTypeOfPresent(int magicNeeded, ref int dollCount,
            ref int woodenTrainCount, ref int teddyBearCount, ref int bicycleCount, SortedDictionary<string, int> presents)
        {

            if (magicNeeded == 150)
            {
                dollCount++;
                if (!presents.ContainsKey("Doll"))
                {
                    presents.Add("Doll", 0);
                }
                presents["Doll"]++;
                return true;
            }
            else if (magicNeeded == 250)
            {
                woodenTrainCount++;
                if (!presents.ContainsKey("Wooden train"))
                {
                    presents.Add("Wooden train", 0);
                }
                presents["Wooden train"]++;
                return true;
            }
            else if (magicNeeded == 300)
            {
                teddyBearCount++;
                if (!presents.ContainsKey("Teddy bear"))
                {
                    presents.Add("Teddy bear", 0);
                }
                presents["Teddy bear"]++;
                return true;
            }

            else if (magicNeeded == 400)
            {
                bicycleCount++;
                if (!presents.ContainsKey("Bicycle"))
                {
                    presents.Add("Bicycle", 0);
                }
                presents["Bicycle"]++;
                return true;
            }
            return false;
        }

    }
}
