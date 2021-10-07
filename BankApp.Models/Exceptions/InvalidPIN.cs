using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Models.Exceptions
{
    public class InvalidPIN: Exception
    {
        public InvalidPIN(String message) : base(message)
        {
        }
    }
}
