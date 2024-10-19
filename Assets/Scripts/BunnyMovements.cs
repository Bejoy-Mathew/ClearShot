using UnityEngine;

public class BunnyMovements : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float maxX = 2.8f;           // boundary 
    public float minX = -2.8f;
    public float groundHeight = -2.8f;
    private bool isIdle = false;        // Tracks if the rabbit is idling



    void Update()
    {


        float xPosition = transform.position.x - moveSpeed * Time.deltaTime;

        transform.position = new Vector3(xPosition, groundHeight, transform.position.z);

        /// the condition to check if the bunny is approxiamately near the border then flip and send it in other direction
        if (transform.position.x + .2 >= maxX && !isIdle)
        {
            /// Quaternion.Euler is used to flip the sprite
            transform.rotation = Quaternion.Euler(0, 0, 0);
            moveSpeed = -moveSpeed;
        }
        else if (transform.position.x - .2 <= minX)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            moveSpeed = -moveSpeed;
        }

    }
}
