using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAIN
{
    public class CheckFields
    {

        /// <summary>
        /// Check valid id
        /// </summary>
        /// <param name="id"></param>
        public static void IsValidID(int id)
        {
            if (id < 10000 || id > 99999)
                throw new Exception("Wrong id format, please enter a 5 digits number.");
        }

        /// <summary>
        /// Check valid address
        /// </summary>
        /// <param name="address"></param>
        public static void IsValidAddress(string address)
        {

        }

        /// <summary>
        /// Check if a specific number in the form is valid
        /// </summary>
        /// <param name="num"></param>
        public static void IsValidPositiveNumber(string num)
        {
            try
            {
                int n =int.Parse(num);
                if (n < 0)
                    throw new Exception();
            }
            catch (Exception)
            {
                throw new Exception("Please enter a valid positive number.");
            }
        }

        /// <summary>
        /// CheckForMother for distances
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        public static void IsValidAcceptedAndWantedDistance(string d1, string d2)
        {
            IsValidPositiveNumber(d1);
            IsValidPositiveNumber(d2);
            int dd1 = int.Parse(d1);
            int dd2 = int.Parse(d2);

            if (dd1 > dd2)
                throw new Exception("The accepted distance must be bigger than the wanted distance.");
        }
    }
}
