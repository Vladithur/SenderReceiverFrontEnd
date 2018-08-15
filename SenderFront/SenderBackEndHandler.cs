using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace SenderFront
{
    internal class SenderBackEndHandler
    {
        /// <summary>
        /// Use to acess SenderMiddleEnd functions.
        /// </summary>
        SenderMiddleEnd senderMiddleEnd { get; set; }
        /// <summary>
        /// the ip address to send to.
        /// </summary>
        IPAddress iPAddress { get; set; }
        /// <summary>
        /// The port number to use.
        /// </summary>
        int portNumber { get; set; }
        /// <summary>
        /// Do you encrypt the connection?
        /// </summary>
        bool encryptConnection { get; set; }


        internal SenderBackEndHandler(IPAddress address, int port, bool encrypt = true)
        {
            // Set all variables
            var sender = new Sender(address, port);
            senderMiddleEnd = new SenderMiddleEnd(address, port, encrypt);
            iPAddress = address;
            portNumber = port;
            encryptConnection = encrypt;
        }

        /// <summary>
        /// Send some text 'text' to ip address "iPAddress" to port "portNumber".
        /// </summary>
        /// <param name="text">The text to send.</param>
        /// <returns>If all successful return 0, otherwise return error code.</returns>
        /* Error codes: (Note: will do later)
         * 1:  No internet connection from start.
         * 2:  Intrenet connection lost during sending.
         * 3:  Got no response from ip adress 'address'.
         * 4:  Got incorrect responce from ip adress 'address'.
         * 5+: Internal error. See senderMiddleEnd for details.
        */
        internal int SendText(string text)
        {
            // Send text
            int code = senderMiddleEnd.SendText(text);
            // Return error code
            return code;
        }

        /// <summary>
        /// Send file with path 'path' to ip address "iPAddress" to port number "portNumber".
        /// </summary>
        /// <param name="path">The path to the file to send.</param>
        /// <returns>Return 0 if successful, otherwise return error code.</returns>
        internal int SendFile(string path)
        {
            // Read file
            byte[] bytesToSend = File.ReadAllBytes(path);
            // Send file
            int code = senderMiddleEnd.SendFile(bytesToSend);
            // Return error code
            return code;
        }

        /* For future use, currently no need to foucs on/// <summary>
        /// Send some text 'text' to ip address "iPAddress" to port number "portNumber" asynchronously
        /// </summary>
        /// <param name="text">The text to send</param>
        /// <returns>Return 0 if successful, otherwise return error code</returns>
        // Error codes, see SendText
        public int SendTextAsync(string text)
        {
            int code = 0;
            // Create a Task to send text
            Task task = Task.Factory.StartNew(() =>
            {
                // Send text
                code = SendText(text);
            });
            // Create empty Task to wait for end of Task 'task' 
            Task continuationTask = task.ContinueWith((c) => { });
            // Wait for 'continuationTask'
            continuationTask.Wait();
            // Return code
            return code;

        }*/
    }
}
