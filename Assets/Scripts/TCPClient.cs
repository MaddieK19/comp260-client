using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.IO;
using UnityEngine;
using System.Text;
using System;

/*
 *  TCPClient class that connects to the specified server
 */
public class TCPClient : MonoBehaviour {
    // int for the portnumber the client will connect to
    private int portNumber = 2222;
    private string host = "localhost";
    private int serverTimeout = 2500;
    
    
    // TcpClient that connects to server
    private TcpClient client;


    private StreamWriter stream;
    private NetworkStream netStream;

    // Use this for initialization
    void Start () {
        connectToServer();
        netStream = client.GetStream();
        stream = new StreamWriter(netStream);
        netStream.ReadTimeout = serverTimeout;
    }

    // Attempts to connect to the server and get the network stream
    void connectToServer()
    {
        try
        {
            client = new TcpClient(host, portNumber);
            client.Connect(host, portNumber);
            Debug.Log("Connected to server");
        }
        catch (SocketException)
        {
            Debug.Log("Unable to connect to server");
            return;
        }
     }

    // Update is called once per frame
    void Update()
    {


    }


    string readFromStream()
    {
        string returnData = null;
        if (netStream.CanRead)
        {

            // Reads NetworkStream into a byte buffer.
            byte[] bytes = new byte[client.ReceiveBufferSize];

            // Read can return anything from 0 to numBytesToRead. 
            // This method blocks until at least one byte is read.
            netStream.Read(bytes, 0, (int)client.ReceiveBufferSize);

            // Returns the data received from the host to the console.
            if (bytes != null)
            {
                returnData = Encoding.UTF8.GetString(bytes);
                Debug.Log("This is what the host returned to you:" + returnData);
            }
        }
            return returnData;

    }

    void writeToStream(string dataToWrite)
    {

        if (netStream.CanWrite)
        {
            Byte[] sendBytes = Encoding.UTF8.GetBytes(dataToWrite);
            netStream.Write(sendBytes, 0, sendBytes.Length);
        }
    }

    /// Disconnects from server
    void disconnectFromServer()
    {
        netStream.Close();
        client.GetStream().Close();
        client.Close();
    }
    /// Disconnects from server when applicationis closed
    private void OnApplicationQuit()
    {
        disconnectFromServer();
    }
}
