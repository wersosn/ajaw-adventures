using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    public float movement = 5; // Movement speed
    public float deadZone = -34; // Where the pipes should be destroyed
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * movement) * Time.deltaTime; // Moving the pipes
        if(transform.position.x < deadZone)
        {
            Debug.Log("Pipe deleted");
            Destroy(gameObject); // Destroy the game object that holds this script
        }
    }
}
