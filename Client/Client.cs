using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Client
{
    class TCPClient
    {
        /// int for the portnumber the client will connect to
        private int portNumber = 2222;
        /// 
        private string host = "178.62.110.12";//"localhost";
        /// int for how long a read from the network  stream can take before timing out
        private int serverReadTimeout = 25000;
        /// float to ensure the server is update at a fixed rate
        float ticks = 0;
        float updateTime = 5;

        /// TcpClient that connects to server
        private TcpClient client;
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
            readFromStream();
        }
        // Update is called once per frame
        void Update()
        {
            if (client.Connected)
            {
                string localDate = DateTime.Now.ToShortTimeString();

                readFromStream();
                writeToStream("move south"); // send up game state
            }

        }

        /// Attempts to connect to the server and get the network stream
        void connectToServer()
        {
            try
            {
                client = new TcpClient(host, portNumber);
                client.Connect(host, portNumber);
                Console.Write("Connected to server");
            }
            catch (SocketException)
            {
                Console.Write("Unable to connect to server");
                return;
            }
        }

        /// Reads data from the network stream
        string readFromStream()
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
                    Console.Write("Server disconnected");
                    connectToServer();
                }

                // Returns the data received from the host to the console.
                if (bytes != null)
                {
                    returnData = Encoding.UTF8.GetString(bytes);
                    Console.Write("Server says: " + returnData);
                }
            }
            //netStream.Close();
            return returnData;
        }


        /// Takes a String and encodes the string and write it to the network stream
        void writeToStream(string dataToWrite)
        {
            dataToWrite = dataToWrite + "\r\n";
            try
            {
                writer.Write(dataToWrite);
                writer.Flush();
            }
            catch (SocketException)
            {
                Console.Write("Server took too long to respond");
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
                Console.Write("No connection");
                return;
            }
        }
    }
}
