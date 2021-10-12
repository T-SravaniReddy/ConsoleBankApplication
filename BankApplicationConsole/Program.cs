using System;
using BankApp.Services;
using BankApp.Models;

namespace BankApplicationConsole
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Menu.UserOptions choice;
            AccountService accountServices = new AccountService();
            TransactionService transactionServices = new TransactionService();
            do
            {
                StandardMessages.WelcomeMessage();
                choice = (Menu.UserOptions) Enum.Parse(typeof(Menu.UserOptions), Console.ReadLine());

                switch (choice)
                {
                    case Menu.UserOptions.CreateAccount:
                        int accountID = AccountService.AddAccount();
                        break;

                    case Menu.UserOptions.Deposit:
                        try
                        {
                            AccountService.Deposit();
                        } catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        break;

                    case Menu.UserOptions.WithDraw:
                        try
                        {
                            AccountService.Withdraw();
                        } catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        break;

                    case Menu.UserOptions.Transfer:
                        try
                        {
                            AccountService.TransferAmount();
                        } catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        break;

                    case Menu.UserOptions.TransactionHistory:
                        try
                        {
                            TransactionService.TransactionHistory();
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