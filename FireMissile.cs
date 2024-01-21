using UnityEngine;

public class MissileController : MonoBehaviour
{
   
    public float missileSpeed = 200; // Speed of the missile

    void Update()
    {
        transform.Translate(Vector3.forward * missileSpeed * Time.deltaTime);
       
    }

}
