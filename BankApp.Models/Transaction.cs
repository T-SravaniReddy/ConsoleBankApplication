using BankApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Models
{
    public class Transaction
    {
        public string TransactionID { get; set; }
        public string SourceBankID { get; set; }
        public string SourceAccountID { get; set; }
        public string DestinationBankID { get; set; }
        public string DestinationAccountID { get; set; }
        public Currency Currency { get; set; }
        public TransactionType Type { get; set; }
        public double Amount { get; set; }
        public DateTime DateTime { get; set; }

        public Transaction(string transactionID, string sourceBankID, string sourceAccountID, string destinationBankID, string destinationAccountID, Currency currency, TransactionType type, double amount, DateTime dateTime)
        {
            TransactionID = transactionID;
            SourceBankID = sourceBankID;
            SourceAccountID = sourceAccountID;
            DestinationBankID = destinationBankID;
            DestinationAccountID = destinationAccountID;
            Currency = currency;
            Type = type;
            Amount = amount;
            DateTime = dateTime;
        }
    }
}
