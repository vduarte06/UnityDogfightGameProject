using UnityEngine;

public class BulletController : MonoBehaviour
{
   
    public float bulletSpeed = 0; // Speed of the missile

    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
       
    }

}
