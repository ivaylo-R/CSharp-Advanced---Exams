using System;
using System.Collections.Generic;
using System.Linq;

namespace Dating_App
{
    class Program
    {
        static void Main()
        {
            var malesInput = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var femalesInput = Console.ReadLine().Split().Select(int.Parse).ToArray();

            Stack<int> males = new Stack<int>(malesInput);
            Queue<int> females = new Queue<int>(femalesInput);
            int matches = 0;
            while (true)
            {
                if (!males.Any())
                {
                    NoMoreMens(ref matches, males, females);
                    return;
                }
                if (!females.Any())
                {
                    NoMoreWomens(ref matches, males, females);
                    return;
                }
                if (females.Any() && males.Any())
                {
                    if (CheckValueForSpecialDivisible(ref matches, males, females))
                    {
                        TryMatch(ref matches, males, females);
                        continue;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        private static bool CheckValueForSpecialDivisible(ref int matches, Stack<int> mens, Queue<int> womens)
        {
            if (mens.Any())
            {
                if (mens.Peek() == 25)
                {
                    mens.Pop();
                    if (mens.Any())
                    {
                        mens.Pop();
                        CheckValueForSpecialDivisible(ref matches, mens, womens);
                        return false;
                    }
                }
            }

            if (womens.Any())
            {
                if (womens.Peek() == 25)
                {
                    womens.Dequeue();
                    if (womens.Any())
                    {
                        womens.Dequeue();
                        CheckValueForSpecialDivisible(ref matches, mens, womens);
                        return false;
                    }
                }
            }

            return true;
        }

        private static void NoMoreWomens(ref int matches, Stack<int> mens, Queue<int> womens)
        {
            Console.WriteLine($"Matches: {matches}");
            if (mens.Any()) Console.WriteLine($"Males left: {String.Join(", ", mens)}");
            else Console.WriteLine($"Males left: none");
            Console.WriteLine($"Females left: none");
            return;
        }

        private static void NoMoreMens(ref int matches, Stack<int> mens, Queue<int> womens)
        {
            Console.WriteLine($"Matches: {matches}");
            Console.WriteLine($"Males left: none");
            if (womens.Any()) Console.WriteLine($"Females left: {String.Join(", ", womens)}");
            else Console.WriteLine($"Females left: none");
            return;
        }

        private static void TryMatch(ref int n, Stack<int> males, Queue<int> females)
        {
            if (CheckValueForZero(males.Peek(), females.Peek(), males, females))
            {
                if (males.Peek() == females.Peek())
                {
                    males.Pop();
                    females.Dequeue();
                    n++;
                }
                else
                {
                    females.Dequeue();
                    males.Push(males.Pop() - 2);
                    CheckValueForZero(males.Peek(), 1, males, females);
                }
            }
            else
            {
                return;
            }
        }

        private static bool CheckValueForZero(int m, int f, Stack<int> males, Queue<int> females)
        {
            if (m <= 0)
            {
                males.Pop();
                return false;
            }
            if (f <= 0)
            {
                females.Dequeue();
                return false;
            }
            return true;
        }
    }
}
