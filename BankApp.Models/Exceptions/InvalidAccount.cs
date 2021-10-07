using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Models.Exceptions
{
    public class InvalidAccount : Exception
    {
        public InvalidAccount(String message) : base(message)
        {
        }
    }
}
