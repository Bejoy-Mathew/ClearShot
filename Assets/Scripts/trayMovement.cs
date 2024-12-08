using UnityEngine;

public class trayMovement : MonoBehaviour
{    
    public float speed = 2f;
    public bool randomMovement = true; 
    private Vector2 targetPosition;

    
    private float leftBound;
    private float rightBound;

    void Start()
    {
       
        Camera mainCamera = Camera.main;
        float cameraWidth = mainCamera.orthographicSize * mainCamera.aspect;
        leftBound = mainCamera.transform.position.x - cameraWidth;
        rightBound = mainCamera.transform.position.x + cameraWidth;

        SetRandomTargetPosition();
    }

    void Update()
    {
        
        if (randomMovement)
        {
            MoveRandomly();
        }
        else
        {            
            MoveContinuously();
        }
    }

    
    void MoveRandomly()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if ((Vector2)transform.position == targetPosition)
        {
            SetRandomTargetPosition();
        }
    }


    void MoveContinuously()
    {
        if (transform.position.x < leftBound || transform.position.x > rightBound)
        {
            speed = -speed; 
        }

        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }


    void SetRandomTargetPosition()
    {
        float randomX = Random.Range(leftBound, rightBound);
        targetPosition = new Vector2(randomX, transform.position.y);
    }
}
