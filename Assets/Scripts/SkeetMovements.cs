using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkeetMovements: MonoBehaviour
{
    public float speed; 
    public float height ; 
    public GameObject menuPanel;
    private Vector2 startPosition;
    private Vector2 targetPosition;
    private float timeAlive;
    public TMP_Text GameResult;


    void Start()
    {
        
        menuPanel.SetActive(false);
        startPosition = transform.position;

        Camera cam = Camera.main;
        Vector2 screenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));
        targetPosition = new Vector2(-startPosition.x, startPosition.y);
    }

    void Update()
    {
        timeAlive += Time.deltaTime * speed;
        float t = Mathf.Clamp01(timeAlive);

        float x = Mathf.Lerp(startPosition.x, targetPosition.x, t);
        float y = Mathf.Lerp(startPosition.y, targetPosition.y, t) + height * Mathf.Sin(t * Mathf.PI); // Parabolic arc just to mimic the gravity arc

        // Set the new position
        transform.position = new Vector2(x, y);

        // Destroy the object once it reaches the target
        if (t >= 1f)
        {
            Destroy(gameObject);

            GameResult.text = "Game Over";
            menuPanel.SetActive(true);
        }
        else if (gameObject.IsDestroyed())
        {
            GameResult.text = "You WIN !!!";
            menuPanel.SetActive(true);
        }
    }
}
