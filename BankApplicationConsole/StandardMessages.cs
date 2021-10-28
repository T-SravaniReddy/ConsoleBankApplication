using System;
using System.Collections.Generic;
using System.Text;
using BankApp.Models;
using BankApp.Models.Enums;
using BankApp.Models.Exceptions;
using BankApp.Services;

namespace BankApplicationConsole
{
    public class StandardMessages
    {
        internal static void WelcomeMessage()
        {
            Console.WriteLine("1. Deposit Amount");
            Console.WriteLine("2. Withdraw Amount");
            Console.WriteLine("3. Transfer Amount");
            Console.WriteLine("4. Print Transaction History");
            Console.WriteLine("5. Exit");
            Console.Write("Select Your Choice: ");
        }

        internal static string EnterBankName()
        {
            Console.Write("Enter Bank Name: ");
            return Console.ReadLine();
        }

        internal static void StaffWelcomeMessage()
        {
            Console.WriteLine("1. Create New Account");
            Console.WriteLine("2. Update Account");
            Console.WriteLine("3. Delete Account");
            Console.WriteLine("4. Add New Currency");
            Console.WriteLine("5. Add Same Bank Service Charge");
            Console.WriteLine("6. Add Different Bank Service Charge");
            Console.WriteLine("7. Account Transaction History");
            Console.WriteLine("8. Revert Transaction");
            Console.WriteLine("9. Exit");
            Console.Write("Select Your Choice: ");
        }

        internal static string EnterCurrency(Bank bank)
        {
            Console.Write("Enter Currency Among ");
            int i;
            for (i = 0; i < bank.AcceptedCurrency.Count - 1; i++)
            {
                Console.Write(bank.AcceptedCurrency[i].CurrencyCode + ", ");
            }
            Console.Write(bank.AcceptedCurrency[i].CurrencyCode + ":");
            string currencyCode = Console.ReadLine();
            return currencyCode;
        }

        internal static Gender EnterGender()
        {
            Console.Write("Enter Gender (Female/Male/Other):");
            return (Gender)Enum.Parse(typeof(Gender), Console.ReadLine());
        }

        internal static string EnterCurrencyCode()
        {
            Console.Write("Enter Currency Code: ");
            return Console.ReadLine();
        }

        internal static double EnterEquivalentINR()
        {
            Console.Write("Enter Equivalent Exchange Rate In INR ");
            return Convert.ToDouble(Console.ReadLine());
        }

        internal static string EnterPassword()
        {
            Console.Write("Enter Password: ");
            return Console.ReadLine();

        }

        internal static string EnterUserName()
        {
            Console.Write("Enter User Name: ");
            return Console.ReadLine();
        }

        internal static int EnterSameIMPS()
        {
            Console.Write("Enter IMPS Service Charges For Same Bank : ");
            return Convert.ToInt32(Console.ReadLine());
        }

        internal static string EnterDestinationBankID()
        {
            Console.Write("Enter Destination Bank ID: ");
            return Console.ReadLine();
        }

        internal static string EnterDestinationAccountID()
        {
            Console.Write("Enter Destination Account ID: ");
            return Console.ReadLine();
        }

        internal static int EnterService()
        {
            Console.Write("Enter Service Among 1.RTGS 2.IMPS ");
            return Convert.ToInt32(Console.ReadLine());
        }

        internal static int EnterSameRTGS()
        {
            Console.Write("Enter RTGS Service Charges For Same Bank : ");
            return Convert.ToInt32(Console.ReadLine());
        }

        internal static int EnterDifferentRTGS()
        {
            Console.Write("Enter RTGS Service Charges For Different Bank : ");
            return Convert.ToInt32(Console.ReadLine());
        }

        internal static int EnterDifferentIMPS()
        {
            Console.Write("Enter IMPS Service Charges For Different Bank : ");
            return Convert.ToInt32(Console.ReadLine());
        }

        internal static string EnterAccountID()
        {
            Console.Write("Enter Your Account ID: ");
            return Console.ReadLine();
        }
        internal static string EnterBankID()
        {
            Console.Write("Enter Your Bank ID: ");
            return Console.ReadLine();
        }
        public static string EnterBankStaffID()
        {
            Console.Write("Enter Your Bank Staff ID: ");
            return Console.ReadLine();
        }
        internal static string EnterTransactionID()
        {
            Console.Write("Enter Transaction ID: ");
            return Console.ReadLine();
        }

        internal static void PrintTransaction(Transaction transaction)
        {
            Console.Write(transaction.TransactionID + " : " + transaction.DateTime + "  " + transaction.Type + "  " + transaction.Currency.CurrencyCode + " " + transaction.Amount + "  ");
        }

        internal static void PrintLine()
        {
            Console.WriteLine();
        }

        internal static void PrintFromStatement(string name)
        {
            Console.Write(" from " + name);
        }

        internal static void PrintToStatement(string name)
        {
            Console.Write(" to " + name);
        }

        internal static double EnterAmount()
        {
            Console.Write("Enter Amount To Deposit: ");
            return Convert.ToDouble(Console.ReadLine());
        }
        internal static void PrintBalance(double balance)
        {
            Console.WriteLine("Your Available Balance : " + balance);
        }

        internal static void Invalid()
        {
            throw new InvalidChoice("Invalid Choice");
        }
        internal static void Exit()
        {
            Console.WriteLine("Successfully Exited");
        }
        internal static void CreateMessage(string accountID, string name, double amount)
        {
            Console.WriteLine("Account Created Successfully with Account No: " + accountID + " Name: " + name + " Balance: " + amount);
        }

        internal static double EnterWithDrawAmount()
        {
            Console.Write("Enter Amount To Withdraw: ");
            return Convert.ToDouble(Console.ReadLine());
        }

        internal static void TransactionHeading()
        {
            Console.WriteLine("\nTRANSACTION HISTORY\n ");
        }
        internal static void DepositMessage()
        {
            Console.WriteLine("Successfully Deposited");
        }
        internal static void WithDrawMessage()
        {
            Console.WriteLine("Successfully WithDrawn");
        }
        internal static void TransferMessage()
        {
            Console.WriteLine("Successfully Transferred");
        }

        internal static double EnterTransferAmount()
        {
            Console.Write("Enter Amount To Transfer: ");
            return Convert.ToDouble(Console.ReadLine());
        }
    }
}

