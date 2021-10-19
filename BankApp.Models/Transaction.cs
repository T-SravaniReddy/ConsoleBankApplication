using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Models
{
    public class Transaction
    {
        public string TransactionID { get; set; }
        public string ToID { get; set; }
        public TransactionType Type { get; set; }
        public double Amount { get; set; }
        public DateTime Datetime { get; set; }

        public Transaction(string TransactionID, string toID, TransactionType type, double amount, DateTime datetime)
        {
            this.TransactionID = TransactionID;
            this.ToID = toID;
            this.Type = type;
            this.Amount = amount;
            this.Datetime = datetime;
        }
    }
}
