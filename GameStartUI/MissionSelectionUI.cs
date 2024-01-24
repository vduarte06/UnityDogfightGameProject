using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionSelectionUI : MonoBehaviour
{
    public Button[] missionButtons; 
    public string[] sceneNames;     

    void Start()
    {
        
        if (missionButtons.Length != sceneNames.Length)
        {
            Debug.LogError("Number of buttons does not match the number of scene names.");
            return;
        }

        
        for (int i = 0; i < missionButtons.Length; i++)
        {
            int index = i; 
            missionButtons[i].onClick.AddListener(() => SelectMission(index));
        }
    }

    void SelectMission(int missionIndex)
    {
        
        if (missionIndex >= 0 && missionIndex < sceneNames.Length)
        {
            SceneManager.LoadScene(sceneNames[missionIndex]);
        }
        else
        {
            Debug.LogError($"Invalid mission index: {missionIndex}");
        }
    }
}
