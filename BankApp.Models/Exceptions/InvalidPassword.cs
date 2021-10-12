using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Models.Exceptions
{
    public class InvalidPassword: Exception
    {
        public InvalidPassword(String message) : base(message)
        {
        }
    }
}
