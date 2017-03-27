using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour {
    private int health;
    int speed = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void moveToGoal(Vector2 goal)
    {
        if (health > 0)
            transform.position = Vector2.MoveTowards(transform.position, goal, Time.deltaTime * speed);
    }
}
