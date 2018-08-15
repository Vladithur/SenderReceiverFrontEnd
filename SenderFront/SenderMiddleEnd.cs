using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SenderFront
{
    internal class SenderMiddleEnd
    {
        /// <summary>
        /// Use to acess SenderFrontEnd functions
        /// </summary>
        SenderFrontEnd senderFrontEnd { get; set; }
        /// <summary>
        /// The ip address to send to
        /// </summary>
        IPAddress iPAddress { get; set; }
        /// <summary>
        /// The port number to use
        /// </summary>
        int portNumber { get; set; }
        /// <summary>
        /// Do you encrypt the connection?
        /// </summary>
        bool encryptConnection { get; set; }

        internal SenderMiddleEnd(IPAddress address, int port, bool encrypt = true)
        {
            // Set all variables
            senderFrontEnd = new SenderFrontEnd(address, port, encrypt);
            iPAddress = address;
            portNumber = port;
            encryptConnection = encrypt;
        }
        /// <summary>
        /// Send some text to ip address 'iPAddress' to port number "portNumber"
        /// </summary>
        /// <param name="text">The text to send</param>
        /// <returns>If all successful return 0, otherwise return error code</returns>
        /* Error codes: (Note: will do later)
         * 1: No connection
         * 2: No connection from start
         * 3: Got no responce
         * 4: Got incorrect responce
         * 5: Encryption failed
         * 6: Unhandled exception
        */
        internal int SendText(string text)
        {
            // T for text
            text = "T" + text;
            // Convert to bytes
            byte[] bytesToSend = Encoding.Unicode.GetBytes(text);
            // Send text
            int code = senderFrontEnd.StartSendBytes(bytesToSend);
            // Return code
            return code;
        }

        /// <summary>
        /// Send a file to ip address 'iPAddress' to port number "portNumber"
        /// </summary>
        /// <param name="fileBytes"> The bytes of the file to send</param>
        /// <returns>If all successful return 0, otherwise return error code</returns>
        internal int SendFile(byte[] fileBytes)
        {
            // F for file and convert it to bytes
            byte[] fSymbolByte = Encoding.Unicode.GetBytes("F");
            // Create new array for concatenation
            byte[] bytesToSend = new byte[fSymbolByte.Length + fileBytes.Length];
            // Concatenate
            fSymbolByte.CopyTo(bytesToSend, 0);
            fileBytes.CopyTo(bytesToSend, fSymbolByte.Length);
            // Send file
            int code = senderFrontEnd.StartSendBytes(bytesToSend);
            // Return error code
            return code;
        }
    }
}
