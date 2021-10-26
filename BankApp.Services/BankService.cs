using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankApp.Models;

namespace BankApp.Services
{
    public class BankService
    {
        public static List<Bank> BanksList { set; get; }
        static DateTime PresentDate = DateTime.Today;
        static BankService()
        {
            BanksList = new List<Bank>();
        }
        public static Bank AddBank(string name)
        {

            Bank bank = new Bank(BankIdPattern(name),name);
            BanksList.Add(bank);
            return bank;
        }
        private static string BankIdPattern(string Name)
        {
            return Name.Substring(0, 3) + PresentDate.ToString("dd") + PresentDate.ToString("MM") + PresentDate.ToString("yyyy");

        }

        public static Bank GetBank(string bankID)
        {
            Bank bank = BanksList.SingleOrDefault(m => m.BankID == bankID);
            return bank;
        }

        public static void AddCurrency(string bankID, Currency currency)
        {
            Bank bank = BanksList.SingleOrDefault(m => m.BankID == bankID);
            bank.AcceptedCurrency.Add(currency);
        }

        public static void ChangeSameBankService(string bankID, int sameRTGS, int sameIMPS)
        {
            Bank bank = BanksList.SingleOrDefault(m => m.BankID == bankID);
            bank.SameRTGS = sameRTGS;
            bank.SameIMPS = sameIMPS;
        }

        public static void ChangeDifferentBankService(string bankID, int differentRTGS, int differentIMPS)
        {
            Bank bank = BanksList.SingleOrDefault(m => m.BankID == bankID);
            bank.DifferentRTGS = differentRTGS;
            bank.DifferentIMPS = differentIMPS;
        }

        public static Currency GetCurrency(string bankID, string currencyCode)
        {
            Bank bank = BanksList.SingleOrDefault(m => m.BankID == bankID);
            Currency currency = bank.AcceptedCurrency.SingleOrDefault(m => m.CurrencyCode == currencyCode);
            return currency;
        }
    }
}
