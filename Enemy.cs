using UnityEngine;
using UnityEngine.Pool;
using System.Collections;
using hadrack.gpst.core.events;

public class Enemy: MonoBehaviour 
{
    public SOEvent<int> updateEnemyEnergyEvent;
    PlaneController planeController;

    void Start(){
        planeController = GetComponent<PlaneController>();
    }

     //Handle collisions between the GameObjects 
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Terrain")){
            planeController.health = 0;
        }
        else if(!collision.gameObject.tag.Contains("Bullet") && !collision.gameObject.name.Contains("Aircraft")){
            planeController.health = 0;
        }
        else if(!collision.gameObject.tag.Contains(gameObject.tag)){
            planeController.health --;
            int healthPercent = Mathf.RoundToInt((planeController.health/planeController.maxHealth)*100);

            updateEnemyEnergyEvent?.invoke(planeController.health);

        }
        
        
    }
   
}
