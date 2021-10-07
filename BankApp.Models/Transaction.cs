using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Models
{
    public class Transaction
    {
        public int accountID { get; set; }
        public int toID { get; set; }
        public string description { get; set; }
        public double amount { get; set; }
        public string datetime { get; set; }

        public Transaction(int accountID, int toID, string description, double amount, string datetime)
        {
            this.accountID = accountID;
            this.toID = toID;
            this.description = description;
            this.amount = amount;
            this.datetime = datetime;
        }
    }
}
