using UnityEngine;
using UnityEngine.Pool;
using System.Collections;
using hadrack.gpst.core.events;

public class Player: MonoBehaviour 
{
    public SOEvent<int> updatePlayerEnergyEvent;
    public SOEvent<string> collisionMessage;
    PlaneController planeController;

    void Start(){
        planeController = GetComponent<PlaneController>();
    }

     //Handle collisions between the GameObjects 
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Terrain")){
            planeController.health = 0;
            collisionMessage?.invoke("Fatal Collision With Terrain");
        }
        else if(!collision.gameObject.tag.Contains("Bullet") && !collision.gameObject.name.Contains("Aircraft")){
            planeController.health = 0;
            collisionMessage?.invoke("Fatal Collision With Another Aircraft/Terrain");
        }
        else if(!collision.gameObject.tag.Contains(gameObject.tag)){
            planeController.health --;
            updatePlayerEnergyEvent?.invoke(planeController.health);
        }
    }
   
}
