using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Script : MonoBehaviour
{
    // Reference 
    public Rigidbody2D myRigidBody;
    public float flapS; // Flap strength
    public LogicScript logic;
    public bool alive = true;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) == true || Input.GetMouseButtonDown(0) && alive == true) // Check if spacebar/left mouse button has been pressed on this frame
        {
            myRigidBody.velocity = Vector2.up * flapS; // Move body up
        }  
    }

    // Trigger game over screen
    private void OnCollisionEnter2D(Collision2D collision)
    {
        logic.gameOver();
        alive = false;
    }
}
