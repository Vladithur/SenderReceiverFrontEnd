using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;

namespace SenderFront
{
    public class Sender
    {
        /// <summary>
        /// Use to acess SenderBackEndHandler functions.
        /// </summary>
        private SenderBackEndHandler senderBackEndHandler;
        /// <summary>
        /// the ip address to send to.
        /// </summary>
        private IPAddress iPAddress;
        /// <summary>
        /// The port number to use.
        /// </summary>
        private int portNumber;
        /// <summary>
        /// Do you encrypt the connection?
        /// </summary>
        private bool encryptConnection;

        public Sender(IPAddress address, int port, bool encrypt = true)
        {
            // Set all variables
            senderBackEndHandler = new SenderBackEndHandler(address, port, encrypt);
            iPAddress = address;
            portNumber = port;
            encryptConnection = encrypt;
        }

        /// <summary>
        /// Send some text 'text' to ip address "IP" to port number "PortNumber".
        /// </summary>
        /// <param name="text">The text to send.</param>
        /// <returns>Return 0 if successful, otherwise return error code.</returns>
        public int SendText(string text)
        {
            // Send text
            int code = senderBackEndHandler.SendText(text);
            // Return error code
            return code;
        }

        /// <summary>
        /// Send file with path 'path' to ip address "IP" to port number "PortNumber".
        /// </summary>
        /// <param name="path">The path to the file to send.</param>
        /// <returns>Return 0 if successful, otherwise return error code.</returns>
        public int SendFile(string path)
        {
            // Send file
            int code = senderBackEndHandler.SendFile(path);
            // Return error code
            return code;
        }

        /// <summary>
        /// Get and Set the port number.
        /// Note: Setting the port number restarts the connection.
        /// </summary>
        public int PortNumber
        {
            get => portNumber;
            set { portNumber = value; UpdateHandler(iPAddress, portNumber, encryptConnection); }
        }
        /// <summary>
        /// Get and Set the IP address.
        /// Note: Setting the IP address restarts the connection.
        /// </summary>
        public IPAddress IP
        {
            get => iPAddress;
            set { iPAddress = value; UpdateHandler(iPAddress, portNumber, encryptConnection); }
        }

        /// <summary>
        /// Get and Set the encryption status.
        /// Note: Setting the encryption status restarts the connection.
        /// </summary>
        public bool EncryptConnection
        {
            get => encryptConnection;
            set { encryptConnection = value; UpdateHandler(iPAddress, portNumber, encryptConnection); }
        }

        /// <summary>
        /// Use to update senderBackEndHandler when "IP", "PortNumber" or "EncryptConnection" where changed.
        /// </summary>
        /// <param name="address">The new ip address.</param>
        /// <param name="port">The new port numbber.</param>
        /// <param name="encrypt">The new encryption status.</param>
        /// <returns>Return 0 if successful, 1 if not.</returns>
        private int UpdateHandler(IPAddress address, int port, bool encrypt = true)
        {
            // Try to update, if successful return 0
            try
            {
                senderBackEndHandler = new SenderBackEndHandler(address, port, encrypt);
                return 0;
            }
            // Else return 1
            catch
            {
                return 1;
            }
        }

        /* For future use, currently no need to foucs on/// <summary>
        /// Send some text 'text' to ip address "IP" to port number "PortNumber" asynchronously
        /// </summary>
        /// <param name="text">The text to send</param>
        /// <returns>Return 0 if successful, otherwise return error code</returns>
        public int SendTextAsync(string text)
        {
            int code = senderBackEndHandler.SendTextAsync(text);
            // Return error code
            return code;
        }*/
    }
}
