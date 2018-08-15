
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SenderFront
{
    internal class SenderFrontEnd
    {
        /// <summary>
        /// The ip address to send to
        /// </summary>
        IPAddress iPAddress;
        /// <summary>
        /// The port number to use
        /// </summary>
        int portNumber;
        /// <summary>
        /// Do you encrypt the connection?
        /// </summary>
        bool encryptConnection;

        internal SenderFrontEnd(IPAddress address, int port, bool encrypt = true)
        {
            // Set all variables
            iPAddress = address;
            portNumber = port;
            encryptConnection = encrypt;
        }

        internal int StartSendBytes(byte[] bytesToSend)
        {
            int code = 0;
            // Check if internet is currently avaliable
            if (IsInternetAvailable())
            {
                // Check if we need to encrypt
                if (encryptConnection)
                {
                    code = SendBytesEncrypted(bytesToSend);
                }
                else
                {
                    code = SendBytes(bytesToSend);
                }
            }
            // Otherwise code is 1
            else code = 1;

            return code;
        }

        private int SendBytes(byte[] bytesToSend)
        {

            return 0;
        }

        private int SendBytesEncrypted(byte[] bytesToSend)
        {
            return 0;
        }

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);

        /// <summary>
        /// Check for internet connection
        /// </summary>
        /// <returns>True if there is network connection, False if no connection</returns>
        private static bool IsInternetAvailable()
        {
            // First check if you have local internet (e.g. WiFi enabled / Lan pluged in)
            int description;
            bool localInternetAvaliable = InternetGetConnectedState(out description, 0);
            // If you have it, then check google to check internet with proxie's or vpn's
            if (localInternetAvaliable)
            {
                // If google responded return true
                try
                {
                    using (var client = new WebClient())
                    using (client.OpenRead("http://g.cn/generate_204"))
                    {
                        return true;
                    }
                }
                // Else return false
                catch
                {
                    return false;
                }
            }
            // If no local internet return false
            else return false;
        }
    }
}
