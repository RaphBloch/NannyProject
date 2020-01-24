using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BE.RandomCreation;

namespace BE
{
    /// <summary>
    /// A class to manage mothers in the project
    /// </summary>
    public class Mother : IComparable
    {
        //----------------
        //  CONSTRUCTOR
        //----------------

        /// <summary>
        /// Constructor for mother class
        /// Initialize the daylist
        /// </summary>
        public Mother()
        {
            Request = new MotherRequest();
            ID = 0;
            FamilyName = "";
            FirstName = "";
            Phone = "";
            Commentaries = "";
        }

        public Mother(Mother m)
         {
            ID = m.ID;
            FamilyName = m.FamilyName;
            FirstName = m.FirstName;
            Phone = m.Phone;
            Commentaries = m.Commentaries;
            Request=new MotherRequest(m.Request);
         }



        //-------------------------
        //  FIELDS AND PROPERTIES
        //-------------------------
        public int ID { get; set; }
        public string FamilyName { get; set; }
        public string FirstName { get; set; }
        public string Phone { get; set; }
        public string SimplePresentation
        {
            get { return String.Format("{0} {1} {2}",ID, FamilyName, FirstName); }
        }
        public MotherRequest Request { get; set; }
        public string Commentaries { get; set; }

        //-----------
        //  METHODS
        //-----------

        /// <summary>
        /// Override the ToString classical function for the mother class
        /// </summary>
        /// <returns>Returns a quick description of the mother</returns>
        public override string ToString()
        {
            string str = "";
            str += "ID: " + String.Format("{0}", ID);
            str += String.Format("\nName: {0} {1}", FamilyName, FirstName);
            str += String.Format("\nPhone Number: {0}", Phone);
            str += String.Format("\nAddress: {0}", Request.Address);

            str += String.Format("\nSearch for an address arround: {0}", Request.SearchAddress);

            str += String.Format("\nDistance Wanted: {0}", Request.DistanceWanted);
            str += String.Format("\nDistance accepted: {0}", Request.DistanceAccepted);

            str += "\n" + Request.P;

            str += "\n\n====================================\n"
                + "Commentaries:"
                + Commentaries
                + "\n====================================";

            

            return str;
        }

        /// <summary>
        /// To override the compare functions
        /// </summary>
        /// <param name="obj">The object to compare to</param>
        /// <returns>Return which of the object is the bigger</returns>
        public int CompareTo(object obj)
        {
            Mother i = (Mother)obj;
            return ID.CompareTo(i.ID);
        }

    }
}
