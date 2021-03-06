using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DataClasses
{
    public class ConnectionInformation
    {
        public string Ip { get; set; }
        public int Port { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
