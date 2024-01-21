using UnityEngine;

public class CompassController : MonoBehaviour
{
    public Transform player; // Reference to the player
    public Transform enemyTarget; // Reference to the enemy target

    void Update()
    {
        if (enemyTarget != null && player != null)
        {
            // Calculate the direction from the player to the enemy target
            Vector3 directionToTarget = enemyTarget.position - player.position;
            Vector3 directionToTargetFlat = new Vector3(directionToTarget.x, 0f, directionToTarget.z);

            // Project the direction onto the player's forward direction (in local space)
            Vector3 localDirection = player.InverseTransformDirection(directionToTargetFlat);
            float angle = Mathf.Atan2(localDirection.z, localDirection.x) * Mathf.Rad2Deg;

            // Apply the rotation to the compass needle only in the z-axis
            transform.rotation = Quaternion.Euler(0f, 0f, angle + 90);
        }
    }
}
