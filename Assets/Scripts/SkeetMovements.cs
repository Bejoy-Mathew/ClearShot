using UnityEngine;

public class SkeetMovements: MonoBehaviour
{
    public float speed; // Speed of the skeet
    public float height ; // Maximum height of the arc

    private Vector2 startPosition;
    private Vector2 targetPosition;
    private float timeAlive;

    void Start()
    {
        // Get the start position from the current position of the skeet
        startPosition = transform.position;

        // Calculate the target position (opposite edge of the camera)
        Camera cam = Camera.main;
        Vector2 screenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));
        targetPosition = new Vector2(-startPosition.x, startPosition.y);
    }

    void Update()
    {
        // Calculate the time alive normalized between 0 and 1
        timeAlive += Time.deltaTime * speed;
        float t = Mathf.Clamp01(timeAlive);

        // Calculate the position along the arc
        float x = Mathf.Lerp(startPosition.x, targetPosition.x, t);
        float y = Mathf.Lerp(startPosition.y, targetPosition.y, t) + height * Mathf.Sin(t * Mathf.PI); // Parabolic arc

        // Set the new position
        transform.position = new Vector2(x, y);

        // Destroy the object once it reaches the target
        if (t >= 1f)
        {
            Destroy(gameObject);
            Debug.Log("Game Over");
        }
    }
}
