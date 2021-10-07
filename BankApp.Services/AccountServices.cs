using System;
using System.Collections.Generic;
using System.Text;
using BankApp.Models;

namespace BankApp.Services
{
    public class AccountServices
    {

        private static Dictionary<int, Account> AccountsList { set; get; }
        
        static AccountServices()
        {
            AccountsList = new Dictionary<int, Account>();
        }
        public static void AddAccount()
        {
            string name = StandardMessages.EnterUserName();
            string pin = StandardMessages.EnterPIN();
            Account account = new Account(name, pin);
            AccountsList.Add(account.accountID, account);
            StandardMessages.CreateMessage(account.accountID, account.name, account.amount);
        }
        public static bool Validate(int accountID, string pin)
        {
            if (pin != AccountsList[accountID].pin)
            {
                return false;
            }
            return true;
        }
        public static void PrintBalance(Account account)
        {
            Console.WriteLine("Your Available Balance : " + account.amount);
        }
        public static bool CheckBalance(Account account, double amount)
        {
            if (account.amount >= amount)
            {
                return true;
            }
            return false;
        }
        
        public static void Deposit()
        {
            int accountID = StandardMessages.EnterAccountID();
            if (AccountsList.ContainsKey(accountID))
            {
                string pin = StandardMessages.EnterPIN();
                if (Validate(accountID, pin))
                {
                    double amount = StandardMessages.EnterAmount();
                    Account account = AccountsList[accountID];
                    account.amount = (account.amount + amount);
                    StandardMessages.DepositMessage();
                    PrintBalance(account);
                    TransactionServices.AddTransaction(accountID, -1, "deposit", amount, DateTime.Now.ToString("MM/dd/yyyy h:mm tt"));
                }
                else
                {
                    try
                    {
                        StandardMessages.InvalidPIN();
                    } catch(Exception ex) {
                        Console.WriteLine(ex);
                    } 
                    
                }
            }
            else
            {
                try
                {
                    StandardMessages.AccountDoesntExist();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

        }

        public static void Withdraw()
        {
            int accountID = StandardMessages.EnterAccountID();
            if (AccountsList.ContainsKey(accountID))
            {
                string pin = StandardMessages.EnterPIN();
                if (Validate(accountID, pin))
                {
                    double amount = StandardMessages.EnterWithDrawAmount();
                    Account account = AccountsList[accountID];
                    if (CheckBalance(account, amount))
                    {
                        account.amount = (account.amount - amount);
                        StandardMessages.WithDrawMessage();
                        PrintBalance(account);
                        TransactionServices.AddTransaction(accountID, -1, "withdraw", amount, DateTime.Now.ToString("MM/dd/yyyy h:mm tt"));
                    }
                    else
                    {
                        try
                        {
                            StandardMessages.InsufficientAmount();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        
                    }

                }
                else
                {
                    try
                    {
                        StandardMessages.InvalidPIN();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    
                }
            }
            else
            {
                try
                {
                    StandardMessages.AccountDoesntExist();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                
            }

        }

        public static void TransferAmount()
        {
            int accountID = StandardMessages.EnterAccountID();
            if (AccountsList.ContainsKey(accountID))
            {
                string pin = StandardMessages.EnterPIN();
                if (Validate(accountID, pin))
                {
                    int toID = StandardMessages.EnterAccountID();
                    if (AccountsList.ContainsKey(toID))
                    {
                        double amount = StandardMessages.EnterTransferAmount();
                        Account account = AccountsList[accountID];
                        if (CheckBalance(account, amount))
                        {
                            account.amount = (account.amount - amount);
                            AccountsList[toID].amount = (AccountsList[toID].amount + amount);
                            StandardMessages.TransferMessage();
                            PrintBalance(account);
                            TransactionServices.AddTransaction(accountID, toID, "transfer", amount, DateTime.Now.ToString("MM/dd/yyyy h:mm tt"));
                            TransactionServices.AddTransaction(toID, accountID, "deposit", amount, DateTime.Now.ToString("MM/dd/yyyy h:mm tt"));
                        }
                        else
                        {
                            try
                            {
                                StandardMessages.InsufficientAmount();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            
                        }
                    }
                    else
                    {
                        try
                        {
                            StandardMessages.AccountDoesntExist();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        
                    }
                }
                else
                {
                    try
                    {
                        StandardMessages.InvalidPIN();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    } 
                }
            }
            else
            {
                try
                {
                    StandardMessages.AccountDoesntExist();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
        public static string GetName(int accountID)
        {
            return AccountsList[accountID].name;
        }
        public static bool Contains(int accountID)
        {
            return AccountsList.ContainsKey(accountID);
        }
    }
}
