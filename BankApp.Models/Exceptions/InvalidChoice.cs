using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Models.Exceptions
{
    public class InvalidChoice : Exception
    {
        public InvalidChoice(String message) : base(message)
        {
        }
    }
}
