using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes
{
    public class HeroRepository
    {
        private List<Hero> data;

        public int Count { get { return data.Count; } }
        public HeroRepository()
        {
            this.data = new List<Hero>();
        }

        public void Add(Hero hero)
        {
            data.Add(hero);
        }

        public void Remove(string name)
        {
            var currentHero = data.FirstOrDefault(x => x.Name == name);
            data.Remove(currentHero);
        }

        public Hero GetHeroWithHighestStrength()
        {
            Hero bestStr = this.data.OrderByDescending(h => h.Item.Strength).FirstOrDefault();
            return bestStr;
        }

        public Hero GetHeroWithHighestAbility()
        {
            Hero hero = this.data.OrderByDescending(x => x.Item.Ability).FirstOrDefault();
            return hero;
        }

        public Hero GetHeroWithHighestIntelligence()
        {
            Hero bestInt = this.data.OrderByDescending(h => h.Item.Intelligence).FirstOrDefault();
            return bestInt;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var hero in this.data)
            {
                sb.AppendLine(hero.ToString());
            }

            return sb.ToString().Trim();
        }
    }
}
