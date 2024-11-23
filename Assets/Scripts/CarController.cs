using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 10f;
    public float turnSpeed = 50f;

    // Headlights
    public Light frontLightLeft;
    public Light frontLightRight;
    private bool lightsOn = false;

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
    }
}
