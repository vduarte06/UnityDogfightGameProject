using UnityEngine;

public class AIPlaneController : MonoBehaviour
{
    public float forwardSpeed = 10.0f;
    public float rotationSpeed = 3.0f;
    public float pitchSpeed = 2.0f;
    private Rigidbody rb;

    private float zeroProbability = 0.7f; // Probability for selecting 0 (70%)
    private float randomInput = 0;
    void Start()
    {
        // Call the method to generate a random number every 3 seconds
        InvokeRepeating("GenerateRandomNumber", 0f, 3f);
        rb = GetComponent<Rigidbody>();
    }

    // Method to generate a random number
    private void GenerateRandomNumber()
    {
        float randomNumber = Random.value; // Generate a random float value between 0.0 and 1.0
        float rand = UnityEngine.Random.Range(0, 1f); // gos
        randomInput = (randomNumber < zeroProbability) ? 0 : rand;

    }

    void Update()
    {
        // Plane rotation based on horizontal input (yaw)
        float horizontalInput = randomInput;
        float verticalInput = 0 ;
        transform.Rotate(new Vector3(0, 0.05f, -0.5f), horizontalInput * rotationSpeed * Time.deltaTime);

        // Plane pitch based on vertical input
        transform.Rotate(Vector3.right, verticalInput * pitchSpeed * Time.deltaTime);


        // Basic physics
        if (horizontalInput == 0 && verticalInput == 0)
            RotateToFoward();


        // Move the plane forward at a fixed speed
        rb.velocity = transform.forward * forwardSpeed;

       
    }
    

    
    private void RotateToFoward()
    {
        Quaternion targetRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0); // Target rotation Quaternion (0, 0, 0)

        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, (rotationSpeed / 50) * Time.deltaTime);
    }
}
