using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * GameState class that manages the other classes and updates them depending on the server state
 */
public class GameState : MonoBehaviour {
    // Player to update player state
    public GameObject player;
    // 
    public TowerManager towerManager;
    // TCPClient to send and recieve data through
    public Client client;

    public List<Command> commandQueue;

    int maxNumberOfCommands = 5;

    Vector3 playerPreviousLocation;

	// Use this for initialization
	void Start () {
        playerPreviousLocation = player.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		if (player.transform.position != playerPreviousLocation)
        {
            checkPlayerPosition();
        }
	}

    void checkPlayerPosition()
    {
        Command newCommand = new Command("move");
        if (player.transform.position.x < playerPreviousLocation.x)
            newCommand.addWord("south");
        else if (player.transform.position.x > playerPreviousLocation.x)
            newCommand.addWord("north");
        if (player.transform.position.y < playerPreviousLocation.y)
            newCommand.addWord("west");
        else if (player.transform.position.y > playerPreviousLocation.y)
            newCommand.addWord("east");

        commandQueue.Add(newCommand);
    }
}
