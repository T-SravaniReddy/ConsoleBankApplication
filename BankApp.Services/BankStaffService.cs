using BankApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankApp.Services
{
    public class BankStaffService
    {
        static DateTime PresentDate = DateTime.Today;
        public static bool Validate(string bankID, string bankStaffID, string password)
        {
            Bank bank = BankService.GetBank(bankID);
            if (bank == null) return false;
            BankStaff staff = bank.StaffList.SingleOrDefault(m => m.StaffID == bankStaffID);
            if (staff != null)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public static string AddBankStaff(string bankID, string name, string password)
        {
            BankStaff staff = new BankStaff(bankID, GenerateStaffID( name), name, password);
            Bank bank = BankService.GetBank(bankID);
            bank.StaffList.Add(staff);
            return staff.StaffID;
        }

        private static string GenerateStaffID( string name)
        {
            return name.Substring(0, 3) + PresentDate.ToString("dd") + PresentDate.ToString("MM") + PresentDate.ToString("yyyy");
        }
    }
}
