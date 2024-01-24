using UnityEngine;

public class PropellerAnimation : MonoBehaviour
{
    public float rotationSpeed = 500.0f; // Adjust the speed of rotation

    void Update()
    {
        // Rotate the propeller smoothly
        RotatePropeller();
    }

    // Rotate the propeller smoothly
    private void RotatePropeller()
    {
        // Rotate the propeller around its local axis
        transform.Rotate(new Vector3(0,1,0), rotationSpeed * Time.deltaTime);
    }
}
