using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeColors_UWP
{
    public class EveryColor
    {
        public string Name { get; set; }
        public double RValue { get; set; }
        public double GValue { get; set; }
        public double BValue { get; set; }
        public string HEX { get; set; }
        public Windows.UI.Color VisualColor { get; set; }
    }
}
