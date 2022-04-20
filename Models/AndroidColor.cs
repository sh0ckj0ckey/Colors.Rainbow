using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeColors_UWP
{
    public class AndroidColor
    {
        public string Name { get; set; }
        public string Hex { get; set; }
        public List<AndroidColorInfo> Colors { get; set; }

        public AndroidColor(string name, string hex, List<AndroidColorInfo> colors)
        {
            Name = name;
            Hex = hex;
            Colors = colors;
        }
    }

    public class AndroidColorInfo
    {
        public string Name { get; set; }
        public string Hex { get; set; }

        public AndroidColorInfo(string name, string hex)
        {
            this.Name = name; this.Hex = hex;
        }
    }
}
