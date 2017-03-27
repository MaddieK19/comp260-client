using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // float for players movement speed
    public float speed = 0.5f;
    
    // Use this for initialization
    void Start()
    {
    }
    
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        Vector2 pos2D = new Vector2(transform.position.x, transform.position.y);

        transform.position = Vector2.MoveTowards(transform.position, pos2D + movement, speed);
    }

}

