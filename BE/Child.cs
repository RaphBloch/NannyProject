using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BE.RandomCreation;

namespace BE
{
    /// <summary>
    /// A class to manage childs in the project
    /// </summary>
    public class Child : IComparable
    {
        #region CONSTRUCTOR
        //----------------
        //  CONSTRUCTOR
        //----------------

        /// <summary>
        /// Contructor for child class
        /// </summary>
        public Child()
        {
            ID = 0;
            MotherID = 0;
            FirstName = "";
            SpecialNeeds = false;
        }

        public Child(Child c)
        {
            ID = c.ID;
            MotherID = c.MotherID;
            FirstName = c.FirstName;
            Birth = new DateTime(c.Birth.Year, c.Birth.Month, c.Birth.Day);
            SpecialNeeds = c.SpecialNeeds;
            Needs = c.Needs;
            FamilyName = c.FamilyName;
        }
        #endregion 

        #region FIELDS and PROPERTIES
        //-------------------------
        //  FIELDS AND PROPERTIES
        //-------------------------
        public int ID { get; set; }
        public int MotherID { get; set; }
        public string FirstName { get; set; }
        public DateTime Birth { get; set; }
        public bool SpecialNeeds { get; set; }
        public string Needs { get; set; }
        public string SimplePresentation
        {
            get { return String.Format("{0} {1} {2}", ID, FamilyName, FirstName); }
        }
        public string FamilyName { get; set; }
        #endregion

        #region METHODS
        //-----------
        //  METHODS
        //-----------

        /// <summary>
        /// Override of the ToString function
        /// </summary>
        /// <returns>Returns a quick description of the child</returns>
        public override string ToString()
        {
            string str = "";
            str += "ID: " + String.Format("{0}", ID);
            str += "\nMother's ID: " + String.Format("{0}", MotherID);
            str += String.Format("\nFirst name: {0}", FirstName);
            str += String.Format("\nBirth: {0}", Birth.ToShortDateString());
            str += (SpecialNeeds ? "\n==========================\nNeeds: " + Needs + "\n==========================\n": "");

            return str;
        }

        /// <summary>
        /// To override the compare functions
        /// </summary>
        /// <param name="obj">The object to compare to</param>
        /// <returns>Return which of the object is the bigger</returns>
        public int CompareTo(object obj)
        {
            Child i = (Child)obj;
            return ID.CompareTo(i.ID);
        }

        #endregion 
    }
}
