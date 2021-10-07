using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Models.Exceptions
{
    public class InsufficientAmount: Exception
    {
        public InsufficientAmount(String message) : base(message)
        {
        }
    }
}
