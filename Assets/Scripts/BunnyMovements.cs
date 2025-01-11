using UnityEngine;

public class BunnyMovements : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float maxX = 2.8f;           // boundary 
    public float minX = -2.8f;
    public float groundHeight = -2.8f;



    void Update()
    {


        float xPosition = transform.position.x - moveSpeed * Time.deltaTime;

        transform.position = new Vector3(xPosition, groundHeight, transform.position.z);

        if (transform.position.x >= maxX)
        {
            if(transform.rotation== Quaternion.Euler(0, 180, 0))
                transform.rotation = Quaternion.Euler(0, 0, 0);
            else
                transform.rotation = Quaternion.Euler(0, 180, 0);


            moveSpeed = -moveSpeed;
        }
        else if (transform.position.x <= minX)
        {
            if (transform.rotation == Quaternion.Euler(0, 180, 0))
                transform.rotation = Quaternion.Euler(0, 0, 0);
            else
                transform.rotation = Quaternion.Euler(0, 180, 0);
            moveSpeed = -moveSpeed;
        }

    }
}
