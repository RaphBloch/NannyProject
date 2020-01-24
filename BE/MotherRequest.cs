using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BE.RandomCreation;

namespace BE
{

    /// <summary>
    /// Class to define the specific request for each mother
    /// </summary>
    public class MotherRequest
    {

        public string Address { get; set; }
        public string SearchAddress { get; set; }
        public bool IsSearchAddress { get; set; }
        public int DistanceAccepted { get; set; }
        public int DistanceWanted { get; set; }

        public Planning P { get; set; }

        /// <summary>
        /// Constructor for mother requests
        /// </summary>
        public MotherRequest()
        {
            Address = ""; //Address();
            SearchAddress = ""; // Address();
            DistanceWanted = 0;
            DistanceAccepted = 0;
            P = new Planning();
        }

        /// <summary>
        ///Copy  Constructor for mother requests
        /// </summary>
        public MotherRequest(MotherRequest mp)
        {
            Address = mp.Address;
            SearchAddress = mp.SearchAddress;
            DistanceWanted = mp.DistanceWanted;
            DistanceAccepted = mp.DistanceAccepted;
            P = new Planning(mp.P.Plan);

        }

    }
}
