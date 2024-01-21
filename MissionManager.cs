using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionManager : MonoBehaviour
{
    public static MissionManager instance; 

    public List<Mission> missions = new List<Mission>();

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

    void Update()
    {
        
        for (int i = missions.Count - 1; i >= 0; i--)
        {
            Mission mission = missions[i];
            mission.UpdateMission();

            if (mission.IsMissionComplete())
            {
                missions.RemoveAt(i);
                Debug.Log("Mission Completed: " + mission.GetMissionName());
            }
        }
    }

    public void AssignMission(Mission mission)
    {
        
        missions.Add(mission);
        Debug.Log("Mission Assigned: " + mission.GetMissionName());
    }

    
}



