using UnityEngine;
using UnityEngine.Pool;
using System.Collections;

public class PlaneController : MonoBehaviour
{
    public float maxSpeed = 100.0f; // Forward speed of the plane
    public float flapsRotationSpeed = 20f;
    public float rudderRotationSpeed = 10f;
    public float pitchSpeed = 2.0f; // Pitch speed of the plane

    private Rigidbody rb;
    public GameObject missilePrefab;
    public GameObject bulletPrefab;
    private ObjectPool<GameObject> bulletPool;
    public GameObject speedEffect;
    [SerializeField] private Pilot pilot;

    public float reloadTime = 0.3f;
    private bool reloaded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bulletPool = new ObjectPool<GameObject>(() => Instantiate(bulletPrefab));
        Physics.IgnoreCollision(bulletPrefab.GetComponent<Collider>(), GetComponent<Collider>());
        PopulatePool(1000);

    }

    void PopulatePool(int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Release(bullet);
        }
    }

    void Update()
    {

        (float horizontalInput, float verticalInput, float forwardSpeed, float rudderRotation) = pilot.ControlPlane(maxSpeed);

        // Plane rotation
        transform.Rotate(new Vector3(0, 0, -1), horizontalInput * flapsRotationSpeed * Time.deltaTime);

        // Plane pitch
        transform.Rotate(Vector3.right, verticalInput * pitchSpeed * Time.deltaTime);

        // Plane rudder
        transform.Rotate(new Vector3(0, rudderRotation, 0), rudderRotationSpeed * Time.deltaTime);

        // Basic physics
        if (horizontalInput == 0 && verticalInput == 0)
            RotateToFoward();

        // Move the plane forward at a fixed speed
        rb.velocity = transform.forward * forwardSpeed;
        // Wind speed visual efect
        speedEffect.SetActive(forwardSpeed > 50f);

        if (pilot.IsFiring() && reloaded == true)
        {
            
            FireGun();
            reloaded = false;
            Invoke("reload", reloadTime);
        }
    }
    void reload() { reloaded = true; }

    private void FireMissle()
    {
        GameObject missile = Instantiate(missilePrefab, transform.position, transform.rotation);
        MissileController missileController = missile.GetComponent<MissileController>();
        missileController.missileSpeed = 500f;

    }

    private void FireGun()
    {
        
        GameObject bullet = bulletPool.Get();
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.SetActive(true);
        BulletController bulletController = bullet.GetComponent<BulletController>();
        bulletController.bulletSpeed = 500f;

        StartCoroutine(ReleaseBulletAfterDelay(bullet));

    }

    IEnumerator ReleaseBulletAfterDelay(GameObject bullet)
    {
        yield return new WaitForSeconds(5f); // Wait for 5 seconds
        bullet.SetActive(false);
        bulletPool.Release(bullet);
    }

    private void RotateToFoward()
    {
        // Given that the plane has speed, it should assume an aerodynimic position naturally if no input is given
        Quaternion targetRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0); 
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, (flapsRotationSpeed / 50) * Time.deltaTime);
    }
}
