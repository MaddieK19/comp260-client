using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour {
    public GameObject cell;

    public GameObject[,] map;
    public int mapSize = 10;
    

	// Use this for initialization
	void Start () {
        map = new GameObject[mapSize, mapSize];

        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                map[x, y] = Instantiate(cell, new Vector2(x * cell.transform.localScale.x, y * cell.transform.localScale.y), Quaternion.identity) as GameObject;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
