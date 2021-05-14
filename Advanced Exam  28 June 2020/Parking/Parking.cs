using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parking
{
    public class Parking
    {
        public List<Car> Data { get; set; } = new List<Car>();

        public string Type { get; set; }
        public int Capacity { get; set; }

        public int Count { get { return Counter(); } }

        public Parking(string type, int capacity)
        {
            this.Type = type;
            this.Capacity = capacity;
        }

        public void Add(Car car)
        {
            if (Count < this.Capacity)
            {
                this.Data.Add(car);
            }
        }
        public bool Remove(string manufacturer, string model)
        {
            foreach (var car in Data)
            {
                if (car.Manufacturer == manufacturer && car.Model == model)
                {
                    this.Data.Remove(car);
                    return true;
                }
            }
            return false;
        }

        public Car GetLatestCar()
        {
            if (this.Data.Count == 0)
            {
                return null;
            }
            return Data.OrderByDescending(x => x.Year).First();
        }

        public Car GetCar(string manufacturer, string model)
        {
            if (this.Data.Count == 0)
            {
                return null;
            }
            return Data.Where(x => x.Manufacturer == manufacturer).Where(x => x.Model == model).FirstOrDefault();
        }

        public int Counter()
        {
            return this.Data.Count;
        }

        public string GetStatistics()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"The cars are parked in {Type}:");
            foreach (var car in Data)
            {
                sb.AppendLine(car.ToString());
            }
            return sb.ToString();
        }
    }
}
