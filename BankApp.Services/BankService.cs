using System;
using System.Collections.Generic;
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
    }
}
