using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankApp.Models;
using BankApp.Models.Enums;
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
        public static Account AddAccount(string bankID, string name, string password, Gender gender, Currency currency)
        {
            Account account = new Account(AccountIdPattern(name), bankID, name,  currency, password, gender);
            Bank bank = BankService.GetBank(bankID);
            bank.AccountsList.Add(account);
            return account;
        }
        public static bool Validate(Bank bank,string accountID, string pin)
        {
            if (bank.AccountsList == null || bank == null) { return false; }
            Account account = bank.AccountsList.SingleOrDefault(m => m.AccountID == accountID);
            if (account == null) return false;
            if (pin != account.Password)
            {
                return false;
            }
            return true;
        }
        private static bool CheckBalance(Account account, double amount)
        {
            if (account.Balance >= amount)
            {
                return true;
            }
            return false;
        }

        public static bool DeleteAccount(string bankID, string accountID)
        {
            Bank bank = BankService.GetBank(bankID);
            if (bank.AccountsList.Remove(GetAccount(bank,accountID))) return true;
            return false;
        }

        public static double Deposit(Bank bank, string accountID, string pin, double amount)
        {
            Account account = bank.AccountsList.SingleOrDefault(m => m.AccountID == accountID);
            if (account != null)
            {
                if (Validate(bank,accountID, pin))
                {
                    account.Balance = (account.Balance + amount);
                    TransactionService.AddTransaction(bank.BankID, account, "-1", "-1", account.Currency, TransactionType.Credit, amount, DateTime.Now);
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
            return account.Balance;
        }

        public static string GetPassword(Bank bank,string accountID)
        {
            Account account = AccountService.GetAccount(bank, accountID);
            return account.Password;
        }

        public static double Withdraw(Bank bank, string accountID, string pin, double amount)
        {
            Account account = bank.AccountsList.SingleOrDefault(m => m.AccountID == accountID);
            if (account != null)
            {
                if (Validate(bank,accountID, pin))
                {
                    if (CheckBalance(account, amount))
                    {
                        account.Balance = (account.Balance - amount);
                        TransactionService.AddTransaction(bank.BankID, account,"-1", "-1",account.Currency, TransactionType.Debit, amount, DateTime.Now);
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
            return account.Balance;
        }

        public static string UpdatePassword(string bankID, string accountID, string password)
        {
            Bank bank = BankService.GetBank(bankID);
            Account account = AccountService.GetAccount(bank, accountID);
            if (account != null)
            {
                account.Password = password;
                return "Password Updated Successfully";
            }
            return "Invalid Account";
        }

        public static string UpdateCurrency(Bank bank, string accountID, string currencyCode)
        {
            Account account = AccountService.GetAccount(bank, accountID);
            if (account != null)
            {
                for (int i = 0; i < bank.AcceptedCurrency.Count; i++)
                {
                    if (bank.AcceptedCurrency[i].CurrencyCode == currencyCode)
                    {
                        account.Currency = bank.AcceptedCurrency[i];
                        return " Currency Updated Successfully";
                    }
                }
            }
            return "Invalid Account";
        }

        public static string UpdateName(string bankID, string accountID, string name)
        {
            Bank bank = BankService.GetBank(bankID);
            Account account = AccountService.GetAccount(bank, accountID);
            if (account != null)
            {
                account.Name = name;
                return "Name Updated Successfully";
            }
            return "Invalid Account";
        }

        public static double TransferAmount(Bank bank, string accountID, string pin, double amount,string destinationBankID, string destinationID, int option)
        {
            Account account = bank.AccountsList.SingleOrDefault(m => m.AccountID == accountID);
            double serviceCharge = 0;
            if (account != null)
            {
                if (Validate(bank,accountID, pin))
                {
                    Account DestAccount = bank.AccountsList.Single(m => m.AccountID == destinationID);
                    if (DestAccount != null)
                    {
                        if (bank.BankID == destinationBankID)
                        {
                            if (option == 1)
                            {
                                serviceCharge = (double)bank.SameRTGS * amount;
                            } else
                            {
                                serviceCharge = (double)bank.SameIMPS * amount;
                            }
                        } else
                        {
                            if (option == 1)
                            {
                                serviceCharge = (double)bank.DifferentRTGS * amount;
                            } else
                            {
                                serviceCharge = (double)bank.DifferentIMPS * amount;
                            }
                        }
                        if (CheckBalance(account, amount + serviceCharge))
                        {
                            account.Balance = (account.Balance - amount - serviceCharge);
                            DestAccount.Balance = (DestAccount.Balance + amount);
                            TransactionService.AddTransaction(bank.BankID, account,destinationBankID, destinationID, account.Currency, TransactionType.Debit, amount, DateTime.Now);
                            TransactionService.AddTransaction(destinationBankID, DestAccount, bank.BankID, accountID, DestAccount.Currency, TransactionType.Credit, amount, DateTime.Now);
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
            return account.Balance;
        }
        public static string GetName(Bank bank,string accountID)
        {
            return bank.AccountsList.SingleOrDefault(m => m.AccountID == accountID).Name;
        }
        public static Account Contains(Bank bank,string accountID)
        {
            return bank.AccountsList.SingleOrDefault(m => m.AccountID == accountID);
        }
        public static Account GetAccount(Bank bank,string accountID)
        {
            return bank.AccountsList.SingleOrDefault(m => m.AccountID == accountID);
        }
    }
}
