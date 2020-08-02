/* 
    Author : LimerBoy
    Github : github.com/LimerBoy/Adamantium-Thief
*/

namespace Stealer
{
    internal sealed class Common
    {
        public struct Password
        {
            public string hostname { get; set; }
            public string username { get; set; }
            public string password { get; set; }
        }

        internal struct Cookie
        {
            public string hostname { get; set; }
            public string name { get; set; }
            public string path { get; set; }
            public string expiresutc { get; set; }
            public string key { get; set; }
            public string value { get; set; }
            public string issecure { get; set; }
        }

        internal struct CreditCard
        {
            public string number { get; set; }
            public string expyear { get; set; }
            public string expmonth { get; set; }
            public string name { get; set; }
        }

        internal struct AutoFill
        {
            public string name;
            public string value;
        }

        internal struct Site
        {
            public string hostname { get; set; }
            public string title { get; set; }
            public string date { get; set; }
            public int visits { get; set; }
        }

        internal struct Bookmark
        {
            public string hostname { get; set; }
            public string title { get; set; }
            public string added { get; set; }
        }
    }
}
