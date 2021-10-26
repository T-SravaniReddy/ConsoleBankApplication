using BankApp.Models.Enums;
using System;
using System.Collections.Generic;

namespace BankApp.Models
{
    public class Account
    { 
        public string AccountID { get; set; }
        public string BankID { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }
        public Currency Currency { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }
        public List<Transaction> TransactionList { set; get; }

        public Account(string accountID, string bankID, string name, Currency currency, string password, Gender gender)
        {
            AccountID = accountID;
            BankID = bankID;
            Name = name;
            Balance = 0;
            Currency = currency;
            Password = password;
            Gender = gender;
            this.TransactionList = new List<Transaction>();
        }
    }
}
