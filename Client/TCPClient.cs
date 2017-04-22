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
            if(client != null)
                initStreams();
            else
            {
                Console.WriteLine("Unable to connect to server.");
                Console.ReadLine();
                Environment.Exit(0);
            }
                
        }

        private void initStreams()
        {
            netStream = client.GetStream();
            netStream.ReadTimeout = serverReadTimeout;
            writer = new StreamWriter(netStream);
            reader = new StreamReader(netStream);
        }

        //! Attempts to connect to the server
        void connectToServer()
        {
            try
             {
                client = new TcpClient(host, portNumber);
                client.Connect(host, portNumber);
                 //Console.WriteLine("Connected to server");
             }
             catch (SocketException)
             {
                 Console.WriteLine("Unable to connect to server");
                 return;
             }
            catch (NullReferenceException)
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
                returnData = reader.ReadLine();
                Console.WriteLine(returnData);
            }
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
                client.Client.Shutdown(SocketShutdown.Both);
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("No connection");
                return;
            }
           
        }

        //! Disconnects from server when application is closed
        void OnProcessExit(object sender, EventArgs e)
        {
            disconnectFromServer();
        }
    }
}

