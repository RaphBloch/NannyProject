using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DS
{
    /// <summary>
    /// Database
    /// </summary>
    public class DataSource
    {
        public static List<Mother> MotherList { get; set; }
        public static List<Nanny> NannyList { get; set; }
        public static List<Child> ChildList { get; set; }
        public static List<Contract> ContractList { get; set; }

        public DataSource()
        {
            MotherList = new List<Mother>();
            NannyList = new List<Nanny>();
            ChildList = new List<Child>();
            ContractList = new List<Contract>();
        }

        /// <summary>
        /// A simple way to see our data source
        /// </summary>
        /// <returns></returns>
        public static string MothersToString()
        {
            string str = "";
            string t = "\t";

            foreach (Mother m in MotherList)
            {
                str += m.ID + t + m.FamilyName + t + m.FirstName + "\n";
            }
            return str;
        }

        /// <summary>
        /// A simple way to see our data source
        /// </summary>
        /// <returns></returns>
        public static string NanniesToString()
        {
            string str = "";
            string t = "\t";

            foreach (Nanny m in NannyList)
            {
                str += m.ID + t + m.FamilyName + t + m.FirstName + "\n";
            }
            return str;
        }

        /// <summary>
        /// A simple way to see our data source
        /// </summary>
        /// <returns></returns>
        public static string ChildToString()
        {
            string str = "";
            string t = "\t";

            foreach (Child m in ChildList)
            {
                str += m.ID + t + m.FirstName + t + "Mother's ID: " + m.MotherID + "\n";
            }
            return str;
        }


        /// <summary>
        /// A simple way to see our data source
        /// </summary>
        /// <returns></returns>
        public static string ContractsToString()
        {
            string str = "";
            string t = "\t";

            foreach (Contract m in ContractList)
            {
                str += m.ID + t + "Child ID: " + m.ChildID + t + "Nanny ID: " + m.NannyID + t + "Signed? " + (m.Signed ? "Yes" : "No") + "\n";
            }
            return str;
        }

    }
}
