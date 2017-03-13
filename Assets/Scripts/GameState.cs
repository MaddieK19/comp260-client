using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * GameState class that manages the other classes and updates them depending on the server state
 */
public class GameState : MonoBehaviour {
    public GameObject player;
    public GameObject[] towers;

    Vector3 playerPreviousLocation;

	// Use this for initialization
	void Start () {
        playerPreviousLocation = new Vector3(0,0,0);

    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if (player.transform.position != playerPreviousLocation)
        {

        }
	}

    void checkCellState(Cell cell)
    {

    }
}
