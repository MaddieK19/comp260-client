using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * TowerManagement classes that updates and controls the active towers
 * in the game
 */


public class TowerManager : MonoBehaviour {
    // List of all the active towers
    public List<GameObject> towers;
    // Tower prefab used to make new towers
    public GameObject towerPrefab;
    // Player to get their position when building towers
    public GameObject player;
    // int for the maximum number of towers a single player can build
    int maxTowerNumber = 10;
    // string for the key the player needs to press to build a tower
    public string key = "q";

    // Use this for initialization
    void Start () {
        towers = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
        checkForInput();
    }

    // Instantiates a new tower and adds it to towers list
    void addTower(Vector3 towerPosition)
    {
        GameObject newTower = Instantiate(towerPrefab, towerPosition, Quaternion.identity) as GameObject;
        towers.Add(newTower);
    }

    // Checks to see if the player has pressed a key
    void checkForInput()
    {
        if (Input.GetKeyDown(key) && towers.Count < maxTowerNumber)
        {
            addTower(player.transform.position);            
        }
    }

    // Check for inactive towers and removes them
    void checkTowerState()
    {
        for (int i = 0; i < towers.Count; i++)
        {
            if (towers[i].GetComponent<Tower>().getHealth() < 1)
                towers.RemoveAt(i);
        }
    }
}
