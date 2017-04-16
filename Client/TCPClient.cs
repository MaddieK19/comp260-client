﻿using System;
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
    class TCPClient
    {
        /// int for the portnumber the client will connect to
        private int portNumber = 2222;
        /// 
        private string host = "localhost"; //"178.62.110.12";
        /// int for how long a read from the network  stream can take before timing out
        private int serverReadTimeout = 25000;

        /// TcpClient that connects to server
        public TcpClient client;
        /// Network stream used to read and write from the server
        private NetworkStream netStream;

        StreamWriter writer;
        StreamReader reader;
        // Use this for initialization
        public void initServerConnection()
        {
            connectToServer();
            netStream = client.GetStream();
            netStream.ReadTimeout = serverReadTimeout;
            writer = new StreamWriter(netStream);
            reader = new StreamReader(netStream);
        }

        /// Attempts to connect to the server and get the network stream
        void connectToServer()
        {
            client = new TcpClient(host, portNumber);
            try
            {
                client.Connect(host, portNumber);
                Console.WriteLine("Connected to server");
            }
            catch (SocketException)
            {
                Console.WriteLine("Unable to connect to server");
                return;
            }
        }

        /// Reads data from the network stream
        public string readFromStream()
        {
            string returnData = null;

            if (netStream.CanRead && client.Connected)
            {
                // Reads NetworkStream into a byte buffer.
                byte[] bytes = new byte[client.ReceiveBufferSize];

                // Read can return anything from 0 to numBytesToRead
                try
                {
                    netStream.Read(bytes, 0, client.ReceiveBufferSize);
                }
                catch (SocketException)
                {
                    Console.WriteLine("Server disconnected");
                    connectToServer();
                }
                // Returns the data received from the host to the console.
                if (bytes != null)
                {
                    returnData = Encoding.UTF8.GetString(bytes);
                    Console.WriteLine("Server says: " + returnData);
                }
            }
            return returnData;
        }


        /// Takes a String and encodes the string and write it to the network stream
        public void writeToStream(string dataToWrite)
        {
            dataToWrite = dataToWrite + "\r\n";
            try
            {
                writer.Write(dataToWrite);
                writer.Flush();
            }
            catch (SocketException)
            {
                Console.WriteLine("Server took too long to respond");
            }
        }

        /// Disconnects from server
        public void disconnectFromServer()
        {
            try
            {
                netStream.Close();
                client.GetStream().Close();
                client.Close();
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("No connection");
                return;
            }
        }

        /// Disconnects from server when application is closed
        void OnProcessExit(object sender, EventArgs e)
        {
            disconnectFromServer();
        }
    }
}
