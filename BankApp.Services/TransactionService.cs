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
        private static void PrintTransactions(int accountID)
        {
            Account account = AccountService.GetAccount(accountID);
            foreach (Transaction transaction in account.transactionList)
            {
                StandardMessages.PrintTransaction(transaction);
                if (transaction.ToID != -1)
                {
                    if (transaction.Description == "deposit")
                    {
                        StandardMessages.PrintFromStatement(AccountService.GetName(transaction.ToID));
                    }
                    else
                    {
                        StandardMessages.PrintToStatement(AccountService.GetName(transaction.ToID));
                    }
                }
                StandardMessages.PrintLine();
            }
        }

        public static void TransactionHistory()
        {
            int accountID = StandardMessages.EnterAccountID();
            if (AccountService.Contains(accountID) != null)
            {
                string pin = StandardMessages.EnterPassword();
                if (AccountService.Validate(accountID, pin))
                {
                    StandardMessages.TransactionHeading();
                    PrintTransactions(accountID);
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
