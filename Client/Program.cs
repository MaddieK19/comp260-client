using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

/*
 *  TCPClient class that connects to the specified server
 *  Used https://github.com/Fulviuus/unity-network-client/blob/master/Assets/networkSocket.cs for writeToStream
 */

namespace Client
{
    class Program
    {
        private static TCPClient client;


        static void Main(string[] args)
        {
            client = new TCPClient();
            client.initServerConnection();
        }
       
        /// Disconnects from server when application is closed
        void OnProcessExit(object sender, EventArgs e)
        {
            client.disconnectFromServer();
        }
    }

   
}
