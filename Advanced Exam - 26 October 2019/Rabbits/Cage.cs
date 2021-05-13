using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rabbits
{
    public class Cage
    {
        private List<Rabbit> Data;

        public Cage(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            this.Data = new List<Rabbit>();
        }


        public string Name { get; set; }

        public int Capacity { get; set; }

        public int Count { get { return Data.Count; } }

        public void Add(Rabbit rabbit)
        {
            if (Data.Count < Capacity)
            {
                this.Data.Add(rabbit);
            }
        }

        public bool RemoveRabbit(string name)
        {
            var rabbit = this.Data.FirstOrDefault(r => r.Name == name);
            if (rabbit != null)
            {
                this.Data.Remove(rabbit);
                return true;
            }
            return false;
        }

        public void RemoveSpecies(string species)
        {
            this.Data.RemoveAll(r=>r.Species==species);
        }

        public Rabbit SellRabbit(string name)
        {
            var rabbit = this.Data.FirstOrDefault(r => r.Name == name);
            rabbit.Available = false;
            return rabbit;
        }

        public Rabbit[] SellRabbitsBySpecies(string species)
        {
            Rabbit[] rabbits = this.Data.Where(r => r.Species == species).ToArray();
            for (int i = 0; i < rabbits.Length; i++)
            {
                rabbits[i].Available = false;
            }
            return rabbits;
        }

        public string Report()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Rabbits available at {this.Name}:");
            foreach (var r in this.Data.Where(r => r.Available == true))
            {
                sb.AppendLine(r.ToString());
            }

            return sb.ToString().Trim();
        }


    }
}
