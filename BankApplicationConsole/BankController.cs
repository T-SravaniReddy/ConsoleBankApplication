using BankApp.Models;
using BankApp.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApplicationConsole
{
    public class BankController
    {
        static internal void SetupBank()
        {
            Bank bank = BankService.AddBank(StandardMessages.EnterBankName());
            Console.WriteLine("Your Bank ID is " + bank.BankID);
            int Choice;
            do
            {
                Console.WriteLine("1. Add Bank Staff");
                Console.WriteLine("2. Exit");
                Console.Write("Choose Any Option : ");
                Choice = Convert.ToInt32(Console.ReadLine());
                if (Choice == 1)
                {
                    string name = StandardMessages.EnterUserName();
                    string password = StandardMessages.EnterPassword();
                    string StaffID = BankStaffService.AddBankStaff(bank.BankID, name, password);
                    Console.WriteLine("Your Staff ID : " + StaffID);

                } else if (Choice != 2)
                {
                    Console.WriteLine("Invalid Selection");
                }
            } while (Choice != 2);
           
        }
    }
}
