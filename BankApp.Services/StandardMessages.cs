using System;
using System.Collections.Generic;
using System.Text;
using BankApp.Models;
using BankApp.Models.Exceptions;

namespace BankApp.Services
{
    public class StandardMessages
    {
        public static void WelcomeMessage()
        {
            Console.WriteLine("1. Create a Account");
            Console.WriteLine("2. Deposit Amount");
            Console.WriteLine("3. Withdraw Amount");
            Console.WriteLine("4. Transfer Amount");
            Console.WriteLine("5. Print Transaction History");
            Console.WriteLine("6. Exit");
            Console.Write("Select Your Choice: ");
        }

        public static string EnterPassword()
        {
            Console.Write("Enter Your Password: ");
            return Console.ReadLine();

        }

        public static string EnterUserName()
        {
            Console.Write("Enter Your Name: ");
            return Console.ReadLine();
        }

        public static int EnterAccountID()
        {
            Console.Write("Enter Your Account ID: ");
            return Convert.ToInt32(Console.ReadLine());
        }

        public static void PrintTransaction(Transaction transaction)
        {
            Console.Write(transaction.Datetime + "  " + transaction.Description + "  " + transaction.Amount + "  ");
        }

        public static void PrintLine()
        {
            Console.WriteLine();
        }

        public static void PrintFromStatement(string name)
        {
            Console.Write(" from " + name);
        }

        public static void PrintToStatement(string name)
        {
            Console.Write(" to " + name);
        }

        public static double EnterAmount()
        {
            Console.Write("Enter Amount To Deposit: ");
            return Convert.ToDouble(Console.ReadLine());
        }
        public static void PrintBalance(Account account)
        {
            Console.WriteLine("Your Available Balance : " + account.Amount);
        }

        public static void Invalid()
        {
            throw new InvalidChoice("Invalid Choice");
        }
        public static void Exit()
        {
            Console.WriteLine("Successfully Exited");
        }
        public static void CreateMessage(int accountID, string name, double amount)
        {
            Console.WriteLine("Account Created Successfully with Account No: " + accountID + " Name: " + name + " Balance: " + amount);
        }

        public static double EnterWithDrawAmount()
        {
            Console.Write("Enter Amount To Withdraw: ");
            return Convert.ToDouble(Console.ReadLine());
        }

        public static void TransactionHeading()
        {
            Console.WriteLine("\nTRANSACTION HISTORY\n ");
        }
        public static void DepositMessage()
        {
            Console.WriteLine("Successfully Deposited");
        }
        public static void WithDrawMessage()
        {
            Console.WriteLine("Successfully WithDrawn");
        }
        public static void TransferMessage()
        {
            Console.WriteLine("Successfully Transferred");
        }

        public static double EnterTransferAmount()
        {
            Console.Write("Enter Amount To Transfer: ");
            return Convert.ToDouble(Console.ReadLine());
        }
    }
}

