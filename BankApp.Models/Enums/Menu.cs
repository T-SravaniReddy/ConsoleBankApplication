using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Models.Enums
{
    public class Menu
    {
        public enum UserOptions
        {
            Deposit = 1,
            WithDraw,
            Transfer,
            AccountBalance,
            TransactionHistory,
            Exit
        }

        public enum StaffOptions
        {
            CreateNewAccount = 1,
            UpdateAccount,
            DeleteAccount,
            AddNewCurrency,
            SameBankServiceCharge,
            DifferentBankServiceCharge,
            AccountTransactionHistory,
            RevertTransaction,
            Exit
        }
    }
}
