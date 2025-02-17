using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // To access more functionality (text/UI stuff)
using UnityEngine.SceneManagement;
using System; // To access more functionality (scene management)

public class LogicScript : MonoBehaviour
{
    public int score = 0; // Current score
    public int highScore = 0; // High score
    public Text textS; // Shown score
    public Text textHighScore;
    public GameObject GameOver;
    public GameObject Ajaw;
    public GameObject Score;
    public GameObject GameStart;
    public GameObject Pipes;
    public GameObject NewHighScore;
    private string userId = "player1";

    public float minYdeath = -30;
    public float maxYdeath = 30;

    void Start()
    {
        if (FirebaseManager.Instance == null)
        {
            Debug.LogError("FirebaseManager.Instance is null!");
            return;
        }

        if (textHighScore == null)
        {
            Debug.LogError("highScoreText is null!");
            return;
        }
        Debug.Log(minYdeath);
        Debug.Log(maxYdeath);
        Debug.Log($"Ajaw position.y: {Ajaw.transform.position.y}");
        FirebaseManager.Instance.InitializeFirebase(() =>
        {
            FirebaseManager.Instance.GetHighScore(userId, (retrievedHighScore) =>
            {
                highScore = retrievedHighScore;
                textHighScore.text = highScore.ToString();
            });
        });
    }

    void Update()
    {
        if (Ajaw != null && Ajaw.transform.position.y < minYdeath || Ajaw.transform.position.y > maxYdeath)
        {
            gameOver();
        }
    }

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //When the Ajaw crashes into a pipe - game over
    public void gameOver()
    {
        GameOver.SetActive(true);
        if(score > highScore)
        {
            NewHighScore.SetActive(true);
            highScore = score;
            textHighScore.text = highScore.ToString();
            FirebaseManager.Instance.SaveHighScore(userId, highScore);
        }
    }
}
