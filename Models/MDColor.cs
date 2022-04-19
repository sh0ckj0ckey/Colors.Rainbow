using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeColors_UWP
{
    public class MDColor
    {
        public string Name { get; set; }
        public string Hex { get; set; }

        public MDColor(string name, string hex) { this.Name = name; this.Hex = hex; }
    }
}
