using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poeFinal
{
    internal class MonthlySaving
    {
        //CLASS FIELDS
        public string SaveReason { set; get; }
        public double SaveAmount { set; get; }
        public double SaveInterest { set; get; }
        public double SavePeriod { set; get; }


        //CALCULATES MONTHLY SAVINGS
        public double getMonthlySavings()
        {
            //MONTHLY SAVINGS FOMULAR
            double monthlysaving = (SaveAmount * (1 + (SaveInterest / (double)100) * ((double)SavePeriod / (double)12))) / (double)SavePeriod;

            return monthlysaving;
        }
    }
}
