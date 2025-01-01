using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed = 10f;  
    public Vector3 direction;  


    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
