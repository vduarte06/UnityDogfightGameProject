using UnityEngine;
using UnityEngine.SceneManagement;
using hadrack.gpst.core.events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public SOEvent<string> loadMissionEvent;
    public SOEvent<int> updatePlayerEnergyEvent;

    void Awake()
    {
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
        MissionManager.instance.AssignMission(new Mission1());
        loadMissionEvent.subscribe(OnLoadMission);
        updatePlayerEnergyEvent.subscribe(OnUpdatePlayerEnergyEvent);
    }

    void Update()
    {
        
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

    void OnLoadMission(string missionKey) { 
        Mission m = MissionManager.instance.GetMissionByKey(missionKey);
        SceneManager.LoadScene(m.GetMissionKey());
    }

    void OnUpdatePlayerEnergyEvent(int energy) { 
    }

}
