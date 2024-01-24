using UnityEngine;

public class GunSoundEffectController : MonoBehaviour
{
    public AudioClip pressSound;   // Audio clip to play when the left mouse button is pressed
    public AudioClip releaseSound; // Audio clip to play when the left mouse button is released
    private AudioSource audioSource;
    PlaneController planeController;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        planeController = GetComponent<PlaneController>();
    }

    void Update()
    {
        if (planeController.health > 0)
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
}
