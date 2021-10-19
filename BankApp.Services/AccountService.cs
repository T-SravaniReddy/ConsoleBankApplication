using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankApp.Models;
using BankApp.Models.Exceptions;

namespace BankApp.Services
{
    public class AccountService
    {

        static DateTime PresentDate = DateTime.Today;

        public static string AccountIdPattern(string Name)
        {
            return Name.Substring(0, 3) + PresentDate.ToString("dd") + PresentDate.ToString("MM") + PresentDate.ToString("yyyy");
        }
        public static Account AddAccount(Bank bank, string name, string pin)
        {
            
            Account account = new Account(AccountIdPattern(name), name, pin);
            bank.AccountsList.Add(account);
            return account;
        }
        public static bool Validate(Bank bank,string accountID, string pin)
        {
            Account account = bank.AccountsList.Single(m => m.AccountID == accountID);
            if (pin != account.Password)
            {
                return false;
            }
            return true;
        }
        private static bool CheckBalance(Account account, double amount)
        {
            if (account.Amount >= amount)
            {
                return true;
            }
            return false;
        }
        
        public static double Deposit(Bank bank, string accountID, string pin, double amount)
        {
            Account account = bank.AccountsList.Single(m => m.AccountID == accountID);
            if (account != null)
            {
                if (Validate(bank,accountID, pin))
                {
                    account.Amount = (account.Amount + amount);
                    TransactionService.AddTransaction(account, "-1", TransactionType.Credit, amount, DateTime.Now);
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
            return account.Amount;
        }

        public static double Withdraw(Bank bank, string accountID, string pin, double amount)
        {
            Account account = bank.AccountsList.Single(m => m.AccountID == accountID);
            if (account != null)
            {
                if (Validate(bank,accountID, pin))
                {
                    if (CheckBalance(account, amount))
                    {
                        account.Amount = (account.Amount - amount);
                        TransactionService.AddTransaction(account, "-1", TransactionType.Debit, amount, DateTime.Now);
                    }
                    else
                    {
                        throw new InsufficientAmount("Insufficient Amount");
                    }
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
            return account.Amount;
        }

        public static double TransferAmount(Bank bank, string accountID, string pin, double amount, string destinationID)
        {
            Account account = bank.AccountsList.Single(m => m.AccountID == accountID);
            if (account != null)
            {
                if (Validate(bank,accountID, pin))
                {
                    Account DestAccount = bank.AccountsList.Single(m => m.AccountID == destinationID);
                    if (DestAccount != null)
                    {
                        if (CheckBalance(account, amount))
                        {
                            account.Amount = (account.Amount - amount);
                            DestAccount.Amount = (DestAccount.Amount + amount);
                            TransactionService.AddTransaction(account, destinationID, TransactionType.Debit, amount, DateTime.Now);
                            TransactionService.AddTransaction(DestAccount, accountID, TransactionType.Credit, amount, DateTime.Now);
                        }
                        else
                        {
                            throw new InsufficientAmount("Insufficient Amount");
                        }
                    }
                    else
                    {
                        throw new InvalidAccount("Account Doesn't Exist");
                    }
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
            return account.Amount;
        }
        public static string GetName(Bank bank,string accountID)
        {
            return bank.AccountsList.Single(m => m.AccountID == accountID).Name;
        }
        public static Account Contains(Bank bank,string accountID)
        {
            return bank.AccountsList.Single(m => m.AccountID == accountID);
        }
        public static Account GetAccount(Bank bank,string accountID)
        {
            return bank.AccountsList.Single(m => m.AccountID == accountID);
        }
    }
}
