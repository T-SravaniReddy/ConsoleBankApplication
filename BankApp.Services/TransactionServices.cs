using System;
using System.Collections.Generic;
using System.Text;
using BankApp.Models;

namespace BankApp.Services
{
     public class TransactionServices
    {
        private static List<Transaction> transactionList { set; get; }

        static TransactionServices()
        {
            
            transactionList = new List<Transaction>();
        }

        public static void AddTransaction(int accountID, int toID, string description, double amount, string datetime)
        {
            Transaction transaction = new Transaction(accountID, toID, description, amount, datetime);
            transactionList.Add(transaction);
        }
        public static void PrintTransactions(int accountID)
        {
            foreach (Transaction transaction in transactionList)
            {
                if (transaction.accountID == accountID)
                {
                    Console.Write(transaction.datetime + "  " + transaction.description + "  " + transaction.amount + "  ");
                    if (transaction.toID != -1)
                    {
                        if (transaction.description == "deposit")
                        {
                            Console.Write(" from " + AccountServices.GetName(transaction.toID));
                        }
                        else
                        {
                            Console.Write(" to " + AccountServices.GetName(transaction.toID));
                        }
                    }
                    Console.WriteLine();
                }
            }
        }

        public static void TransactionHistory()
        {
            int accountID = StandardMessages.EnterAccountID();
            if (AccountServices.Contains(accountID))
            {
                string pin = StandardMessages.EnterPIN();
                if (AccountServices.Validate(accountID, pin))
                {
                    StandardMessages.TransactionHeading();
                    PrintTransactions(accountID);
                }
                else
                {
                    StandardMessages.InvalidPIN();
                }
            }
            else
            {
                StandardMessages.AccountDoesntExist();
            }

        }
    }
}
