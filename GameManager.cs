using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance

    public int score = 0; // Example score variable

    void Awake()
    {
        // Implementing a simple Singleton pattern to ensure only one GameManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SceneManager.LoadScene(0); 
        MissionManager.instance.AssignMission(new SampleMission());
    }

    void Update()
    {
        // Check for game over conditions or other continuous checks
    }

    public void IncreaseScore(int amount)
    {
        score += amount;

    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene(1); 

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

}
