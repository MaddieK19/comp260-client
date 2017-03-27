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
public class Client : MonoBehaviour
{
    /// int for the portnumber the client will connect to
    private int portNumber = 2222;
    ///
    private string host = "localhost";
    /// int for how long a read from the network  stream can take before timing out
    private int serverReadTimeout = 2500;
    /// float to ensure the server is update at a fixed rate
    float ticks = 0;

    /// TcpClient that connects to server
    private TcpClient client;
    /// Network stream used to read and write from the server
    private NetworkStream netStream;

    // Use this for initialization
    void Start()
    {
        connectToServer();
        netStream = client.GetStream();
        netStream.ReadTimeout = serverReadTimeout;
        ticks = Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (ticks < 1)
        {
            readFromStream();
            writeToStream("Maddie");
        }

        else if (Time.deltaTime > ticks + 1 / 5)
        {
            ticks = Time.deltaTime;
            readFromStream();
            //writeToStream("Maddie");
        }

    }

    /// Attempts to connect to the server and get the network stream
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

    /// Reads data from the network stream
    string readFromStream()
    {
        string returnData = null;
        if (netStream.CanRead)
        {
            // Reads NetworkStream into a byte buffer.
            byte[] bytes = new byte[client.ReceiveBufferSize];

            // Read can return anything from 0 to numBytesToRead
            netStream.Read(bytes, 0, client.ReceiveBufferSize);

            // Returns the data received from the host to the console.
            if (bytes != null)
            {
                returnData = Encoding.UTF8.GetString(bytes);
                Debug.Log("Server says: " + returnData);
            }
        }
        return returnData;
    }


    /// Takes a String and encodes the string and write it to the network stream
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
