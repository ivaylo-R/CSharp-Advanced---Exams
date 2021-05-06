using System;
using System.Collections.Generic;
using System.Linq;

namespace Loot_Box
{
    public class Program
    {

        static void Main()
        {
            int[] firstSeq = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int[] secondSeq = Console.ReadLine().Split().Select(int.Parse).ToArray();
            Queue<int> firstBox = new Queue<int>(firstSeq);
            Stack<int> secondBox = new Stack<int>(secondSeq);
            List<int> claimedItems = new List<int>();
            while (true)
            {
                if (!firstBox.Any())
                {
                    Console.WriteLine($"First lootbox is empty");
                    PrintResult(claimedItems);
                    break;
                }
                if (!secondBox.Any())
                {
                    Console.WriteLine($"Second lootbox is empty");
                    PrintResult(claimedItems);
                    break;
                }
                if ((firstBox.Peek() + secondBox.Peek()) % 2 == 0)
                {
                    claimedItems.Add(firstBox.Dequeue() + secondBox.Pop());
                }
                else
                {
                    firstBox.Enqueue(secondBox.Pop());
                }

            }


        }

        private static void PrintResult(List<int> claimedItems)
        {
            int sum = claimedItems.Sum();
            if (sum>=100)
            {
                Console.WriteLine($"Your loot was epic! Value: {sum}");
            }
            else
            {
                Console.WriteLine($"Your loot was poor... Value: {sum}");
            }
        }
    }
}
