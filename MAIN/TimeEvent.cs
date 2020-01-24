using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAIN
{
    public class TimeEventArgs : EventArgs
    {
        DateTime MinTime { get; set; }

        public TimeEventArgs(DateTime d)
        {
            MinTime = d;
        }
    }
}
