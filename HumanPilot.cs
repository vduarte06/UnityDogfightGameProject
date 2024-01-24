
using UnityEngine;

class Human : Pilot
{
   
    private bool isFiring = false;
    public float mouseSensitivity = 5.0f;

    public override (float, float, float, float) ControlPlane(float maxSpeed)
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        // Alternative way to control
        // Vector2 mousePosition = Input.mousePosition;
        // Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        // float horizontalDistance = mousePosition.x - screenCenter.x;
        // float verticalDistance = mousePosition.y - screenCenter.y;
        // Debug.Log(horizontalDistance/Screen.width);
        // mouseX = horizontalDistance/Screen.width;
        // mouseY = verticalDistance/Screen.width;

        mouseX = Mathf.Clamp(mouseX, -1f, 1f);
        mouseY = Mathf.Clamp(mouseY, -1, 1);

        if(mouseY != 0)
            verticalInput = mouseY;
        if(mouseX != 0)
            horizontalInput = mouseX;
        float forwardSpeed;

        if (Input.GetKey(KeyCode.LeftShift)){
            forwardSpeed = maxSpeed;
        }
            
        else{
            forwardSpeed = maxSpeed/2;
        }
        
        float rudderRotation = 0;
        if (Input.GetKey(KeyCode.Q))
            rudderRotation = -1;
        else if (Input.GetKey(KeyCode.E))
            rudderRotation = 1;
        return (horizontalInput, verticalInput, forwardSpeed, rudderRotation);

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