using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

/*
 *  TCPClient class that connects to the specified server
 */
public class TCPClient : MonoBehaviour {
    private int portNumber = 2222;
    private string host = "localhost";
    TcpClient client;
    public bool connected = false;

    // Use this for initialization
    void Start () {
        try
        {
            client = new TcpClient(host, portNumber);
            connected = true;
        }
        catch (SocketException)
        {
            Debug.Log("Unable to connect to server");
            return;
        }
    }
	
	// Update is called once per frame
	void Update () {
        

    }
}
