using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Script : MonoBehaviour
{
    // Reference 
    public Rigidbody2D myRigidBody;
    public AudioSource jumpSound;
    public float flapS; // Flap strength
    public LogicScript logic;
    public bool alive = true;
    public float minYdeath = -30;
    public float maxYdeath = 30;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKeyDown(KeyCode.Space) == true || Input.GetMouseButtonDown(0)) && alive == true) // Check if spacebar/left mouse button has been pressed on this frame
        {
            myRigidBody.velocity = Vector2.up * flapS; // Move body up
            if (jumpSound != null)
            {
                jumpSound.Play(); // Play jump sound
            }
        }
        if((myRigidBody.transform.position.y < minYdeath || myRigidBody.transform.position.y > maxYdeath) && alive == true)
        {
            Debug.Log($"alive = {alive}");
            logic.gameOver();
            alive = false;
        }
    }

    // Trigger game over screen
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(alive)
        {
            logic.gameOver();
            alive = false;
        }
    }
}
