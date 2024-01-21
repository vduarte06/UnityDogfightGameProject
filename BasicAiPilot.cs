
using UnityEngine;

class BasicAi : Pilot
{
   
    private bool isFiring = false;
   public Transform target; 

    // Method for AI pilot to control the plane's movement
    public override (float, float, float, float) ControlPlane(float maxSpeed)
    {
        // Get the direction towards the target
        Vector3 directionToTarget = target.position - transform.position;

        // Calculate the rotation needed to face the target direction
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

        // Get the angle differences between current rotation and target rotation in each axis
        float angleDifferenceX = Mathf.DeltaAngle(transform.rotation.eulerAngles.x, targetRotation.eulerAngles.x);
        float angleDifferenceY = Mathf.DeltaAngle(transform.rotation.eulerAngles.y, targetRotation.eulerAngles.y);
        float angleDifferenceZ = Mathf.DeltaAngle(transform.rotation.eulerAngles.z, targetRotation.eulerAngles.z);

        // Determine the inputs that will be given to the aircraft
        float rudderRotation = (angleDifferenceY > 1f) ? 1f : ((angleDifferenceY < -1f) ? -1f : 0f);
        float verticalOutput = (angleDifferenceX > 1f) ? 1f : ((angleDifferenceX < -1f) ? -1f : 0f);
        float flapsRotationOutput = (angleDifferenceY > 1f) ? 0.2f : ((angleDifferenceY < -1f) ? -0.2f : 0f);

        if (IsObjectBehindAndSameDirection(target)){
            rudderRotation = 0;
            verticalOutput = 0;
            flapsRotationOutput = 0;
        }
       
        return (flapsRotationOutput, verticalOutput, maxSpeed, rudderRotation);
    }

    bool IsObjectBehindAndSameDirection(Transform targetTransform)
    {
        // Calculate the direction from this object to the target in 2D (x, z)
        Vector3 directionToTarget = new Vector3(targetTransform.position.x - transform.position.x, 0f, targetTransform.position.z - transform.position.z);

        // Calculate the forward direction of this object in 2D (x, z)
        Vector3 forwardDirection = new Vector3(transform.forward.x, 0f, transform.forward.z);

        // Use the dot product to determine if the target is behind this object
        float dotProduct = Vector3.Dot(directionToTarget.normalized, forwardDirection.normalized);

        // Check if the target is behind
        if (dotProduct < 0f)
        {
            // Compare the magnitudes of the velocities (assuming both objects have Rigidbody components)
            Rigidbody thisRigidbody = GetComponent<Rigidbody>();
            Rigidbody targetRigidbody = targetTransform.GetComponent<Rigidbody>();

            if (thisRigidbody && targetRigidbody)
            {
                float velocityDotProduct = Vector3.Dot(thisRigidbody.velocity.normalized, targetRigidbody.velocity.normalized);

                // Check if the velocities are approximately in the same direction
                if (velocityDotProduct > 0.8f) // Adjust the threshold as needed
                {
                    return true;
                }
            }
        }

        return false;
    }

    public override bool IsFiring()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isFiring = true;
        }
        else if (Input.GetMouseButtonUp(0)) 
        {
            isFiring = false;
        }
        return isFiring;
    }
    
}