using System;
using BankApp.Services;
using BankApp.Models;
using System.Collections.Generic;

namespace BankApplicationConsole
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Menu.UserOptions choice;
            AccountService accountServices = new AccountService();
            TransactionService transactionServices = new TransactionService();
            string pin;
            int accountID;
            double amount;
            do
            {
                StandardMessages.WelcomeMessage();
                choice = (Menu.UserOptions) Enum.Parse(typeof(Menu.UserOptions), Console.ReadLine());

                switch (choice)
                {
                    case Menu.UserOptions.CreateAccount:
                        string name = StandardMessages.EnterUserName();
                        pin = StandardMessages.EnterPassword();
                        Account account = AccountService.AddAccount(name, pin);
                        StandardMessages.CreateMessage(account.AccountID, account.Name, account.Amount);
                        break;

                    case Menu.UserOptions.Deposit:
                        accountID = StandardMessages.EnterAccountID();
                        pin = StandardMessages.EnterPassword();
                        amount = StandardMessages.EnterAmount();
                        try
                        { 
                            double balance = AccountService.Deposit(accountID, pin, amount);
                            StandardMessages.DepositMessage();
                            StandardMessages.PrintBalance(balance);
                        } catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        break;

                    case Menu.UserOptions.WithDraw:
                        accountID = StandardMessages.EnterAccountID();
                        pin = StandardMessages.EnterPassword();
                        amount = StandardMessages.EnterWithDrawAmount();
                        try
                        {
                            double balance = AccountService.Withdraw(accountID, pin, amount);
                            StandardMessages.WithDrawMessage();
                            StandardMessages.PrintBalance(balance);
                        } catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        break;

                    case Menu.UserOptions.Transfer:
                        accountID = StandardMessages.EnterAccountID();
                        pin = StandardMessages.EnterPassword();
                        int destinationID = StandardMessages.EnterAccountID();
                        amount = StandardMessages.EnterTransferAmount();
                        try
                        {
                            double balance = AccountService.TransferAmount(accountID, pin, amount, destinationID);
                            StandardMessages.TransferMessage();
                            StandardMessages.PrintBalance(balance);
                        } catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        break;

                    case Menu.UserOptions.TransactionHistory:
                        accountID = StandardMessages.EnterAccountID();
                        pin = StandardMessages.EnterPassword();
                        try
                        {
                            StandardMessages.TransactionHeading();
                            List<Transaction> transactionList = TransactionService.TransactionHistory(accountID, pin);
                            foreach (Transaction transaction in transactionList)
                            {
                                StandardMessages.PrintTransaction(transaction);
                                if (transaction.ToID != -1)
                                {
                                    if (transaction.Description == "deposit")
                                    {
                                        StandardMessages.PrintFromStatement(AccountService.GetName(transaction.ToID));
                                    }
                                    else
                                    {
                                        StandardMessages.PrintToStatement(AccountService.GetName(transaction.ToID));
                                    }
                                }
                                StandardMessages.PrintLine();
                            }
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

        }
    }
}