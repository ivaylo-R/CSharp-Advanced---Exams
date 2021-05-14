using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace _01._Bombs
{
    public class Program
    {
        static void Main()
        {

            int[] bombsEffectList = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();
            int[] bombCasingsList = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();
            Queue<int> bombEffects = new Queue<int>(bombsEffectList);
            Stack<int> bombCasings = new Stack<int>(bombCasingsList);
            int daturaBomb = 40;
            int daturaBombCount = 0;
            int cherryBomb = 60;
            int cherryBombCount = 0;
            int smokeBomb = 120;
            int smokeBombCount = 0;
            while (true)
            {
                if (!bombCasings.Any())
                {
                    Console.WriteLine($"You don't have enough materials to fill the bomb pouch.");
                    if (bombEffects.Any())
                    {
                        Console.WriteLine($"Bomb Effects: {string.Join(", ", ForEachQueue(bombEffects))}");
                    }
                    else
                    {
                        Console.WriteLine($"Bomb Effects: empty");
                    }
                    Console.WriteLine($"Bomb Casings: empty");
                    Console.WriteLine($"Cherry Bombs: {cherryBombCount}");
                    Console.WriteLine($"Datura Bombs: {daturaBombCount}");
                    Console.WriteLine($"Smoke Decoy Bombs: {smokeBombCount}");
                    return;
                }
                if (!bombEffects.Any())
                {
                    Console.WriteLine($"You don't have enough materials to fill the bomb pouch.");
                    Console.WriteLine($"Bomb Effects: empty");
                    if (bombCasings.Any())
                    {
                        Console.WriteLine($"Bomb Casings: {string.Join(", ", ForEachStack(bombCasings))}");
                    }
                    else
                    {
                        Console.WriteLine($"Bomb Casings: empty");
                    }
                    Console.WriteLine($"Cherry Bombs: {cherryBombCount}");
                    Console.WriteLine($"Datura Bombs: {daturaBombCount}");
                    Console.WriteLine($"Smoke Decoy Bombs: {smokeBombCount}");
                    return;
                }

                if ((bombEffects.Peek() + bombCasings.Peek()) == daturaBomb)
                {
                    daturaBombCount++;
                    bombEffects.Dequeue();
                    bombCasings.Pop();
                }

                else if ((bombEffects.Peek() + bombCasings.Peek()) == cherryBomb)
                {
                    cherryBombCount++;
                    bombEffects.Dequeue();
                    bombCasings.Pop();
                }

                else if ((bombEffects.Peek() + bombCasings.Peek()) == smokeBomb)
                {
                    smokeBombCount++;
                    bombEffects.Dequeue();
                    bombCasings.Pop();
                }

                else
                {
                    var currentBombCasings = bombCasings.Pop();
                    bombCasings.Push(currentBombCasings - 5);
                }

                if (daturaBombCount >= 3 && smokeBombCount >= 3 && cherryBombCount >= 3)
                {
                    Console.WriteLine($"Bene! You have successfully filled the bomb pouch!");
                    if (bombEffects.Any())
                    {
                        Console.WriteLine($"Bomb Effects: {string.Join(", ", ForEachQueue(bombEffects))}");
                    }
                    else
                    {
                        Console.WriteLine("Bomb Effects: empty");
                    }
                    if (bombCasings.Any())
                    {
                        Console.WriteLine($"Bomb Casings: {string.Join(", ",ForEachStack(bombCasings))}");
                    }
                    else
                    {
                        Console.WriteLine("Bomb Casings: empty");
                    }

                    Console.WriteLine($"Cherry Bombs: {cherryBombCount}");
                    Console.WriteLine($"Datura Bombs: {daturaBombCount}");
                    Console.WriteLine($"Smoke Decoy Bombs: {smokeBombCount}");
                    return;
                }

            }

        }
        public static List<int> ForEachStack(Stack<int> stack)
        {
            List<int> list = new List<int>();
            while (stack.Any())
            {
                list.Add(stack.Pop());
            }
            return list;
        }
        public static List<int> ForEachQueue(Queue<int> queue)
        {
            List<int> list = new List<int>();
            while (queue.Any())
            {
                list.Add(queue.Dequeue());
            }
            return list;
        }
    }
}
