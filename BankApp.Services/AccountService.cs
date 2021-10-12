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
        public static int AddAccount()
        {
            string name = StandardMessages.EnterUserName();
            string pin = StandardMessages.EnterPassword();
            Account account = new Account(++Index, name, pin);
            AccountsList.Add(account);
            StandardMessages.CreateMessage(account.AccountID, account.Name, account.Amount);
            return account.AccountID;
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
        
        public static void Deposit()
        {
            int accountID = StandardMessages.EnterAccountID();
            Account account = AccountsList.Single(m => m.AccountID == accountID);
            if (account != null)
            {
                string pin = StandardMessages.EnterPassword();
                if (Validate(accountID, pin))
                {
                    double amount = StandardMessages.EnterAmount();
                    account.Amount = (account.Amount + amount);
                    StandardMessages.DepositMessage();
                    StandardMessages.PrintBalance(account);
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

        }

        public static void Withdraw()
        {
            int accountID = StandardMessages.EnterAccountID(); 
            Account account = AccountsList.Single(m => m.AccountID == accountID);
            if (account != null)
            {
                string pin = StandardMessages.EnterPassword();
                if (Validate(accountID, pin))
                {
                    double amount = StandardMessages.EnterWithDrawAmount();
                    if (CheckBalance(account, amount))
                    {
                        account.Amount = (account.Amount - amount);
                        StandardMessages.WithDrawMessage();
                        StandardMessages.PrintBalance(account);
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

        }

        public static void TransferAmount()
        {
            int accountID = StandardMessages.EnterAccountID(); 
            Account account = AccountsList.Single(m => m.AccountID == accountID);
            if (account != null)
            {
                string pin = StandardMessages.EnterPassword();
                if (Validate(accountID, pin))
                {
                    int toID = StandardMessages.EnterAccountID();
                    Account DestAccount = AccountsList.Single(m => m.AccountID == toID);
                    if (DestAccount != null)
                    {
                        double amount = StandardMessages.EnterTransferAmount();
                        if (CheckBalance(account, amount))
                        {
                            account.Amount = (account.Amount - amount);
                            DestAccount.Amount = (DestAccount.Amount + amount);
                            StandardMessages.TransferMessage();
                            StandardMessages.PrintBalance(account);
                            TransactionService.AddTransaction(account, toID, "transfer", amount, DateTime.Now);
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
