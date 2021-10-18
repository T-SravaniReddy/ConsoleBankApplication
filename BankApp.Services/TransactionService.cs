using System;
using System.Collections.Generic;
using System.Text;
using BankApp.Models;
using BankApp.Models.Exceptions;

namespace BankApp.Services
{
     public class TransactionService
    {

        public static void AddTransaction(Account account, int toID, string description, double amount, DateTime datetime)
        {
            Transaction transaction = new Transaction(toID, description, amount, datetime);
            account.transactionList.Add(transaction);
        }
        
        public static List<Transaction> TransactionHistory(int accountID, string pin)
        {
            if (AccountService.Contains(accountID) != null)
            {
                if (AccountService.Validate(accountID, pin))
                {
                    Account account = AccountService.GetAccount(accountID);
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
