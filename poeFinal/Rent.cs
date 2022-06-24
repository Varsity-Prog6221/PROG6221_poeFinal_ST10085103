using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poeFinal
{
    internal class Rent : Expense
    {
        //CLASS FIELD
        public double RentalAmount { set; get; }


        //METHOD CALCULATES COST OF RENTING ACCOMODATION
        public override double calcAccomodation(double grossIncome)
        {
            double houseCost = grossIncome - RentalAmount;

            return houseCost;
        }
    }
}
