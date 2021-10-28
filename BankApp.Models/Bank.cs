using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Models
{
    public class Bank
    {
        public string BankID { get; set; }
        public string Name { get; set; }
        public int SameRTGS { get; set; }
        public int SameIMPS { get; set; }
        public int DifferentRTGS { get; set; }
        public int DifferentIMPS { get; set; }
        public List<BankStaff> StaffList { get; set; }
        public List<Currency> AcceptedCurrency { get; set; }
        public List<Account> AccountsList { set; get; }

        public Bank(string bankID, string name)
        {
            BankID = bankID;
            Name = name;
            SameRTGS = 0;
            SameIMPS = 5;
            DifferentRTGS = 2;
            DifferentIMPS = 6;
            StaffList = new List<BankStaff>();
            AcceptedCurrency = new List<Currency>();
            AccountsList = new List<Account>();
            AcceptedCurrency.Add(new Currency("INR", 1));
        }
    }
}
