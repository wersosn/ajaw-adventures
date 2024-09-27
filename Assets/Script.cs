using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour
{
    // Reference 
    public Rigidbody2D myRigidBody;
    public float flapS; // Flap strength

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) == true || Input.GetMouseButtonDown(0)) // Check if spacebar/left mouse button has been pressed on this frame
        {
            myRigidBody.velocity = Vector2.up * flapS; // Move body up
        }  
    }
}
