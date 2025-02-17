using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawn : MonoBehaviour
{
    public GameObject pipe;
    public float spawnRate;
    public float timer = 0;
    public float heightOffset = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnPipe();
        spawnRate = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < spawnRate) // If current timer is smaller than spawn rate
        {
            timer += Time.deltaTime; // Count the timer up
        }
        else
        {
            spawnPipe(); //Spawn the new pipe
            timer = 0; // Reset the timer
        }
    }

    void spawnPipe()
    {
        float lowestPoint = transform.position.y - heightOffset; // Calculate the lowest point for y
        float highestPoint = transform.position.y + heightOffset; // Calculate the highest point for y
        Instantiate(pipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation); //Spawn the new pipe
    }
}
