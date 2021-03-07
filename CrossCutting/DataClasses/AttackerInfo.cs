using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DataClasses
{
    public class AttackerInfo
    {
        public string Ip { get; set; }
        public int Count { get; set; }
        public int AverageConnectionTime { get; set; }
        public int StandardDerivation { get; set; }
    }
}
