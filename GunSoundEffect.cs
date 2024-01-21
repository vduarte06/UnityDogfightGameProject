using UnityEngine;

public class GunSoundEffectController : MonoBehaviour
{
    public AudioClip pressSound;   // Audio clip to play when the left mouse button is pressed
    public AudioClip releaseSound; // Audio clip to play when the left mouse button is released

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button press
        {
            // Play the pressSound audio clip when the left mouse button is pressed
            if (pressSound != null)
            {
                audioSource.clip = pressSound;
                audioSource.loop = true;
                audioSource.Play();
                
            }
        }
        else if (Input.GetMouseButtonUp(0)) // Check for left mouse button release
        {
            // Play the releaseSound audio clip when the left mouse button is released
            if (releaseSound != null)
            {
                audioSource.clip = releaseSound;
                audioSource.loop = false;
                audioSource.Play();
            }
        }
    }
}
