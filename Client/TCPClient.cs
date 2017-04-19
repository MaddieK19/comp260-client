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
        //! int for the portnumber the client will connect to
        private int portNumber = 25565;
        //! String for the host
        private string host = "localhost"; //"178.62.110.12";
        //! int for how long a read from the network  stream can take before timing out
        private int serverReadTimeout = 25000;

        //! TcpClient that connects to server
        public TcpClient client;
        //! Network stream used to read and write from the server
        private NetworkStream netStream;
        //! StreamWriter for writing data to the network stream
        StreamWriter writer;
        //! StreamWriter for reading data from the network stream
        StreamReader reader;

        //! Connects to the server and initialises the streams
        public void initServerConnection()
        {
            connectToServer();
            netStream = client.GetStream();
            netStream.ReadTimeout = serverReadTimeout;
            writer = new StreamWriter(netStream);
            reader = new StreamReader(netStream);
        }

        //! Attempts to connect to the server
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

        //! Reads data from the network stream
        public string readFromStream()
        {
            string returnData = null;

            if (netStream.CanRead && netStream.DataAvailable)
            {
                /*// Reads netStream into a byte buffer
                byte[] bytes = new byte[client.ReceiveBufferSize];

                try
                {
                    netStream.Read(bytes, 0, client.ReceiveBufferSize);
                }
                catch (SocketException)
                {
                    Console.WriteLine("Server disconnected");
                }
                // Returns the data received from the host to the console.
                if (bytes != null)
                {
                    returnData = Encoding.UTF8.GetString(bytes);
                   
                }*/
                //while (netStream.DataAvailable)
               // {
                    Console.WriteLine("Server says: " + reader.ReadLine());
               // }
               // returnData = reader.ReadLine();
            }
            //Console.WriteLine("Server says: " + returnData);
            return returnData;
        }


        //! Takes a String and encodes the string and write it to the network stream
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

        //! Closes network streams and disconnects from server
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
            catch (InvalidOperationException)
            {

            }
        }

        //! Disconnects from server when application is closed
        void OnProcessExit(object sender, EventArgs e)
        {
            disconnectFromServer();
        }
    }
}

