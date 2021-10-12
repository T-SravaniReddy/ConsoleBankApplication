using System;
using System.Collections.Generic;

namespace BankApp.Models
{
    public class Account
    { 
        public int AccountID { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public string Password { get; set; }

        public List<Transaction> transactionList { set; get; }

        public Account(int AccountID, string Name, string password)
        {
            this.AccountID = AccountID;
            this.Password = password;
            this.Name = Name;
            this.Amount = 0;
            this.transactionList = new List<Transaction>();
        }

    }
}
