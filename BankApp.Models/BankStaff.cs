using BankApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Models
{
    public class BankStaff
    {
       

        public string BankID { get; set; }
        public string StaffID { get; set; }
        public string Name { get; set; }
        public string password { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailID { get; set; }
        public BankStaff(string bankID, string staffID, string name, string password)
        {
            BankID = bankID;
            StaffID = staffID;
            Name = name;
            this.password = password;
        }
    }
}
