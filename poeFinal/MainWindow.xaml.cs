using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace poeFinal
{
    /// Interaction logic for MainWindow.xaml
    public partial class MainWindow : Window
    {
        public delegate void MyDelegate(string msg);

        public MainWindow()
        {
            InitializeComponent();
        }


        //ONSUBMIT EVENT CALCULATES & OUTPUTS RESULT
        private void btnSubmitForm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lblMessage.Content = "";
                lblHomeloan.Content = "The Homeloan Repayment is: ";
                lblAvailableMoney.Content = "The Available Money is: ";
                lblMonthlyCarCost.Content = "The monthly cost of car purchase is: ";
                lblTotalExpense.Content = "Your Total Expense is: ";
                lblExpenseOrder.Content = "All Expenses (Desc. Order): ";

                //CLASS FIELDS
                double rentalCost = 0;
                double homeloanCost = 0;
                double vehicleCost = 0;
                double expenditures = 0;
                double totalExpense = 0;
                List<double> expenseArray = new List<double>();

                //INPUT FOR GROSS-INCOME & TAX
                double grossIncome = Convert.ToDouble(txtIncome.Text);
                double tax = Convert.ToDouble(txtTax.Text);

                //COLLECT EXPENDITURE INPUT
                expenseArray.Add(Convert.ToDouble(txtGroceries.Text));
                expenseArray.Add(Convert.ToDouble(txtWater.Text));
                expenseArray.Add(Convert.ToDouble(txtTravel.Text));
                expenseArray.Add(Convert.ToDouble(txtTelephone.Text));
                expenseArray.Add(Convert.ToDouble(txtOtherExp.Text));

                //ADDS UP EXPENDITURES
                for (int i = 0; i < expenseArray.Count(); i++)
                {
                    expenditures += expenseArray[i];
                }


                //DECISION: RENT | BUY?
                int rentOrBuy = cmbOne.SelectedIndex;
                //IF RENTING
                if (rentOrBuy == 0)
                {
                    Rent rentObj = new Rent();
                    rentObj.RentalAmount = Convert.ToDouble(txtRentAmount.Text);
                    //DISPLAYS AVAILABLE MONEY & RENTAL-COST
                    rentalCost = rentObj.RentalAmount;
                    double availableMoney = grossIncome - (tax + expenditures + rentalCost);
                    lblAvailableMoney.Content = "The Available Monthly Money is: R" + availableMoney;
                }
                //IF BUYING (HOMELOAN)
                if (rentOrBuy == 1)
                {
                    Homeloan homeloanObj = new Homeloan();
                    homeloanObj.PurchasePrice = Convert.ToDouble(txtPrice.Text);
                    homeloanObj.TotalDeposit = Convert.ToDouble(txtDeposit.Text);
                    homeloanObj.InterestRate = Convert.ToDouble(txtInterestRate.Text);
                    homeloanObj.NumMonths = Convert.ToInt32(txtNumMonths.Text);
                    //DISPLAYS AVAILABLE MONEY & HOMELOAN REPAYMENT
                    homeloanCost = Math.Ceiling(homeloanObj.getHomeLoanRepayment() * 100) / 100;
                    double availableMoney = grossIncome - (tax + expenditures + homeloanCost);
                    lblHomeloan.Content = "The Homeloan Repayment is: R" + homeloanCost;
                    lblAvailableMoney.Content = "The Available Money is: R" + availableMoney;

                    //ASSESS CHANCE OF HOMELOAN APPROVAL
                    if (homeloanCost > (grossIncome * 0.33))
                    {
                        lblMessage.Content += "Approval of Homeloan is unlikely ";
                    }
                }


                //DECISION: BUY VEHICLE?
                int toBuyCar = cmbTwo.SelectedIndex;

                //IF BUY VEHICLE
                if (toBuyCar == 0)
                {
                    CarPurchase car = new CarPurchase();
                    car.CarMake = txtCarMake.Text;
                    car.CarPrice = Convert.ToDouble(txtCarPrice.Text);
                    car.CarDeposit = Convert.ToDouble(txtCarDeposit.Text);
                    car.CarInterestRate = Convert.ToDouble(txtCarInterest.Text);
                    car.CarPremium = Convert.ToDouble(txtCarPremium.Text);

                    //CAR PURCHASE EXPENSE ADDED TO EXPENSE ARRAY
                    vehicleCost = Math.Ceiling(car.getCarMonthlyCost() * 100) / 100;
                    lblMonthlyCarCost.Content = "The monthly cost of car purchase is: R" + vehicleCost;
                }


                //NOTIFY IF COST EXCEED 75% OF INCOME
                totalExpense = tax + expenditures + rentalCost + homeloanCost + vehicleCost;
                if (totalExpense > (grossIncome * 0.75))
                {
                    String msg = "Total expenses exceed 75% of income";
                    lblMessage.Content += "- Total expenses exceed 75% of income";

                    //INVOKE DELEGATE
                    MyDelegate d = new MyDelegate(Display);
                    d.Invoke(msg);
                }


                //DISPLAY TOTAL EXPENSES
                lblTotalExpense.Content = "Your Total Expense is: R" + Math.Ceiling(totalExpense * 100) / 100;


                //ADDING RENT, HOMELOAN & VEHICLE COSTS TO ARRAY
                expenseArray.Add(tax);
                expenseArray.Add(rentalCost);
                expenseArray.Add(homeloanCost);
                expenseArray.Add(vehicleCost);


                //SORTING EXPENSE ARRAY
                string output = "";
                output += "All Expenses: ";
                expenseArray.Sort((a, b) => b.CompareTo(a));
                for (int i = 0; i < expenseArray.Count(); i++)
                {
                    output += "R" + expenseArray[i] + ", ";
                }
                lblExpenseOrder.Content = output;
            }


            catch {
                lblMessage.Content = "ERROR! Please, enter required data accurately!";
            }
        }


        //INVOKED BY DELEGATE
        static void Display(string msg)
        {
            Console.WriteLine(msg);
        }


        //ONCLICK EVENT RESETS FORM
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            lblMessage.Content = "";

            txtIncome.Text = "";
            txtTax.Text = "";
            txtGroceries.Text = "";
            txtWater.Text = "";
            txtTravel.Text = "";
            txtTelephone.Text = "";
            txtOtherExp.Text = "";

            txtRentAmount.Text = "";
            txtPrice.Text = "";
            txtDeposit.Text = "";
            txtInterestRate.Text = "";
            txtNumMonths.Text = "";

            txtCarMake.Text = "";
            txtCarPrice.Text = "";
            txtCarDeposit.Text = "";
            txtCarInterest.Text = "";
            txtCarPremium.Text = "";

            lblHomeloan.Content = "The Homeloan Repayment is: ";
            lblAvailableMoney.Content = "The Available Money is: ";
            lblMonthlyCarCost.Content = "The monthly cost of car purchase is: ";
            lblTotalExpense.Content = "Your Total Expense is: ";
            lblExpenseOrder.Content = "All Expenses (Desc. Order): ";
        }


        //ADVANCED FEATURE
        //PERMITS USER INPUT BASED ON SELECTION (RENT | HOMELOAN)
        private void cmbOne_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbOne.SelectedIndex == 0)
            {
                txtRentAmount.Visibility = Visibility.Visible;

                txtPrice.Visibility = Visibility.Hidden;
                txtDeposit.Visibility = Visibility.Hidden;
                txtInterestRate.Visibility = Visibility.Hidden;
                txtNumMonths.Visibility = Visibility.Hidden;
                lblHomeloan.Visibility = Visibility.Hidden;
            }
            if (cmbOne.SelectedIndex == 1)
            {
                txtRentAmount.Visibility = Visibility.Hidden;

                txtPrice.Visibility = Visibility.Visible;
                txtDeposit.Visibility = Visibility.Visible;
                txtInterestRate.Visibility = Visibility.Visible;
                txtNumMonths.Visibility = Visibility.Visible;
                lblHomeloan.Visibility = Visibility.Visible;
            }
        }


        //ADVANCED FEATURE
        //PERMITS USER INPUT BASED ON SELECTION (BUY VEHICLE)
        private void cmbTwo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTwo.SelectedIndex == 0)
            {
                txtCarMake.Visibility = Visibility.Visible;
                txtCarPrice.Visibility = Visibility.Visible;
                txtCarDeposit.Visibility = Visibility.Visible;
                txtCarInterest.Visibility = Visibility.Visible;
                txtCarPremium.Visibility = Visibility.Visible;
                lblMonthlyCarCost.Visibility = Visibility.Visible;
            }
            if (cmbTwo.SelectedIndex == 1)
            {
                txtCarMake.Visibility = Visibility.Hidden;
                txtCarPrice.Visibility = Visibility.Hidden;
                txtCarDeposit.Visibility = Visibility.Hidden;
                txtCarInterest.Visibility = Visibility.Hidden;
                txtCarPremium.Visibility = Visibility.Hidden;
                lblMonthlyCarCost.Visibility = Visibility.Hidden;
            }
        }


        //COMPUTES USER'S MONTHLY SAVINGS GOAL
        private void btnSaveCompute_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MonthlySaving monthlysaveObj = new MonthlySaving();
                monthlysaveObj.SaveReason = txtSaveReason.Text;
                monthlysaveObj.SaveAmount = Convert.ToDouble(txtSaveAmount.Text);
                monthlysaveObj.SaveInterest = Convert.ToDouble(txtSaveInterest.Text);
                monthlysaveObj.SavePeriod = Convert.ToDouble(txtSavePeriod.Text);
                //DISPLAYS AVAILABLE MONEY & HOMELOAN REPAYMENT
                double monthlySavingsCost = Math.Ceiling(monthlysaveObj.getMonthlySavings() * 100) / 100;
                txtSaveMessage.Text = "The monthly savings needed to reach your goal is R" + monthlySavingsCost;
            }
            catch {
                txtSaveMessage.Text = "ERROR in input provided!";
            }
        }


        //UNUSED EVENT HANDLER
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {}
    }
}
