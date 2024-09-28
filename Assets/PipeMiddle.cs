using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMiddle : MonoBehaviour
{
    public LogicScript logic;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Function OnTrigger will run whenever an object first hits the trigger
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        logic.addScore();
    }
}
