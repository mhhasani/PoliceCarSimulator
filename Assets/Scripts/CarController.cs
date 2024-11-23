using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 10f;
    public float turnSpeed = 50f;

    // Headlights
    public Light frontLightLeft;
    public Light frontLightRight;
    private bool lightsOn = false;

    // Siren lights
    public Light sirenLightLeft;
    public Light sirenLightRight;
    private float switchInterval = 0.75f; // Time interval for switching siren lights
    private float timer = 0f; // Timer to control switching
    private bool isSirenLightLeftOn = false;

    void Update()
    {
        // Handle car movement
        float move = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(Vector3.forward * move);

        // Handle car turning
        float turn = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * turn);

        // Toggle headlights with G key
        if (Input.GetKeyDown(KeyCode.G))
        {
            lightsOn = !lightsOn;
            frontLightLeft.enabled = lightsOn;
            frontLightRight.enabled = lightsOn;
        }

        // Control alternating siren lights
        timer += Time.deltaTime;
        if (timer >= switchInterval)
        {
            // Alternate the siren lights
            sirenLightLeft.enabled = !isSirenLightLeftOn;
            sirenLightRight.enabled = isSirenLightLeftOn;
            isSirenLightLeftOn = !isSirenLightLeftOn;

            // Reset the timer
            timer = 0f;
        }
    }
}
