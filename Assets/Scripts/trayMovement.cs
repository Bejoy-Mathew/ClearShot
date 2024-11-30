using UnityEngine;

public class trayMovement : MonoBehaviour
{

    // Movement speed and control variables
    public float speed = 2f;
    public bool randomMovement = true; // If false, no random movement, sprite moves continuously
    private Vector2 targetPosition;

    // Camera boundaries
    private float leftBound;
    private float rightBound;

    // Start is called before the first frame update
    void Start()
    {
        // Calculate camera boundaries based on the camera's position and orthographic size
        Camera mainCamera = Camera.main;
        float cameraWidth = mainCamera.orthographicSize * mainCamera.aspect;
        leftBound = mainCamera.transform.position.x - cameraWidth;
        rightBound = mainCamera.transform.position.x + cameraWidth;

        // Initial target position
        SetRandomTargetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        // If random movement is enabled, periodically change direction
        if (randomMovement)
        {
            MoveRandomly();
        }
        else
        {
            // If random movement is disabled, move toward the target position continuously
            MoveContinuously();
        }
    }

    // Method to move the sprite randomly left to right
    void MoveRandomly()
    {
        // Move towards the current target position
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // If the sprite reaches the target position, pick a new random target
        if ((Vector2)transform.position == targetPosition)
        {
            SetRandomTargetPosition();
        }
    }

    // Method to move the sprite continuously back and forth
    void MoveContinuously()
    {
        if (transform.position.x < leftBound || transform.position.x > rightBound)
        {
            speed = -speed; // Reverse direction if boundaries are hit
        }

        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    // Set a random target position within the camera's bounds
    void SetRandomTargetPosition()
    {
        float randomX = Random.Range(leftBound, rightBound);
        targetPosition = new Vector2(randomX, transform.position.y);
    }
}
