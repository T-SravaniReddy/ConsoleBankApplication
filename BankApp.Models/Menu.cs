using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Models
{
    public class Menu
    {
        public enum UserOptions
        {
            CreateAccount = 1,
            Deposit,
            WithDraw,
            Transfer,
            TransactionHistory,
            Exit
        }
    }
}
