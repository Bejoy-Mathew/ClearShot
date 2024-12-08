using UnityEngine;

public class BottleSway : MonoBehaviour
{
    public float swaySpeed = 2f;        
    public float swayAmount = 15f;      
    public float windStrength = 1f;     
    public float windRandomness = 1f;   

    private Rigidbody2D rb;
    private float angle;                
    private float windDirection;        

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        windDirection = Random.Range(-1f, 1f);
    }

    void Update()
    {
        angle = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
        float windForce = Mathf.PerlinNoise(Time.time * windRandomness, 0) * windStrength;
        angle += windForce * windDirection;
        rb.MoveRotation(angle);
    }
}
