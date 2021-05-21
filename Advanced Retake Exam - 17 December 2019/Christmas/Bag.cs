using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Christmas
{
    public class Bag
    {
        private List<Present> Data;

        public string Color { get; set; }
        public int Capacity { get; set; }

        public int Count { get { return this.Data.Count; } }

        public Bag(string color, int capacity)
        {
            this.Color = color;
            this.Capacity = capacity;
            this.Data = new List<Present>();
        }

        public void Add(Present present)
        {
            if (this.Capacity > this.Data.Count)
            {
                this.Data.Add(present);
            }
        }

        public bool Remove(string name)
        {
            var present = this.Data.FirstOrDefault(p => p.Name == name);
            if (present != null)
            {
                this.Data.Remove(present);
                return true;
            }
            return false;
        }

        public Present GetHeaviestPresent()
        {
            var heavistPresent = this.Data.OrderByDescending(p => p.Weight).FirstOrDefault();
            if (heavistPresent != null)
            {
                return heavistPresent;
            }
            return null;
        }

        public Present GetPresent(string name)
        {
            Present present = this.Data.FirstOrDefault(p => p.Name == name);
            if (present != null)
            {
                return present;
            }
            return null;
        }

        public string Report()
        {
            var presents = new StringBuilder();
            presents.AppendLine($"{this.Color} bag contains:");
            foreach (var present in this.Data)
            {
                presents.AppendLine(present.ToString());
            }

            return presents.ToString().Trim();
        }

    }
}
