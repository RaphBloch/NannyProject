using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BE.RandomCreation;

namespace BE
{


    /// <summary>
    /// A class to mannage nannies in the project
    /// </summary>
    public class Nanny : IComparable
    {
        //---------------
        //  CONSTRUCTOR
        //---------------

        /// <summary>
        /// Constructor for nannies' class
        /// </summary>
        public Nanny()
        {
            ID = 0;
            FamilyName = "";
            FirstName = "";
            Phone = "";
            Address = ""; 
            Elevator = false;
            MinAge = 3;
            MaxAge = 36;
            MaxChilds = 5;
            Birth = DateTime.Parse("01/01/1980");
            IsSalaryPerHour = true;
            SalaryType = PaymentType.Hour;
            Salary = 0;
            Recommandation = "";
            Vacation = VacationType.Education;
            P = new Planning();
        }

        public Nanny(Nanny n)
        {
            ID = n.ID;
            Phone = n.Phone;
            FamilyName = n.FamilyName;
            FirstName = n.FirstName;
            Birth = new DateTime(n.Birth.Year, n.Birth.Month, n.Birth.Day);
            Address = n.Address;
            Elevator = n.Elevator;
            Floor = n.Floor;
            Seniority = n.Seniority;
            MaxChilds = n.MaxChilds;
            MinAge = n.MinAge;
            MaxAge = n.MaxAge;
            IsSalaryPerHour = n.IsSalaryPerHour;
            Salary = n.Salary;
            p = n.p;
            SalaryType = n.SalaryType;
            Recommandation = n.Recommandation;
            CountChild = n.CountChild;
            Vacation = n.Vacation;
            P = new Planning(n.P.Plan);
        }

        //-------------------------
        //  FIELDS AND PROPERTIES
        //-------------------------
        public int ID { get; set; }
        public string FamilyName { get; set; }
        public string FirstName { get; set; }
        public string Phone { get; set; }
        public DateTime Birth { get; set; }
        public string Address { get; set; }
        public bool Elevator { get; set; }
        public int Floor { get; set; }
        public int Seniority { get; set; }
        public int MaxChilds { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public bool IsSalaryPerHour { get; set; }

        public double Salary { get; set; }
        private PaymentType p;
        public PaymentType SalaryType {
            get {
                return p;
            }
            set
            {
                p = value;
                IsSalaryPerHour = (p == PaymentType.Hour) ? false : true;

            }
        }
        public string Recommandation { get; set; }
        public VacationType Vacation { get; set; }
        public int CountChild { get; set; }


        public string SimplePresentation
        {
            get { return String.Format("{0} {1} {2}", ID, FirstName, FamilyName); }
        }

        public Planning P { get; set; }

        //-----------
        //  METHODS
        //-----------

        /// <summary>
        /// Override the ToString function
        /// </summary>
        /// <returns>Returns a quick description of the Nanny</returns>
        public override string ToString()
        {
            string str = "";
            str += "ID: " + String.Format("{0}", ID);
            str += String.Format("\nName: {0} {1}", FamilyName, FirstName);
            str += String.Format("\nPhone Number: {0}", Phone);
            str += String.Format("\nAddress: {0}", Address);
            str += "\nBirth: " + Birth.ToShortDateString();

            str += String.Format("\nFloor:{0}", Floor);
            str += "\nElevator:" + (Elevator ? "Yes" : "No");

            //str += String.Format("\nSeniority: {0}", Seniority);
            str += String.Format("\nMaxChilds: {0}", MaxChilds);
            str += String.Format("\nMinAge: {0}", MinAge);
            str += String.Format("\nMaxAge: {0}", MaxAge);

            if (IsSalaryPerHour)
                str += String.Format("\nSalary: {0:0.00} NIS/hour", Salary);
            else
                str += String.Format("\nSalary: {0:0.00} NIS/month", Salary);

            str += "\nVacation according to ministery of ";
            if (Vacation == VacationType.Education)
                str += "education";
            else str += "work";

            str += "\n\n==============================" +
                "\nRecommandation(s):\n" + Recommandation 
                + "\n===============================\n\n";

            str += "PLANNING\n";
            str += P.ToString();
            return str;
        }



        /// <summary>
        /// To get possibility of sorting our data
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Comparaison between ID</returns>
        public int CompareTo(object obj)
        {
            Nanny c = (Nanny)obj;
            return ID.CompareTo(c.ID);
        }

    }

   
}
