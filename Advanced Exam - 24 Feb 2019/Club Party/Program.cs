using System;
using System.Collections.Generic;
using System.Linq;

namespace Club_Party
{
    public class Program
    {
        public class Hall
        {
            public int Capacity { get; set; }
            public string Name { get; set; }
            public List<int> Reservations { get; set; }
            public Hall()
            {
                this.Reservations = new List<int>();
            }

            public override string ToString()
            {
                return $"{this.Name} -> {string.Join(", ", this.Reservations)}";
            }

        }
        static void Main()
        {
            int capacity = int.Parse(Console.ReadLine());
            string[] input = Console.ReadLine().Split();
            Stack<string> stack = new Stack<string>(input);
            Queue<Hall> halls = new Queue<Hall>();


            while (stack.Any())
            {
                var current = stack.Pop();
                int people;
                if (int.TryParse(current, out people))
                {
                    if (halls.Count==0)
                    {
                        continue;
                    }
                    Hall hall = halls.Peek();

                    if (hall.Capacity-people >= 0)
                    {
                        hall.Capacity -= people;
                        hall.Reservations.Add(people);
                    }
                    else
                    {
                        Console.WriteLine(hall.ToString());
                        halls.Dequeue();
                        if (halls.Count > 0)
                        {
                            Hall newHall = halls.Peek();
                            newHall.Capacity -= people;
                            newHall.Reservations.Add(people);

                        }
                    }
                }
                else
                {
                    halls.Enqueue(new Hall { Capacity = capacity, Name = current });
                }
            }


        }
    }
}
