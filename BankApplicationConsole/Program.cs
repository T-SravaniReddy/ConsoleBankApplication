using System;
using BankApp.Services;

namespace BankApplicationConsole
{
    class Program
    {
        public enum userOptions
        {
            CreateAccount = 1,
            Deposit,
            WithDraw,
            Transfer,
            TransactionHistory,
            Exit
        }
        static void Main(string[] args)
        {
            userOptions choice;
            AccountServices accountServices = new AccountServices();
            TransactionServices transactionServices = new TransactionServices();
            do
            {
                StandardMessages.WelcomeMessage();
                choice = (userOptions) Enum.Parse(typeof(userOptions), Console.ReadLine());

                switch (choice)
                {
                    case userOptions.CreateAccount:
                        AccountServices.AddAccount();
                        break;

                    case userOptions.Deposit:
                        AccountServices.Deposit();
                        break;

                    case userOptions.WithDraw:
                        AccountServices.Withdraw();
                        break;

                    case userOptions.Transfer:
                        AccountServices.TransferAmount();
                        break;

                    case userOptions.TransactionHistory:
                        TransactionServices.TransactionHistory();
                        break;

                    case userOptions.Exit:
                        StandardMessages.Exit();
                        break;

                    default:
                        StandardMessages.Invalid();
                        break;
                }
                Console.WriteLine("\n");
            } while (choice != userOptions.Exit);

        }
    }
}