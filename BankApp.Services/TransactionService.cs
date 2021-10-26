using System;
using System.Collections.Generic;
using System.Text;
using BankApp.Models;
using BankApp.Models.Exceptions;
using BankApp.Models.Enums;
using System.Linq;

namespace BankApp.Services
{
     public class TransactionService
    {
        static DateTime PresentDate = DateTime.Today;
        public static string TransactionIdGenerator(string bankID, string accountID)
        {
            return "TXN" + bankID + accountID + PresentDate.ToString("dd") + PresentDate.ToString("MM") + PresentDate.ToString("yyyy");
        }
        public static void AddTransaction(string bankID, Account account, string destinationBankID, string destinationID,Currency currency,  TransactionType type, double amount, DateTime datetime)
        {
            Transaction transaction = new Transaction(TransactionIdGenerator(bankID, account.AccountID),bankID, account.AccountID, destinationBankID, destinationID, currency, type, amount, datetime);
            account.TransactionList.Add(transaction);
        }
        
        public static List<Transaction> TransactionHistory(Bank bank,string accountID, string pin)
        {
            if (AccountService.Contains(bank,accountID) != null)
            {
                if (AccountService.Validate(bank,accountID, pin))
                {
                    Account account = AccountService.GetAccount(bank,accountID);
                    return account.TransactionList;
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

        public static void DeleteTransaction(string bankID, string accountID, string transactionID)
        {
            Bank bank = BankService.GetBank(bankID);
            Account account = AccountService.GetAccount(bank,accountID);
            List<Transaction> transactionList = account.TransactionList;

            Transaction transaction = transactionList.SingleOrDefault(m => m.TransactionID == transactionID);
            if (transaction.Type == TransactionType.Credit) {
                AddTransaction(bankID, account, transaction.DestinationBankID, transaction.DestinationAccountID, transaction.Currency, TransactionType.Debit, transaction.Amount, DateTime.Now);
                Account destAccount = AccountService.GetAccount(BankService.GetBank(transaction.DestinationBankID), transaction.DestinationAccountID);
                AddTransaction(transaction.DestinationBankID, destAccount, bankID, accountID, transaction.Currency, TransactionType.Credit, transaction.Amount, DateTime.Now);
            }
            else
            {
                AddTransaction(bankID, account, transaction.DestinationBankID, transaction.DestinationAccountID, transaction.Currency, TransactionType.Credit, transaction.Amount, DateTime.Now);
                Account destAccount = AccountService.GetAccount(BankService.GetBank(transaction.DestinationBankID), transaction.DestinationAccountID);
                AddTransaction(transaction.DestinationBankID, destAccount, bankID, accountID, transaction.Currency, TransactionType.Debit, transaction.Amount, DateTime.Now);
            }
        }
    }
}
