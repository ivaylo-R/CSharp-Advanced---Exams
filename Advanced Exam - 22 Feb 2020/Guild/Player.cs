﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Guild
{
    public class Player
    {
        public string Name { get; set; }
        public string Class { get; set; }

        public string Rank { get; set; } = "Trial";

        public string Description { get; set; } = "n/a";
        public Player(string name, string class1)
        {
            this.Name = name;
            this.Class = class1;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Player {this.Name}: {this.Class}");
            sb.AppendLine($"Rank: {this.Rank}");
            sb.AppendLine($"Description: {this.Description}");
            return sb.ToString().TrimEnd();
        }

        
    }
}
