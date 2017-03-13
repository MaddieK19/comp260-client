using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
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

    // Use this for initialization
    void Start () {
        
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
        //client.GetStream();
    }
	
	// Update is called once per frame
	void Update () {

    }
}
