using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Models
{
    public class Bank
    {
        public string BankID { get; set; }
        public string Name { get; set; }

        public List<Account> AccountsList { set; get; }
        public Bank(string BankId, string Name)
        {
            this.BankID = BankID;
            this.Name = Name;
            this.AccountsList = new List<Account>();
        }
    }
}
