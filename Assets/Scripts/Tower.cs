using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Tower class 
 */


public class Tower : MonoBehaviour
{
    private int health = 100;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int getHealth()
    {
        return health;
    }

    public void setHealth(int newHealth)
    {
        health = newHealth;
    }
}
