using UnityEngine;
using UnityEngine.Pool;
using System.Collections;
using hadrack.gpst.core.events;

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
    public GameObject gunFireEffect;
    private GameObject engineSmokeEffect;
    private GameObject engineFireEffect;
    public int health = 50;
    public int maxHealth;
    [SerializeField] private Pilot pilot;
    public float reloadTime = 0.3f;
    private bool reloaded = true;

    void Start()
    {
        maxHealth = health;
        rb = GetComponent<Rigidbody>();
        bulletPool = new ObjectPool<GameObject>(() => Instantiate(bulletPrefab));
        engineSmokeEffect = transform.Find("EngineSmoke").gameObject;
        engineFireEffect = transform.Find("EngineFire").gameObject;
        PopulatePool(1000);

    }
    void PopulatePool(int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            SetTag(bullet.transform, gameObject.tag + "Bullet");
            bullet.SetActive(false);
            bulletPool.Release(bullet);
        }
    }
    void SetTag(Transform parentTransform, string tag)
    {
        parentTransform.gameObject.tag = tag;
        // Iterate through all child objects
        foreach (Transform childTransform in parentTransform)
        {
            childTransform.gameObject.tag = tag;
        }
    }
    void Update()
    {
        if (health > 0)
        {
            ControlPlane();
            ControlGun();
        }
        else
            Fall();

        DamageEffects();
    }

    private void ControlPlane()
    {
        (float horizontalInput, float verticalInput, float forwardSpeed, float rudderRotation) = pilot.ControlPlane(maxSpeed);

        // rotation
        transform.Rotate(new Vector3(0, 0, -1), horizontalInput * flapsRotationSpeed * Time.deltaTime);
        // pitch
        transform.Rotate(Vector3.right, verticalInput * pitchSpeed * Time.deltaTime);
        // rudder
        transform.Rotate(new Vector3(0, rudderRotation, 0), rudderRotationSpeed * Time.deltaTime);

        // Basic "physics"
        if (horizontalInput == 0 && verticalInput == 0)
            RotateToFoward();

        // Move the plane forward
        rb.velocity = transform.forward * forwardSpeed;

        // Wind speed visual efect
        speedEffect.SetActive(forwardSpeed > 50f);
    }

    private void Fall()
    {
        rb.useGravity = true;
        transform.Rotate(new Vector3(0, 0, -1), 50 * Time.deltaTime);
    }


    void reload() { reloaded = true; }

    private void FireMissle()
    {
        GameObject missile = Instantiate(missilePrefab, transform.position, transform.rotation);
        MissileController missileController = missile.GetComponent<MissileController>();
        missileController.missileSpeed = 500f;

    }

    private void ControlGun()
    {
        // Gun / Missle Control
        if (pilot.IsFiring() && reloaded == true)
        {

            FireGun();
            reloaded = false;
            Invoke("reload", reloadTime);
        }
        else if (pilot.IsFiring())
            gunFireEffect.SetActive(true);
        else
            gunFireEffect.SetActive(false);
    }
    private void FireGun()
    {

        GameObject bullet = bulletPool.Get();


        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.transform.GetChild(0).localPosition = new Vector3(5f, 0f, 5f);
        bullet.transform.GetChild(1).localPosition = new Vector3(-5f, 0f, 5f);
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

    private void DamageEffects()
    {
        if (health < 4)
        {
            engineFireEffect.SetActive(true);
            engineSmokeEffect.SetActive(true);

        }
        else if (health < 7)
        {
            engineSmokeEffect.SetActive(true);
        }

    }


}
