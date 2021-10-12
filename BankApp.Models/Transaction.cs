using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Models
{
    public class Transaction
    {
        public int ToID { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public DateTime Datetime { get; set; }

        public Transaction( int toID, string description, double amount, DateTime datetime)
        {
            this.ToID = toID;
            this.Description = description;
            this.Amount = amount;
            this.Datetime = datetime;
        }
    }
}
