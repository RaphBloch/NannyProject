using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// Class to manage a couple mother nanny
    /// </summary>
    public class CoupleMotherNanny : IComparable
    {
        public Nanny N { get; set; }
        public bool SameDays { get; set; }
        public bool AbsoluteConcordance { get; set; }
        public double TotalMinutes { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        public CoupleMotherNanny(MotherRequest m, Nanny n)
        {
            //  assign the nanny
            N = n;

            bool concordance = true;
            for (int i = 0; i < 7; i++)
            {
                if (m.P.Plan[i].Selected == true &&
                    n.P.Plan[i].Selected == false)
                    concordance = false;

                if (m.P.Plan[i].Start < n.P.Plan[i].Start ||
                    m.P.Plan[i].End > n.P.Plan[i].End)
                    concordance = false;
            }

            AbsoluteConcordance = concordance;
            TotalMinutes = 0;

            if(!AbsoluteConcordance)
            {
                double total = 0;
                double totalstart = 0;
                double totalend = 0;
                for (int i = 0; i < 7; i++)
                {
                    if(m.P.Plan[i].Start < n.P.Plan[i].Start)
                        totalstart = Math.Abs((n.P.Plan[i].Start - m.P.Plan[i].Start).TotalMinutes);

                    if(m.P.Plan[i].End > n.P.Plan[i].End)
                        totalend = Math.Abs((m.P.Plan[i].End - n.P.Plan[i].End).TotalMinutes);
                    total += totalstart + totalend;
                }
                TotalMinutes = total;
            }

            
            
        }

        /// <summary>
        /// Implementation of the ienumerable
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            CoupleMotherNanny cmn = (CoupleMotherNanny)obj;

            if (this.SameDays == true && cmn.SameDays == false)
                return -1;
            else if (this.SameDays == false && cmn.SameDays == true)
                return 1;

            else
            {
                if (this.AbsoluteConcordance == true && cmn.AbsoluteConcordance == false)
                    return -1;
                else if (this.AbsoluteConcordance == false && cmn.AbsoluteConcordance == true)
                    return 1;

                else
                    return this.TotalMinutes.CompareTo(cmn.TotalMinutes);
            }

        }
    }
}
