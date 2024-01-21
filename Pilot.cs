
using UnityEngine;
public interface IPilot
{
    (float, float, float, float) ControlPlane(float maxSpeed);
    bool IsFiring();

}

public abstract class Pilot : MonoBehaviour, IPilot
{
    public abstract (float, float, float, float) ControlPlane(float maxSpeed);
    public abstract bool IsFiring();

}


