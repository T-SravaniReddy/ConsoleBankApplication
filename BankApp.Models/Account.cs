using System;

namespace BankApp.Models
{
    public class Account
    {
        private static int index = 0; 
        public int accountID { get; set; }
        public string name { get; set; }
        public double amount { get; set; }
        public string pin { get; set; }


        public Account(string name, string pin)
        {
            this.accountID = ++index;
            this.pin = pin;
            this.name = name;
            this.amount = 0;
        }

    }
}
