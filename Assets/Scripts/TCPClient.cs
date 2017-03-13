using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TCPClient : MonoBehaviour {
    private int socketNumber;
    private string host = "localhost";
    TCPClient client;

    // Use this for initialization
    void Start () {
        client = new TCPClient();
        client.socketNumber = socketNumber;
        client.host = host;
    }
	
	// Update is called once per frame
	void Update () {
        

    }
}
