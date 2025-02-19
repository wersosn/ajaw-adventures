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
    public AudioSource newHighScoreSound, gameOverSound;
    public bool alive = true;
    private string userId;

    void Start()
    {
        userId = GetUserId();
        highScore = 0;
        //highScore = PlayerPrefs.GetInt("HighScore", 0);
        textHighScore.text = highScore.ToString();
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

        FirebaseManager.Instance.InitializeFirebase(() =>
        {
            FirebaseManager.Instance.GetHighScore(userId, (retrievedHighScore) =>
            {
                if(retrievedHighScore > highScore)
                {
                    highScore = retrievedHighScore;
                    textHighScore.text = highScore.ToString();
                    PlayerPrefs.SetInt("HighScore", highScore);
                    PlayerPrefs.Save();
                    FirebaseManager.Instance.SaveHighScore(userId, highScore);
                }
            });
        });
    }

    public static string GetUserId()
    {
        string userIdKey = "UserID";
        if (PlayerPrefs.HasKey(userIdKey))
        {
            return PlayerPrefs.GetString(userIdKey);
        }
        else
        {
            string newUserId = SystemInfo.deviceUniqueIdentifier;
            PlayerPrefs.SetString(userIdKey, newUserId);
            PlayerPrefs.Save();
            return newUserId;
        }
    }

    [ContextMenu("Increase score")] // To access the functions in unity (3 dots next to the Logic Script (script))
    public void addScore(int scoreToAdd)
    {
        if (alive)
        {
            score += scoreToAdd;
            textS.text = score.ToString();
        }
    }

    public void startGame()
    {
        alive = true;
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
        alive = false;
        GameOver.SetActive(true);
        if(score > highScore)
        {
            NewHighScore.SetActive(true);
            if(newHighScoreSound != null)
            {
                newHighScoreSound.Stop();
                newHighScoreSound.Play();
            }
            highScore = score;
            textHighScore.text = highScore.ToString();
            FirebaseManager.Instance.SaveHighScore(userId, highScore);
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
        else
        {
            if(gameOverSound != null)
            {
                gameOverSound.Stop();
                gameOverSound.Play();
            }
        }
    }
}
