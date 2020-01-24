using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static BE.RandomCreation;

namespace BE
{

    /// <summary>
    /// A class to manage contracts in the project
    /// </summary>
    public class Contract : IComparable
    {

        //----------------
        //  CONSTRUCTOR
        //----------------

        /// <summary>
        /// Constructor for the contract class
        /// </summary>
        public Contract(int nID, int cID)
        {

            NannyID = nID;
            ChildID = cID;
            Meet = Test();
            IsSalaryPerHour = Test();
            Payment = Test() ? PaymentType.Month : PaymentType.Hour;
            Begin = DateTime.Now;
            End = End();
        }

        public Contract(Contract c)
        {
            ID = c.ID;
            NannyID = c.NannyID;
            ChildID = c.ChildID;
            Meet = c.Meet;
            Signed = c.Signed;
            SalaryPerHour = c.SalaryPerHour;
            SalaryPerMonth = c.SalaryPerMonth;
            Salary = c.Salary;
            Distance = c.Distance;
            Payment = c.Payment;
            Begin = c.Begin;
            End = c.End;
            ChildName = c.ChildName;
            NannyName = c.NannyName;
        }

        /// </summary>
        public Contract()
        {

            NannyID = 0;
            ChildID = 0;
            Meet = false;
            Payment = PaymentType.Hour;
            //Begin = DateTime.Now.AddDays(1);
            //End = End();
        }


        //-------------------------
        //  FIELDS AND PROPERTIES
        //-------------------------

        //private static int num = 0;

        public int ID { get; set; }
        public int NannyID { get; set; }
        public int ChildID { get; set; }
        public bool Meet { get; set; }
        public bool Signed { get; set; }
        public double SalaryPerHour { get; set; }
        public double SalaryPerMonth { get; set; }
        public double Salary { get; set; }
        

        private PaymentType p;
        public PaymentType Payment
        {
            get
            {
                return p;
            }
            set
            {
                p = value;
                IsSalaryPerHour = (p == PaymentType.Hour) ? false : true;
            }
        }

        public bool IsSalaryPerHour { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public int Distance { get; set; }

        public string SimplePresentation
        {
            get { return String.Format("Contract ID: {0}\nChild's name: {1}\nNanny's name : {2}",
                ID, ChildName, NannyName); }
        }
        public string ChildName { get; set; }
        public string NannyName { get; set; }






        //-----------
        //  METHODS
        //-----------

        /// <summary>
        /// Override of the tostring function
        /// </summary>
        /// <returns>A quick description of the contract</returns>
        public override string ToString()
        {
            string str = "";
            str += "ID: " + String.Format("{0}", ID);
            str += String.Format("\nNanny's ID: {0}", NannyID);
            str += String.Format("\nChild's ID: {0}", ChildID);

            str += "\nAlready meet: " + (Meet ? "Yes" : "No");
            str += "\nAlready signed: " + (Signed ? "Yes" : "No");

            str += "\nSalary: " + String.Format("{0:0.00} NIS/month", Salary);

            str += String.Format("\nDistance : {0}", Distance);
            str += String.Format("\nStart at: {0}", Begin.ToShortDateString());
            str += String.Format("\nEnd at: {0}", End.ToShortDateString());

            return str;
        }

        /// <summary>
        /// To override the compare functions
        /// </summary>
        /// <param name="obj">The object to compare to</param>
        /// <returns>Return which of the object is the bigger</returns>
        public int CompareTo(object obj)
        {
            Contract c = (Contract)obj;
            int id1 = ID;
            int id2 = c.ID;
            bool b1 = this.Signed;
            bool b2 = c.Signed;

            if (b1 && !b2) return -1;
            if (b2 && !b1) return 1;
            return id1.CompareTo(id2);
        }



    }
}
