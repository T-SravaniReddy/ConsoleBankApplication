using BankApp.Models;
using BankApp.Models.Enums;
using BankApp.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApplicationConsole
{
    public class AccountHolderController
    {
        static public void AccountHolderChoice()
        {
            Menu.UserOptions choice;
            string pin;
            string accountID, bankID;
            double amount;
            bankID = StandardMessages.EnterBankID();
            accountID = StandardMessages.EnterAccountID();
            pin = StandardMessages.EnterPassword();
            Bank bank = BankService.GetBank(bankID);
            string currencyCode;
            Currency currency;
            if (AccountService.Validate(bank, accountID, pin) == true)
            {
                Console.WriteLine();
                do
                {
                    StandardMessages.WelcomeMessage();
                    choice = (Menu.UserOptions)Enum.Parse(typeof(Menu.UserOptions), Console.ReadLine());

                    switch (choice)
                    {
                        case Menu.UserOptions.Deposit:
                            currencyCode = StandardMessages.EnterCurrency(bank);
                            amount = StandardMessages.EnterAmount();
                            if (amount <= 0) { Console.WriteLine("Invalid Amount"); break; }
                            currency = BankService.GetCurrency(bankID, currencyCode);
                            if (currency != null)
                            {
                                try
                                {
                                    double balance = AccountService.Deposit(bank, accountID, pin, amount, currency);
                                    StandardMessages.DepositMessage();
                                    StandardMessages.PrintBalance(balance);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                            }
                            else Console.WriteLine("Invalid Currency");
                            break;

                        case Menu.UserOptions.WithDraw:
                            currencyCode = StandardMessages.EnterCurrency(bank);
                            amount = StandardMessages.EnterWithDrawAmount();
                            if (amount <= 0) { Console.WriteLine("Invalid Amount"); break; }
                            currency = BankService.GetCurrency(bankID, currencyCode);
                            if (currency != null)
                            {
                                try
                                {
                                    double balance = AccountService.Withdraw(bank, accountID, pin, amount, currency);
                                    StandardMessages.WithDrawMessage();
                                    StandardMessages.PrintBalance(balance);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                            } else Console.WriteLine("Invalid Currency");
                            break;

                        case Menu.UserOptions.Transfer:
                            string destinationBankID = StandardMessages.EnterDestinationBankID();
                            string destinationID = StandardMessages.EnterDestinationAccountID();
                            amount = StandardMessages.EnterTransferAmount();
                            if (amount <= 0) { Console.WriteLine("Invalid Amount"); break; }
                            int option = StandardMessages.EnterService();
                            try
                            {
                                double balance = AccountService.TransferAmount(bank, accountID, pin, amount, destinationBankID, destinationID, option);
                                StandardMessages.TransferMessage();
                                StandardMessages.PrintBalance(balance);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;

                        case Menu.UserOptions.TransactionHistory:
                            try
                            {
                                TransactionHistory(bank, accountID, pin);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;

                        case Menu.UserOptions.Exit:
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
                } while (choice != Menu.UserOptions.Exit);
            } else
            {
                Console.WriteLine("Invalid Credentials");
            }
        }
        static internal void TransactionHistory(Bank bank, string accountID, string pin)
        {
            StandardMessages.TransactionHeading();
            List<Transaction> transactionList = TransactionService.TransactionHistory(bank, accountID, pin);
            foreach (Transaction transaction in transactionList)
            {
                StandardMessages.PrintTransaction(transaction);
                if (transaction.DestinationAccountID != "-1")
                {
                    if (transaction.Type == TransactionType.Credit)
                    {
                        StandardMessages.PrintFromStatement(AccountService.GetName(bank, transaction.DestinationAccountID));
                    }
                    else
                    {
                        StandardMessages.PrintToStatement(AccountService.GetName(bank, transaction.DestinationAccountID));
                    }
                }
                StandardMessages.PrintLine();
            }
        }
    }
}
