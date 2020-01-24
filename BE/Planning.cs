using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{

        /// <summary>
    /// Class to manage a single day planning
    /// </summary>
    public class DayPlanning
    {
        //  FIELDS
        public bool Selected { get; set; }
        public string Day { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="b"></param>
        /// <param name="d"></param>
        /// <param name="s"></param>
        /// <param name="e"></param>
        public DayPlanning(bool b, string d, DateTime s, DateTime e)
        {
            Selected = b;
            Day = d;
            Start = s;
            End = e;
        }
        public DayPlanning(DayPlanning d)
        {
            Selected = d.Selected;
            Day = d.Day;
            Start = d.Start;
            End = d.End;
        }

        public DayPlanning(string str)
        {
            Selected = true;
            Day = str;
            Start = DateTime.Parse("06:00");
            End = DateTime.Parse("18:00");
        }

        public override string ToString()
        {
            string str = "";
            if (Selected)
            {
                str += Day;
                str += String.Format(" from {0} to {1}", Start.ToShortTimeString(), End.ToShortTimeString());
            }
            return str;
        }
    }

    /// <summary>
    /// Class to manage planning for mothers and nannies
    /// </summary>
    public class Planning
    {
        //  FIELDS
        public DayPlanning[] Plan { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="d"></param>
        public Planning(DayPlanning[] d)
        {
            Plan = d;
        }
        public Planning() { Plan = new DayPlanning[]
        {new DayPlanning("Sunday"),
        new DayPlanning("Monday"),
        new DayPlanning("Tuesday"),
        new DayPlanning("Wednesday"),
        new DayPlanning("Thursday"),
        new DayPlanning("Friday"),
        new DayPlanning("Saturday")}; }

        /// <summary>
        /// Override of to string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string str = "";

            for (int i = 0; i < 7; i++)
            {
                str += Plan[i].ToString() + "\n";
            }

            return str;
        }
    }

}
