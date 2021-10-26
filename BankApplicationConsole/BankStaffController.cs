using BankApp.Models;
using BankApp.Models.Enums;
using BankApp.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApplicationConsole
{
    public class BankStaffController
    {
        public static void BankStaffChoice()
        {
            Menu.StaffOptions choice;
            string bankID = StandardMessages.EnterBankID();
            string bankStaffID = StandardMessages.EnterBankStaffID();
            string password = StandardMessages.EnterPassword();
            if (BankStaffService.Validate(bankID, bankStaffID, password))
            {
                string accountID, currencyCode, name;
                Currency currency;
                Bank bank;
                int i;
                Console.WriteLine();
                do
                {
                   
                    StandardMessages.StaffWelcomeMessage();
                    choice = (Menu.StaffOptions)Enum.Parse(typeof(Menu.StaffOptions), Console.ReadLine());

                    switch (choice)
                    {
                        case Menu.StaffOptions.CreateNewAccount:
                            name = StandardMessages.EnterUserName();
                            string accountPassword = StandardMessages.EnterPassword();
                            Gender gender = StandardMessages.EnterGender();
                            Console.Write("Enter Currency Among ");
                            bank = BankService.GetBank(bankID);
                            for (i = 0; i < bank.AcceptedCurrency.Count-1; i++)
                            {
                                Console.Write(bank.AcceptedCurrency[i].CurrencyCode + ", ");
                            }
                            Console.Write(bank.AcceptedCurrency[i].CurrencyCode + ":");
                            currencyCode = Console.ReadLine();
                            currency = BankService.GetCurrency(bankID, currencyCode);
                            AccountService.AddAccount(bankID, name, accountPassword, gender, currency);
                            break;

                        case Menu.StaffOptions.UpdateAccount:
                            Console.WriteLine("1. Update Account Holder Name");
                            Console.WriteLine("2. Update Account Currency");
                            Console.WriteLine("3. Update Account Password");
                            Console.Write("Choose Any Option : ");
                            int option = Convert.ToInt32(Console.ReadLine());
                            accountID = StandardMessages.EnterAccountID();
                            if (option == 1)
                            {
                                Console.Write("Enter New Name : ");
                                name = Console.ReadLine();
                                Console.WriteLine(AccountService.UpdateName(bankID, accountID, name));
                            } else if (option == 2)
                            {
                                Console.Write("Enter Currency Among ");
                                bank = BankService.GetBank(bankID);
                                for (i = 0; i < bank.AcceptedCurrency.Count - 1; i++)
                                {
                                    Console.Write(bank.AcceptedCurrency[i].CurrencyCode + ", ");
                                }
                                Console.Write(bank.AcceptedCurrency[i].CurrencyCode + ":");
                                currencyCode = Console.ReadLine();
                                Console.WriteLine(AccountService.UpdateCurrency(bank, accountID, currencyCode));
                            } else if (option == 3)
                            {
                                string newPassword = StandardMessages.EnterPassword();
                                Console.WriteLine(AccountService.UpdatePassword(bankID, accountID, password));
                            } else
                            {
                                Console.WriteLine("Invalid Selection");
                            }
                            break;
                        case Menu.StaffOptions.DeleteAccount:
                            accountID = StandardMessages.EnterAccountID();
                            if (AccountService.DeleteAccount(bankID, accountID)) Console.WriteLine("Account Deleted Successfully");
                            else Console.WriteLine("Something Went Wrong");
                            break;

                        case Menu.StaffOptions.AddNewCurrency:
                            currencyCode = StandardMessages.EnterCurrencyCode();
                            double equivalentINR = StandardMessages.EnterEquivalentINR();
                            currency = new Currency(currencyCode, equivalentINR);
                            BankService.AddCurrency(bankID, currency);
                            break;

                        case Menu.StaffOptions.SameBankServiceCharge:
                            int sameRTGS = StandardMessages.EnterSameRTGS();
                            int sameIMPS = StandardMessages.EnterSameIMPS();
                            if (sameIMPS <= 0 || sameRTGS <= 0) { Console.WriteLine("Invalid Charge"); break; }
                            BankService.ChangeSameBankService(bankID, sameRTGS, sameIMPS);
                            break;

                        case Menu.StaffOptions.DifferentBankServiceCharge:
                            int differentRTGS = StandardMessages.EnterDifferentRTGS();
                            int differentIMPS = StandardMessages.EnterDifferentIMPS();
                            if (differentIMPS <= 0 || differentRTGS <= 0) { Console.WriteLine("Invalid Charge"); break; }
                            BankService.ChangeDifferentBankService(bankID, differentRTGS, differentIMPS);
                            break;

                        case Menu.StaffOptions.AccountTransactionHistory:
                            accountID = StandardMessages.EnterAccountID();
                            AccountHolderController.TransactionHistory(BankService.GetBank(bankID), accountID, AccountService.GetPassword(BankService.GetBank(bankID),accountID));
                            break;

                        case Menu.StaffOptions.RevertTransaction:
                            accountID = StandardMessages.EnterAccountID();
                            string transactionID = StandardMessages.EnterTransactionID();
                            TransactionService.DeleteTransaction(bankID, accountID,transactionID);
                            break;

                        case Menu.StaffOptions.Exit:
                            try
                            {
                                StandardMessages.Exit();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;

                        default:
                            try
                            {
                                StandardMessages.Invalid();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;
                    }
                    Console.WriteLine("\n");
                } while (choice != Menu.StaffOptions.Exit);
            } else
            {
                Console.WriteLine("Invalid Credentials");
            }
        }
    }
}
