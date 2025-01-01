using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEngine.GraphicsBuffer;

public class crosshairScript : MonoBehaviour
{
    [SerializeField] private Transform crosshairTransform;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private int bullets;
    [SerializeField] private float gameTime;
    public TMP_Text bulletCount;
    private Vector2 designatedPosition = new Vector2(0, -3.5f);
    private Vector2 screenBounds;
    private bool isDragging = false;
    private int targets;
    int targetCount;
    public GameObject menuPanel;
    public TMP_Text GameResult;    
    public bool isDestroyedByRaycast = false;
    private ParticleSystem particle;
    private float timeLimit ; 
    private bool isGameActive = false;
    public TMP_Text timerText;
    public GameObject gamePanel;
    int currentBullet;
    int destroyedCount;
    public LevelCondition _currentLevelCondition;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {

        GameManager.Instance.destroyedTargets = 0;
        GameManager.Instance.gameResult = "";
        GameManager.Instance.lastLevelBuildIndex=SceneManager.GetActiveScene().buildIndex;
        GameManager.Instance.currentLevelCondition = _currentLevelCondition;
        destroyedCount = 0;
        gamePanel.SetActive(true);
        Time.timeScale = 0;
        timeLimit = gameTime;                
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        int targetLayer = LayerMask.NameToLayer("Target");
        targetCount = CountObjectsWithLayer(targetLayer);
        currentBullet = bullets;
        bulletCount.text = currentBullet.ToString();
        GameManager.Instance.stars = 0;        

    }

    void Update()
    {
        if (isGameActive)
            timer();

        if(isGameActive && Time.timeScale!=0)
        {
            
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                Vector3 inputPosition = GetInputPosition();
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(inputPosition.x, inputPosition.y, Camera.main.nearClipPlane));

                if (Vector2.Distance(worldPosition, crosshairTransform.position) < 0.5f)
                {
                    isDragging = true;
                }
            }

            
            if ((Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)) && isDragging)
            {
                MoveCrosshair();
            }

            
            if (isDragging && (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)))
            {
                if (Input.touchCount > 0)
                {
                    
                    Touch touch = Input.GetTouch(0);
                    FireBullet(touch.position);
                }
                else
                {
                    
                    FireBullet(Input.mousePosition);
                }

                isDragging = false;
                ReturnCrosshair();  
            }
        }
        
    }

    private Vector3 GetInputPosition()
    {
        if (Input.touchCount > 0)
        {
            return Input.GetTouch(0).position;
        }
        else
        {
            return Input.mousePosition;
        }
    }

    private void MoveCrosshair()
    {
        Vector3 inputPosition = GetInputPosition();


        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(inputPosition.x, inputPosition.y, Camera.main.nearClipPlane));


        float clampedX = Mathf.Clamp(worldPosition.x, -screenBounds.x, screenBounds.x);
        float clampedY = Mathf.Clamp(worldPosition.y, -screenBounds.y, screenBounds.y);

        
        crosshairTransform.position = new Vector3(clampedX, clampedY, crosshairTransform.position.z);
    }
    private void ReturnCrosshair()
    {

        crosshairTransform.position = new Vector3(designatedPosition.x, designatedPosition.y, crosshairTransform.position.z);

    }

    private void FireBullet(Vector3 crosshairPosition)
    {
        if (currentBullet > 0)
        {
            currentBullet--;
            bulletCount.text = currentBullet.ToString();
            audioManager.PlaySfx(audioManager.gunShot);

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(crosshairPosition);
            Vector2 rayDirection = Vector2.zero;
            RaycastHit2D[] allHits = Physics2D.RaycastAll(worldPosition, rayDirection, 1f);

            List<RaycastHit2D> sortedHits = new List<RaycastHit2D>(allHits);
            sortedHits.Sort((a, b) =>
            {
                SpriteRenderer rendererA = a.collider.GetComponent<SpriteRenderer>();
                SpriteRenderer rendererB = b.collider.GetComponent<SpriteRenderer>();


                if (rendererA != null && rendererB != null)
                {
                    return rendererB.sortingOrder.CompareTo(rendererA.sortingOrder);
                }
                return 0;
            });

            foreach (RaycastHit2D hit in sortedHits)
            {

                if (hit.collider != null && ((1 << hit.collider.gameObject.layer) & targetLayer) != 0)
                {
                    SpriteRenderer hitRenderer = hit.collider.GetComponent<SpriteRenderer>();


                    if (hitRenderer != null && hitRenderer.sortingOrder == sortedHits[0].collider.GetComponent<SpriteRenderer>().sortingOrder)
                    {
                        GameObject target = new GameObject();
                        target = hit.collider.gameObject;
                        Vector2 direction = (target.transform.position - transform.position).normalized;
                        Debug.DrawRay(transform.position, direction * 5, Color.red);


                        Debug.Log("target Hit : " + hit.collider.name);
                        isDestroyedByRaycast = true;
                        StartCoroutine(Destroying(hit));
                        
                    }
                    else
                    {
                        Debug.Log("you have hit environment");
                    }
                }
            }
        }
        else
        {
            audioManager.PlaySfx(audioManager.emptyGunShot);
        }
        

            
    }

    int CountObjectsWithLayer(int layer)
    {        

        int layerC = LayerMask.NameToLayer("Target");
        GameObject[] allObjects = GameObject.FindObjectsByType<GameObject>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        int count = 0;

        foreach (GameObject obj in allObjects)
        {
            
            if (obj.layer == layerC &&
                obj.GetComponent<SpriteRenderer>() != null &&
                (obj.GetComponent<Collider2D>() != null || obj.GetComponent<Collider>() != null))
            {
                count++;
            }
        }

        return count;
    }

    bool HasNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        return currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1;
    }
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            Debug.Log("This is the last scene. No next scene to load.");
        }
    }

    private IEnumerator Destroying(RaycastHit2D raycastHit2D)
    {
        SpriteRenderer spriteRenderer = raycastHit2D.collider.gameObject.GetComponent<SpriteRenderer>();
        AudioSource breakSound=raycastHit2D.collider.gameObject.GetComponent<AudioSource>();
        GameObject targetToDestroy;
        if (raycastHit2D.collider.gameObject.transform.parent == null)
        {
            targetToDestroy = raycastHit2D.collider.gameObject;
        }
        else
        {
            targetToDestroy = raycastHit2D.collider.transform.parent.gameObject;
        }

        particle = raycastHit2D.collider.gameObject.GetComponentInChildren<ParticleSystem>();
        if (particle != null)
        {
            breakSound.Play();
            spriteRenderer.enabled = false;
            particle.Play();
            yield return new WaitForSeconds(particle.main.startLifetime.constantMax);
            
            Destroy(targetToDestroy);
            
        }
        else 
        {
            Destroy(targetToDestroy);
        }
        destroyedCount++;
        GameManager.Instance.RegisterTargetDestroyed(targetToDestroy);
        GameManager.Instance.timeRemaing = timeLimit;
        

    }

    private void timer()
    {
        if (isGameActive)
        {
            timeLimit -= Time.deltaTime;
            timerText.text = Mathf.Ceil(timeLimit).ToString();

            if (timeLimit <= 0)
            {
                OnTimeUp();
            }
        }
    }

    void OnTimeUp()
    {
        isGameActive = false;
        Debug.Log("Time's up!");
        Time.timeScale = 0;
        GameManager.Instance.gameResult = "Time's Up !!! \n Game Over";
        SceneManager.LoadScene("TransitionScene");
    }

    public void GameStart()
    {        
        gamePanel.SetActive(false);
        isGameActive = true;
        Time.timeScale = 1;
    }
}