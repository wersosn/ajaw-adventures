using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // To access more functionality (text/UI stuff)
using UnityEngine.SceneManagement; // To access more functionality (scene management)

public class LogicScript : MonoBehaviour
{
    public int score = 0; // Current score
    public bool isRestarted = false; // Flag that checks if the game was restarted or not
    public Text textS; // Shown score
    public GameObject GameOver;
    public GameObject Ajaw;
    public GameObject Score;
    public GameObject GameStart;
    public GameObject Pipes;

    [ContextMenu("Increase score")] // To access the functions in unity (3 dots next to the Logic Script (script))
    public void addScore(int scoreToAdd)
    {
        score += scoreToAdd;
        textS.text = score.ToString(); 
    }

    public void startGame()
    {
         GameStart.SetActive(false);
         Ajaw.SetActive(true);
         Score.SetActive(true);
         Pipes.SetActive(true);
    }

    //Game over:
    public void restartGame()
    {
        isRestarted = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //When the Ajaw crashes into a pipe - game over
    public void gameOver()
    {
        GameOver.SetActive(true);
    }
}
