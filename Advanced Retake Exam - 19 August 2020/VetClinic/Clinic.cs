using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VetClinic
{
    public class Clinic
    {
        private List<Pet> Pets;
        public int Capacity { get; set; }

        public int Count { get { return Counter(); } }
        public Clinic( int capacity)
        {
            this.Pets = new List<Pet> ();
            this.Capacity = capacity;
        }
        

        public void Add(Pet pet)
        {
            if (this.Capacity > Pets.Count)
            {
                Pets.Add(pet);
            }
        }

        public bool Remove(string name)
        {
            Pet pet = Pets.Where(x => x.Name == name).FirstOrDefault();
            if (pet == null)
            {
                return false;
            }
            Pets.Remove(pet);
            return true;
        }

        public Pet GetPet(string name, string owner)
        {
            Pet pet = Pets.Where(x => x.Name == name).Where(x => x.Owner == owner).FirstOrDefault();
            return pet;
        }

        public Pet GetOldestPet()
        {
            return Pets.OrderByDescending(x => x.Age).FirstOrDefault();
        }

        public int Counter() => Pets.Count();

        public string GetStatistics()
        {
            var sb = new StringBuilder();
            sb.AppendLine("The clinic has the following patients:");
            foreach (var pet in Pets)
            {
                sb.AppendLine(pet.ToString());
            }
            return sb.ToString();
        }
    }
}
