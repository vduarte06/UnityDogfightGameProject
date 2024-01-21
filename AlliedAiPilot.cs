
using UnityEngine;

class AlliedAI : Pilot
{
   
    private bool isFiring = false;
   public Transform target; 

    // Method for AI pilot to control the plane's movement
    public override (float, float, float, float) ControlPlane(float maxSpeed)
    {
        Vector3 targetDirection = GetTargetDirection();

        // Calculate the angle difference between the current plane's forward direction and the target direction
        float angleDifference = Vector3.SignedAngle(transform.forward, targetDirection, Vector3.up);

        float horizontalInput = 0f;
        float verticalInput = 0f;
        float forwardSpeed = maxSpeed; 

        // Adjust the horizontal input based on the angle difference
        if (angleDifference > 1f)
        {
            horizontalInput = 1f; // Turn right
        }
        else if (angleDifference < -1f)
        {
            horizontalInput = -1f; // Turn left
        }
        return (0, verticalInput, forwardSpeed, horizontalInput);
    }

    private Vector3 GetTargetDirection()
    {
        Vector3 directionToTarget = target.transform.forward; 
        return directionToTarget;
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