using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poeFinal
{
    internal class CarPurchase
    {
        //CLASS FIELDS
        public string CarMake { set; get; }
        public double CarPrice { set; get; }
        public double CarDeposit { set; get; }
        public double CarInterestRate { set; get; }
        public double CarPremium { set; get; }


        //CALCULATES MONTHLY COST OF CAR PURCHASE
        public double getCarMonthlyCost()
        {
            //CAR MONTHLY COST FOMULAR
            double cost = ((CarPrice - CarDeposit) * (1 + (CarInterestRate / (double)100) * ((double)60 / (double)12))) / (double)60;
            double carCost = cost + CarPremium;

            return carCost;
        }
    }
}
