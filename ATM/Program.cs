// Author: Annija Cunska

// Please implement interface ICashMachine. Cash machine accepts/withdraws only 5, 10, 20, 50, 100 banknotes.
// Please write some unit tests if possible.

namespace CashMachine
{
    public interface ICashMachine
    {
        int Withdraw(int amount);

        void Insert(int[] cash);
    }

    public class ATM : ICashMachine
    {
        public double Balance { get; set; }

        public Dictionary<string, int> banknoteAmount = new Dictionary<string, int>();

        public void Insert(int[] cash)
        {
            int amount = 0;
            int[] validBanknotes = new int[] { 5, 10, 20, 50, 100 };

            foreach (int banknote in cash)
            {
                if (!validBanknotes.Contains(banknote))
                {
                    Console.WriteLine("Deposit only accepts 5, 10, 20, 50 and 100 banknotes. DEPOSIT CANCELED");
                    return;
                }
                amount += banknote;
            }
            Balance += amount;
            Console.WriteLine("DEPOSIT SUCCESSFUL." + " Balance = " + Balance);
        }

        public int Withdraw(int amount)
        {
            int b5, b10, b20, b50, b100;
            int withdrawalAmount = amount;

            if (amount <= Balance)
            {
                if (amount % 5 == 0)
                {
                    b100 = addBanknote(100, ref amount);
                    b50 = addBanknote(50, ref amount);
                    b20 = addBanknote(20, ref amount);
                    b10 = addBanknote(10, ref amount);
                    b5 = addBanknote(5, ref amount);
                    if (amount > 0)
                    {
                        Console.WriteLine("Unable to withdraw this amount. Insufficient banknotes in the ATM. WITHDRAWAL CANCELED");
                        return 0;
                    }
                    Balance -= withdrawalAmount;
                    banknoteAmount["100"] -= b100;
                    banknoteAmount["50"] -= b50;
                    banknoteAmount["20"] -= b20;
                    banknoteAmount["10"] -= b10;
                    banknoteAmount["5"] -= b5;
                    Console.WriteLine("WITHDRAWAL SUCCESSFUL:\n " +
                        "100 - " + b100 + "\n " +
                        "50 - " + b50 + "\n " +
                        "20 - " + b20 + "\n " +
                        "10 - " + b10 + "\n " +
                        "5 - " + b5);
                    return 1;
                }
                else
                {
                    Console.WriteLine("Unable to withdraw this amount. WITHDRAWAL CANCELED");
                }
            }
            else
            {
                Console.WriteLine("Insufficient funds. WITHDRAWAL CANCELED");
            }
            return 0;
        }

        //Determines how many of specific denomination banknotes can be withdrawn
        private int addBanknote(int denom, ref int amount)
        {
            int banknotes = 0;
            while (amount / denom > 0)
            {
                if (banknoteAmount[denom.ToString()] > 0 + banknotes)
                {
                    banknotes++;
                    amount -= denom;
                }
                else break;
            }
            return banknotes;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            ATM atm = new ATM();

            //Start balance
            atm.Balance = 300;

            //Amount of banknote denominations
            atm.banknoteAmount.Add("5", 5);
            atm.banknoteAmount.Add("10", 0);
            atm.banknoteAmount.Add("20", 3);
            atm.banknoteAmount.Add("50", 1);
            atm.banknoteAmount.Add("100", 0);

            bool exit = false;

            Console.WriteLine("Hello");
            do
            {
                Console.WriteLine("\nChoose operation\n 1 - Deposit\n 2 - Withdrawal\n 3 - Current balance\n 4 - Exit");
                int operation = RequestOperation();

                switch (operation)
                {
                    case 1:
                        Console.WriteLine("Please enter banknotes. Seperate with ','.");
                        int[] cash = RequestBanknotes();
                        atm.Insert(cash);
                        break;
                    case 2:
                        Console.WriteLine("Please enter amount.");
                        int amount = RequestAmount();
                        atm.Withdraw(amount);
                        break;
                    case 3:
                        Console.WriteLine("Current balance = " + atm.Balance);
                        break;
                    case 4:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Default - operation not valid");
                        break;
                }
            } while (!exit);
        }

        private static int RequestOperation()
        {
            bool isInputValid = false;
            string inputOperation;
            int operation;
            bool isInt;
            do
            {
                inputOperation = Console.ReadLine();
                isInt = int.TryParse(inputOperation, out operation);

                if (isInt && (operation >= 1 && operation <= 4))
                {
                    isInputValid = true;
                }
                else
                {
                    Console.WriteLine("Please input valid operation code.");
                }
            } while (!isInputValid);
            return operation;
        }

        private static int[] RequestBanknotes()
        {
            bool isInputValid = false;
            string inputBanknotes;
            int[] cash;

            do
            {
                inputBanknotes = Console.ReadLine();
                int b;
                cash = inputBanknotes.Split(',').Select(n => int.TryParse(n, out b) && b > 0 ? b : -1).ToArray();

                if (!cash.Contains(-1))
                {
                    isInputValid = true;
                }
                else
                {
                    Console.WriteLine("Please input valid banknotes");
                }
            } while (!isInputValid);
            return cash;
        }

        private static int RequestAmount()
        {
            bool isInputValid = false;
            bool isInt;
            string inputAmount;
            int amount;

            do
            {
                inputAmount = Console.ReadLine();
                isInt = int.TryParse(inputAmount, out amount);

                if (isInt && amount > 0)
                {
                    isInputValid = true;
                }
                else
                {
                    Console.WriteLine("Please input valid amount");
                }
            } while (!isInputValid);
            return amount;
        }
    }
}
