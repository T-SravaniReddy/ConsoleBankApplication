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

        private static List<Account> AccountsList { set; get; }
        private static int Index = 0;
        
        static AccountService()
        {
            AccountsList = new List<Account>();
        }
        public static Account AddAccount(string name, string pin)
        {
            
            Account account = new Account(++Index, name, pin);
            AccountsList.Add(account);
            return account;
        }
        public static bool Validate(int accountID, string pin)
        {
            Account account = AccountsList.Single(m => m.AccountID == accountID);
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
        
        public static double Deposit(int accountID, string pin, double amount)
        {
            Account account = AccountsList.Single(m => m.AccountID == accountID);
            if (account != null)
            {
                if (Validate(accountID, pin))
                {
                    account.Amount = (account.Amount + amount);
                    TransactionService.AddTransaction(account, -1, "deposit", amount, DateTime.Now);
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

        public static double Withdraw(int accountID, string pin, double amount)
        {
            Account account = AccountsList.Single(m => m.AccountID == accountID);
            if (account != null)
            {
                if (Validate(accountID, pin))
                {
                    if (CheckBalance(account, amount))
                    {
                        account.Amount = (account.Amount - amount);
                        TransactionService.AddTransaction(account, -1, "withdraw", amount, DateTime.Now);
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

        public static double TransferAmount(int accountID, string pin, double amount, int destinationID)
        {
            Account account = AccountsList.Single(m => m.AccountID == accountID);
            if (account != null)
            {
                if (Validate(accountID, pin))
                {
                    Account DestAccount = AccountsList.Single(m => m.AccountID == destinationID);
                    if (DestAccount != null)
                    {
                        if (CheckBalance(account, amount))
                        {
                            account.Amount = (account.Amount - amount);
                            DestAccount.Amount = (DestAccount.Amount + amount);
                            TransactionService.AddTransaction(account, destinationID, "transfer", amount, DateTime.Now);
                            TransactionService.AddTransaction(DestAccount, accountID, "deposit", amount, DateTime.Now);
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
        public static string GetName(int accountID)
        {
            return AccountsList.Single(m => m.AccountID == accountID).Name;
        }
        public static Account Contains(int accountID)
        {
            return AccountsList.Single(m => m.AccountID == accountID);
        }
        public static Account GetAccount(int accountID)
        {
            return AccountsList.Single(m => m.AccountID == accountID);
        }
    }
}
