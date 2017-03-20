using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.IO;
using UnityEngine;

/*
 *  TCPClient class that connects to the specified server
 */
public class TCPClient : MonoBehaviour {
    // int for the portnumber the client will connect to
    private int portNumber = 2222;
    private string host = "localhost";
    
    
    // TcpClient that connects to server
    private TcpClient client;


    private StreamWriter stream;
    private NetworkStream netStream;

    // Use this for initialization
    void Start () {
        connectToServer();
        netStream = client.GetStream();
        stream = new StreamWriter(netStream);

    }

    // Attempts to connect to the server and get the network stream
    void connectToServer()
    {
        try
        {
            client = new TcpClient(host, portNumber);
            client.Connect(host, portNumber);
        }
        catch (SocketException)
        {
            Debug.Log("Unable to connect to server");
            return;
        }
       
    }
	
	// Update is called once per frame
	void Update () {
        stream.Write("move north"); //writeToStream("move north");
    }

    void readFromStream()
    {
        //netStream.Read();
    }

    void writeToStream(string dataToWrite)
    {
        stream.Flush();
        stream.Write(dataToWrite, 0, dataToWrite.Length);
    }
}
