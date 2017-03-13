using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour {
    public List<GameObject> towers;
    public GameObject towerPrefab;
    int maxTowerNumber = 10;
    public string key = "a";

    private PlayerController player;

    // Use this for initialization
    void Start () {
        player = GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
        checkForInput();
    }

    void addTower(Vector3 towerPosition)
    {
        GameObject newTower = Instantiate(towerPrefab, towerPosition, Quaternion.identity) as GameObject;
        towers.Add(newTower);
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
