using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// This class is only for testing our program and add random values in the constructors
    /// </summary>
    public class RandomCreation
    {
        static Random r = new Random();

        /// <summary>
        /// To create a random ID between 1 and 100
        /// </summary>
        /// <returns></returns>
        public static int ID()
        {
            return r.Next(0, 101);
        }

        /// <summary>
        /// Create a random Name
        /// </summary>
        /// <returns></returns>
        public static string Name()
        {
            string lower = "abcdefghijklmnopqrstuvwxyz";
            string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            //  create a name with 10 characters
            string name = "";
            name += upper[r.Next(0, 26)];

            for (int i = 1; i < 10; i++)
                name += lower[r.Next(0, 26)];

            return name;
        }


        
        /// <summary>
        /// Create a random planning
        /// </summary>
        /// <returns></returns>
        public static Planning P()
        {


            return new Planning(new DayPlanning[]{
                DP("Sunday"),
                DP("Monday"),
                DP("Tuesday"),
                DP("Wednesday"),
                DP("Thursday"),
                DP("Friday"),
                DP("Saturday")
            });
        }

        public static DayPlanning DP(string dayName)
        {
            return new DayPlanning(Test(),
                dayName,
                DateTime.Parse("00:00"),
                DateTime.Parse("23:59"));
        }

        /// <summary>
        /// Create a random phone number
        /// </summary>
        /// <returns></returns>
        public static string Phone()
        {
            string num = "0123456789";

            string p = "052";
            for (int i = 3; i < 10; i++)
                p += num[r.Next(0, 10)];

            return p;
        }

        /// <summary>
        /// Set random accepted distance
        /// </summary>
        /// <returns></returns>
        public static int Distance()
        {
            return r.Next(100, 1000);
        }

        public static string Address()
        {
            string[] cities = { "Jerusalem", "TelAviv", "Raanana", "Netanya" };
            string[] streets = { "Yaffo", "Herzl", "Sheshet Hayamim" };

            return String.Format("{0}",r.Next(1,100)) + "," + cities[r.Next(0, 4)] + "," + streets[r.Next(0, 3)];
        }

        /// <summary>
        /// Create the array of days in which the mother will want a nanny
        /// </summary>
        /// <returns></returns>
        public static bool[] DayList()
        {
            bool[] days = { false, false, false, false, false, false, false };
            for (int i = 0; i < 7; i++)
                days[i] = (r.Next(0, 2) == 0 ? false : true);

            return days;
        }

        public static bool Test()
        {
            int flag = r.Next(0, 2);
            return flag == 1;
        }

        public static double SalaryPerHour()
        {
            double d = r.NextDouble();
            double i = (double)r.Next(30, 61);
            return d + i;
        }

        public static double SalaryPerMonth()
        {
            double d = r.NextDouble();
            double i = r.Next(1500, 2001);
            return d + i;
        }

        public static DateTime Birth()
        {
            int day = r.Next(1, 29);
            int month = r.Next(1, 13);
            int year = r.Next(2010, 2018);

            return new DateTime(year, month, day);
        }

        public static DateTime BirthNanny()
        {
            int day = r.Next(1, 29);
            int month = r.Next(1, 13);
            int year = r.Next(1960, 2000);

            return new DateTime(year, month, day);
        }


        public static DateTime End()
        {
            double b = r.Next(10, 90);
            return DateTime.Now.AddDays(b);
        }
    }
}
