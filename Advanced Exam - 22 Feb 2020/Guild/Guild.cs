using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Guild
{
    public class Guild
    {
        private List<Player> Roster { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }

        public int Count{get{ return Roster.Count; } }

        public Guild(string name, int capacity)
        {
            this.Roster = new List<Player>();
            this.Name = name;
            this.Capacity = capacity;
        }

        public void AddPlayer(Player player)
        {
            if (this.Capacity > this.Roster.Count)
            {
                Roster.Add(player);
            }
        }

        public bool RemovePlayer(string name)
        {
            var player = this.Roster.FirstOrDefault(p => p.Name == name);
            if (player == null) return false;
            else Roster.Remove(player);
            return true;
        }

        public void PromotePlayer(string name)
        {
            var p = Roster.FirstOrDefault(p => p.Name == name);
            if (p.Rank != "Member")
            {
                p.Rank = "Member";
            }
        }

        public void DemotePlayer(string name)
        {
            var p = Roster.FirstOrDefault(p => p.Name == name);
            if (p.Rank != "Trial")
            {
                p.Rank = "Trial";
            }
        }

        public Player[] KickPlayersByClass(string class1)
        {
            Player [] playerz = Roster.Where(x=>x.Class==class1).ToArray();
            this.Roster = Roster.Where(x=>x.Class!=class1).ToList();
            return playerz;
        }

        public string Report()
        {
            var report = new StringBuilder();
            report.AppendLine($"Players in the guild: {this.Name}");

            foreach (var player in Roster)
            {
                report.AppendLine(player.ToString());
            }

            return report.ToString();
        }
    }
}
