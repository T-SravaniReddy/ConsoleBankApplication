using System;
using System.Collections.Generic;
using System.Text;
using BankApp.Models;
using BankApp.Models.Exceptions;

namespace BankApp.Services
{
     public class TransactionService
    {
        static DateTime PresentDate = DateTime.Today;
        public static string TransactionIdGenerator(string bankID, string accountID)
        {
            return "TXN" + bankID + accountID + PresentDate.ToString("dd") + PresentDate.ToString("MM") + PresentDate.ToString("yyyy");
        }
        public static void AddTransaction(Account account, string toID, TransactionType type, double amount, DateTime datetime)
        {
            Transaction transaction = new Transaction(TransactionIdGenerator("0", account.AccountID),toID, type, amount, datetime);
            account.transactionList.Add(transaction);
        }
        
        public static List<Transaction> TransactionHistory(Bank bank,string accountID, string pin)
        {
            if (AccountService.Contains(bank,accountID) != null)
            {
                if (AccountService.Validate(bank,accountID, pin))
                {
                    Account account = AccountService.GetAccount(bank,accountID);
                    return account.transactionList;
                }
                else
                {
                    throw new InvalidPassword("Wrong/Invalid Password");
                }
            }
            else
            {
                throw new InvalidAccount("Account Doesn't Exist");
            }

        }
    }
}
