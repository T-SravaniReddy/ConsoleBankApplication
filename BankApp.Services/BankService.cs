using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BankApp.Models;
using BankApp.Models.Enums;
using Newtonsoft.Json.Linq;

namespace BankApp.Services
{
    public class BankService
    {
        public static List<Bank> BanksList { set; get; }
        static DateTime PresentDate = DateTime.Today;
        static BankService()
        {
            BanksList = new List<Bank>();
            string jsonFile = "C:/Users/RAM/source/repos/BankApplicationConsole/BankApp.Jsons/Bank.json";
            var json = File.ReadAllText(jsonFile);
            try
            {
                var jObject = JObject.Parse(json);

                if (jObject != null)
                {
                    string bankID = jObject["BankID"].ToString();
                    string name = jObject["Name"].ToString();
                    Bank bank = new Bank(bankID, name);
                    JArray staffArray = (JArray)jObject["StaffList"];
                    if (staffArray != null)
                    {
                        foreach (var item in staffArray)
                        {
                            BankStaff staff = new BankStaff(bankID, item["StaffID"].ToString(), item["Name"].ToString(), item["password"].ToString());
                            bank.StaffList.Add(staff);
                        }
                    }
                    JArray accountArray = (JArray)jObject["AccountsList"];
                    if (accountArray != null)
                    {
                        foreach (var item in accountArray)
                        {
                            Account account = new Account(item["AccountID"].ToString(), bankID,  item["Name"].ToString(), bank.AcceptedCurrency[0] ,item["Password"].ToString(), (Gender)Enum.Parse(typeof(Gender), item["Gender"].ToString()));
                           
                            bank.AccountsList.Add(account);
                        }
                    }


                    BanksList.Add(bank);
                }
            }
            catch (Exception)
            {
                throw;
            }

            }
        public static Bank AddBank(string name)
        {

            Bank bank = new Bank(BankIdPattern(name),name);
            BanksList.Add(bank);
            return bank;
        }
        private static string BankIdPattern(string Name)
        {
            return Name.Substring(0, 3) + PresentDate.ToString("dd") + PresentDate.ToString("MM") + PresentDate.ToString("yyyy");

        }

        public static Bank GetBank(string bankID)
        {
            Bank bank = BanksList.SingleOrDefault(m => m.BankID == bankID);
            return bank;
        }

        public static void AddCurrency(string bankID, Currency currency)
        {
            Bank bank = BanksList.SingleOrDefault(m => m.BankID == bankID);
            bank.AcceptedCurrency.Add(currency);
        }

        public static void ChangeSameBankService(string bankID, int sameRTGS, int sameIMPS)
        {
            Bank bank = BanksList.SingleOrDefault(m => m.BankID == bankID);
            bank.SameRTGS = sameRTGS;
            bank.SameIMPS = sameIMPS;
        }

        public static void ChangeDifferentBankService(string bankID, int differentRTGS, int differentIMPS)
        {
            Bank bank = BanksList.SingleOrDefault(m => m.BankID == bankID);
            bank.DifferentRTGS = differentRTGS;
            bank.DifferentIMPS = differentIMPS;
        }

        public static Currency GetCurrency(string bankID, string currencyCode)
        {
            Bank bank = BanksList.SingleOrDefault(m => m.BankID == bankID);
            Currency currency = bank.AcceptedCurrency.SingleOrDefault(m => m.CurrencyCode == currencyCode);
            return currency;
        }
    }
}
