using System;
using BankApp.Services;
using BankApp.Models;
using System.Collections.Generic;
using BankApp.Models.Enums;

namespace BankApplicationConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            BankService bankService = new BankService();
            int choice;
            do
            {
                Console.WriteLine("1.Setup A Bank");
                Console.WriteLine("2.Login Page");
                Console.Write("Choose Any Option :");
                choice = Convert.ToInt32(Console.ReadLine());
                if (choice == 1)
                {
                    BankController.SetupBank();
                }
                else if (choice == 2)
                {
                    Console.WriteLine("\tLOGIN PAGE");
                    Console.WriteLine("1. Login As Account Holder");
                    Console.WriteLine("2. Login As Bank Staff");
                    Console.Write("Choose Your Login : ");

                    int LoginChoice = Convert.ToInt32(Console.ReadLine());
                    if (LoginChoice == 1)
                    {
                        AccountHolderController.AccountHolderChoice();
                    }
                    else if (LoginChoice == 2)
                    {
                        BankStaffController.BankStaffChoice();
                    }
                    else
                    {
                        Console.WriteLine("Invalid Selection");
                    }
                }
                else 
                {
                    Console.WriteLine("Invalid Selection");
                }
            } while (choice < 3);
           
        }

    }
}