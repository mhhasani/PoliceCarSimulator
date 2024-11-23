using UnityEngine;

public class CarController : MonoBehaviour
{
    // Movement variables
    public float speed = 10f; // Speed of the car
    public float turnSpeed = 50f; // Turning speed of the car

    // Headlights
    public Light frontLightLeft;
    public Light frontLightRight;

    // Siren lights
    public Light sirenLightLeft;
    public Light sirenLightRight;

    // Audio for the siren
    private AudioSource sirenAudio;

    // Toggle states
    private bool lightsOn = false; // State for headlights
    private bool sirenActive = false; // State for siren (sound and increased intensity)

    // Siren blinking logic
    private float switchInterval = 0.75f; // Time interval for switching siren lights
    private float timer = 0.5f; // Timer for controlling siren light switching
    private bool isSirenLightLeftOn = false; // State of left siren light
    private bool isSirenLightRightOn = false; // State of right siren light

    void Start()
    {
        // Get the AudioSource component from the GameObject
        sirenAudio = GetComponent<AudioSource>();

        // Set initial intensity for siren lights
        sirenLightLeft.intensity = 5;
        sirenLightRight.intensity = 5;
    }

    void Update()
    {
        HandleCarMovement(); // Manage car movement
        HandleHeadlights(); // Manage headlights toggle
        HandleSirenLights(); // Manage siren lights' blinking
        HandleSirenSound(); // Manage siren sound and light intensity toggle
    }

    // Handles car movement and turning
    private void HandleCarMovement()
    {
        // Forward/backward movement
        float move = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(Vector3.forward * move);

        // Left/right turning
        float turn = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * turn);
    }

    // Toggles headlights on/off when the G key is pressed
    private void HandleHeadlights()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            lightsOn = !lightsOn; // Toggle the state
            frontLightLeft.enabled = lightsOn;
            frontLightRight.enabled = lightsOn;
        }
    }

    // Controls the alternating blinking of siren lights
    private void HandleSirenLights()
    {
        // Update the timer
        timer += Time.deltaTime;

        // Switch lights when the timer exceeds the interval
        if (timer >= switchInterval)
        {
            if (isSirenLightLeftOn)
            {
                // Turn off the left light and turn on the right light
                sirenLightLeft.enabled = false;
                sirenLightRight.enabled = true;
            }
            else
            {
                // Turn on the left light and turn off the right light
                sirenLightLeft.enabled = true;
                sirenLightRight.enabled = false;
            }

            // Toggle the states
            isSirenLightLeftOn = !isSirenLightLeftOn;
            isSirenLightRightOn = !isSirenLightRightOn;

            // Reset the timer
            timer = 0f;
        }
    }

    // Toggles the siren sound and increases/decreases siren light intensity with the Space key
    private void HandleSirenSound()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sirenActive = !sirenActive; // Toggle the siren state

            if (sirenActive)
            {
                // Increase siren light intensity for active state
                sirenLightLeft.intensity = 20;
                sirenLightRight.intensity = 20;

                // Play siren audio if available and not already playing
                if (sirenAudio != null && !sirenAudio.isPlaying)
                {
                    sirenAudio.Play();
                }
            }
            else
            {
                // Reduce siren light intensity to blinking state
                sirenLightLeft.intensity = Mathf.PingPong(Time.time * 5, 10);
                sirenLightRight.intensity = Mathf.PingPong(Time.time * 5, 10);

                // Stop siren audio if playing
                if (sirenAudio != null && sirenAudio.isPlaying)
                {
                    sirenAudio.Stop();
                }
            }
        }
    }
}
