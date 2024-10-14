using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;

namespace whatifbtc
{
    internal class Program
    {
        public static void Main(string[] args)
        {

            List<day> dayList = new List<day>();
            List<calc> calculations = new List<calc>();

            User user = new User
            {
                balance = 0,
                name = "sean",
                recurringAdd = 50,
                interval = "weekly",
                btcHolding = 0
            };

            List<string> data = dataInput();

            dayList = dataFilter(data);
            var orderedDayList = dayList.OrderBy(d => d.DayOfMarket).ToList();

            if (user.interval == "weekly")
            {
                int daysForward = 7;

                for (int i = 0; i < orderedDayList.Count; i = i + daysForward)
                {
                    var calc = new calc
                    {
                        Day = orderedDayList[i],


                    };


                    // add to the principal with the recuring add
                    user.principal += user.recurringAdd;

                    //we need to buy the amount of btc that we can for that 50. first divide the amount we buy.
                    user.btcHolding = user.btcHolding + (user.recurringAdd / calc.Day.amount);

                    user.balance = (user.btcHolding * calc.Day.amount);
                    //
                    calc.principal = user.principal;
                    calc.btcHolding = user.btcHolding;

                    calc.btcHoldingUsd = user.balance;
                    calculations.Add(calc);

                }

            }


            dataOutput(calculations);

        }

        public static void dataOutput(List<calc> calculations)
        {

            string pathS = "../../../data/output.csv";

            StreamWriter print = new StreamWriter(pathS);



            foreach (var item in calculations)
            {

                print.WriteLine($"{item.Day.DayOfMarket.ToString()}, {item.btcHoldingUsd}, {item.principal}");


            }

     

            print.Close();

        }




        public static List<string> dataInput()
        {

            string pathU = "../../../data/BitcoinHistoricalData.csv";


            string dream;
            StreamReader scan = new StreamReader(pathU);

            List<string> excelFile = new List<string>();

            dream = scan.ReadLine();
            while (dream != null)
            {


                dream = scan.ReadLine();

                excelFile.Add(dream);
            }


            scan.Close();

            return excelFile;

        }

        public static List<day> dataFilter(List<string> data)
        {

            List<day> dayList = new List<day>();



            foreach (var item in data)
            {

                // there is a comma in the thousand

                if (item != null)
                {

                    var array = item.Split(',', 2); // code isnt failing, data does 

                    //we now have three indexs 0 1 2 
                    //                       d - a - a



                    string amountString = array[1].Trim('\"'); // Remove quotes
                    amountString = amountString.Replace(",", ""); // Remove the comma
                    decimal amount = decimal.Parse(amountString);



                    day period = new day
                    {
                        DayOfMarket = DateTime.Parse(array[0]),

                        amount = amount

                    };

                    dayList.Add(period);
                }


            }




            return dayList;

        }
    }
}
