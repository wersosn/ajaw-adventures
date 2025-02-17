using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class FirebaseManager : MonoBehaviour
{
    private DatabaseReference dbReference;
    public static FirebaseManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                FirebaseApp app = FirebaseApp.DefaultInstance;
                dbReference = FirebaseDatabase.DefaultInstance.RootReference;
            }
            else
            {
                Debug.LogError("Firebase error: " + task.Result);
            }
        });
    }

    public void SaveHighScore(string userId, int score)
    {
        dbReference.Child("highscores").Child(userId).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                int currentHighScore = snapshot.Exists ? int.Parse(snapshot.Value.ToString()) : 0;

                if (score > currentHighScore)
                {
                    dbReference.Child("highscores").Child(userId).SetValueAsync(score);
                }
            }
        });
    }

    public void GetHighScore(string userId, System.Action<int> onResult)
    {
        dbReference.Child("highscores").Child(userId).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && task.Result.Exists)
            {
                int highScore = int.Parse(task.Result.Value.ToString());
                onResult?.Invoke(highScore);
            }
            else
            {
                onResult?.Invoke(0);
            }
        });
    }

    public void InitializeFirebase(System.Action onInitialized)
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                FirebaseApp app = FirebaseApp.DefaultInstance;
                dbReference = FirebaseDatabase.DefaultInstance.RootReference;
                onInitialized?.Invoke();
            }
            else
            {
                Debug.LogError("Firebase error: " + task.Result);
            }
        });
    }

}
