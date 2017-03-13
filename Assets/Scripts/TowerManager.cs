using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour {
    public List<GameObject> towers;
    public GameObject towerPrefab;
    public GameObject player;
    int maxTowerNumber = 10;
    public string key = "q";

    //private PlayerController player;

    // Use this for initialization
    void Start () {
        //player = GetComponent<PlayerController>();
        towers = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
        checkForInput();
    }

    void addTower(Vector3 towerPosition)
    {
        Debug.Log("Adding tower");
        GameObject newTower = (GameObject)Instantiate(towerPrefab, towerPosition, Quaternion.identity);
        towers.Add(towerPrefab);
    }

    void destroyTower()
    {
        // TODO
    }

    void checkForInput()
    {
        if (Input.GetKeyDown(key))
        {
            addTower(player.transform.position);            
        }
    }
}
