using UnityEngine;
using UnityEngine.UI;
using TMPro;
using hadrack.gpst.core.events;
using System.Collections;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI  playerName;
    public TextMeshProUGUI  playerEnergy;
    public SOEvent<int> updatePlayerEnergyEvent;
    public TextMeshProUGUI  collisionMessage;
    public SOEvent<string> collisionMessageEvent;
    public TextMeshProUGUI  enemyEnergyFeedback;
    public SOEvent<int> updateEnemyEnergyEvent;


    void Start()
    {
        updatePlayerEnergyEvent.subscribe(OnUpdatePlayerEnergy);
        collisionMessageEvent.subscribe(OncollisionMessage);
        updateEnemyEnergyEvent.subscribe(OnEnemyEnergyUpdate);
    }

    public void OnUpdatePlayerEnergy(int energy){
        playerEnergy.text = "Health: " + energy;
        OncollisionMessage(playerEnergy.text);
        
    }

    public void OncollisionMessage(string message){
        collisionMessage.gameObject.SetActive(true);
        collisionMessage.text = message;
        StartCoroutine(HideElementAfterDelay(collisionMessage.gameObject, 1));
    }

    public void OnEnemyEnergyUpdate(int energy){
        enemyEnergyFeedback.gameObject.SetActive(true);
        enemyEnergyFeedback.text = "Enemy: " + energy;
        StartCoroutine(HideElementAfterDelay(enemyEnergyFeedback.gameObject, 1));
    }

    IEnumerator HideElementAfterDelay(GameObject obj, float seconds)
    {
        yield return new WaitForSeconds(seconds); 
        obj.SetActive(false);
    }
   

}
