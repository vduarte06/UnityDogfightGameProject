
using UnityEngine;

class BasicAi : Pilot
{
    private bool isFiring = false;
    public float gunFireAngleThreshold = 10;
    public float gunFireDistanceThreshold = 500;
    private float angleToTarget;
    private float distanceToTarget;
   public Transform target; 

    // Method for AI pilot to control the plane's movement
    public override (float, float, float, float) ControlPlane(float maxSpeed)
    {
        // Get the direction, distance and rotation needed
        Vector3 directionToTarget = target.position - transform.position;
        distanceToTarget = directionToTarget.magnitude;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        angleToTarget = Quaternion.Angle(transform.rotation, targetRotation); 

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
        Vector3 directionToTarget = new Vector3(targetTransform.position.x - transform.position.x, 0f, targetTransform.position.z - transform.position.z);

        Vector3 forwardDirection = new Vector3(transform.forward.x, 0f, transform.forward.z);

        float dotProduct = Vector3.Dot(directionToTarget.normalized, forwardDirection.normalized);

        if (dotProduct < 0f)
        {
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

    void FireGun(){
    
    }

    public override bool IsFiring()
    {
        if (angleToTarget < gunFireAngleThreshold && distanceToTarget < gunFireDistanceThreshold)
            isFiring = true;
        else
            isFiring = false;
        return isFiring;
    }
    
}