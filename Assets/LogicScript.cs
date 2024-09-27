using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // To access more functionality (text/UI stuff)

public class LogicScript : MonoBehaviour
{
    public int score = 0; // Current score
    public Text textS; // Shown score

    [ContextMenu("Increase score")] // To access the functions in unity (3 dots next to the Logic Script (script))
    public void addScore()
    {
        score += 1;
        textS.text = score.ToString(); 
    }
}
