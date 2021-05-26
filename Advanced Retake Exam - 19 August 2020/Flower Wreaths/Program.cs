using System;
using System.Collections.Generic;
using System.Linq;

namespace Flower_Wreaths
{
    public class Program
    {
        static void Main()
        {
            int[] liliesInfo = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToArray();
            int[] rosesInfo = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToArray();
            Stack<int> lilies = new Stack<int>(liliesInfo);
            Queue<int> roses = new Queue<int>(rosesInfo);
            int wreath = 0;
            List<int> warehouse = new List<int>();
            while (true)
            {
                if (!CheckLiliesExist(lilies))
                {
                    if (warehouse.Sum() >= 15)
                    {
                        wreath += warehouse.Sum() / 15;
                        if (wreath < 5)
                        {
                            Console.WriteLine($"You didn't make it, you need {5 - wreath} wreaths more!");
                            return;
                        }
                        else
                        {
                            Console.WriteLine($"You made it, you are going to the competition with {wreath} wreaths!");
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"You didn't make it, you need {5 - wreath} wreaths more!");
                        return;
                    }
                }
                if (!CheckRosesExist(roses))
                {
                    if (warehouse.Sum() >= 15)
                    {
                        wreath += warehouse.Sum() / 15;
                        if (wreath < 5)
                        {
                            Console.WriteLine($"You didn't make it, you need {5 - wreath} wreaths more!");
                            return;
                        }
                        else
                        {
                            Console.WriteLine($"You made it, you are going to the competition with {wreath} wreaths!");
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"You didn't make it, you need {5 - wreath} wreaths more!");
                        return;
                    }
                }
                int currSum = FindSum(lilies, roses);
                if (currSum == 15)
                {
                    wreath++;
                    if (wreath == 5)
                    {
                        Console.WriteLine($"You made it, you are going to the competition with {wreath} wreaths!");
                        return;
                    }
                    RemoveFlowers(lilies, roses);
                }
                else if (currSum > 15)
                {

                    while (currSum >= 15)
                    {
                        DecreaseLilies(lilies, ref currSum);
                        if (currSum == 15)
                        {
                            wreath++;
                            if (wreath == 5)
                            {
                                Console.WriteLine($"You made it, you are going to the competition with {wreath} wreaths!");
                                return;
                            }
                            RemoveFlowers(lilies, roses);
                            break;
                        }
                        else if (currSum < 15)
                        {
                            warehouse.Add(currSum);
                            RemoveFlowers(lilies, roses);
                            break;
                        }
                    }
                }
                else
                {
                    warehouse.Add(currSum);
                    RemoveFlowers(lilies, roses);
                }

            }
        }

        public static bool CheckLiliesExist(Stack<int> stack) => stack.Any();
        public static bool CheckRosesExist(Queue<int> queue) => queue.Any();
        private static void DecreaseLilies(Stack<int> lilies, ref int sum)
        {
            var currentLilie = lilies.Pop();
            lilies.Push(currentLilie - 2);
            sum -= 2;
        }

        private static void RemoveFlowers(Stack<int> lilies, Queue<int> roses)
        {
            lilies.Pop(); roses.Dequeue();
        }

        private static int FindSum(Stack<int> lilies, Queue<int> roses) =>
        lilies.Peek() + roses.Peek();

    }
}
