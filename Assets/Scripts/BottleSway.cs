using UnityEngine;

public class BottleSway : MonoBehaviour
{
    public float swaySpeed = 2f;         // How fast the bottle sways back and forth
    public float swayAmount = 15f;       // How far the bottle sways (in degrees)
    public float windStrength = 1f;      // The strength of the wind effect
    public float windRandomness = 1f;    // Controls how unpredictable the wind is

    private Rigidbody2D rb;
    private float angle;                // Current sway angle
    private float windDirection;        // The direction of the wind (randomized)

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        // Set initial wind direction
        windDirection = Random.Range(-1f, 1f);
    }

    void Update()
    {
        // Apply the sway force to simulate the pendulum effect
        angle = Mathf.Sin(Time.time * swaySpeed) * swayAmount;

        // Apply the wind effect (a constant force that affects the sway)
        float windForce = Mathf.PerlinNoise(Time.time * windRandomness, 0) * windStrength;
        angle += windForce * windDirection;

        // Apply the calculated sway as torque to the Rigidbody2D
        rb.MoveRotation(angle);
    }
}
