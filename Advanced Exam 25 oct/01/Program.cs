using System;
using System.Collections.Generic;
using System.Linq;

namespace _01
{
    public class Program
    {
        static void Main(string[] args)
        {
            int[] tasksInput = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            int[] threadsInput = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            int task = int.Parse(Console.ReadLine());
            Stack<int> tasks = new Stack<int>(tasksInput);
            Queue<int> threads = new Queue<int>(threadsInput);

            while (true)
            {
                if (tasks.Any() && threads.Any())
                {
                    if (tasks.Peek() == task)
                    {
                        Console.WriteLine($"Thread with value {threads.Peek()} killed task {tasks.Peek()}");
                        while (threads.Any())
                        {
                            Console.Write($"{threads.Dequeue()} ");
                        }
                        return;
                    }
                    if (threads.Peek()>=tasks.Peek())
                    {
                        threads.Dequeue();
                        tasks.Pop();
                        continue;
                    }
                    else if (threads.Peek()<tasks.Peek())
                    {
                        if (tasks.Peek()==task)
                        {
                            Console.WriteLine($"Thread with value {threads.Peek()} killed task {tasks.Peek()}");
                            while (threads.Any())
                            {
                                Console.Write($"{threads.Dequeue()} ");
                            }
                            return;
                        }
                        else
                        {
                            threads.Dequeue();
                        }
                        
                    }
                }
            }
        }
    }
}
