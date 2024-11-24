using System;
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
    private Vector2 designatedPosition = new Vector2(0, -3.5f);
    private Vector2 screenBounds;
    private bool isDragging = false;
    private int targets;
    int targetCount;
    public GameObject menuPanel;     
    public TMP_Text GameResult;
    public AudioClip GunShot; 
    private AudioSource audioSource; 


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        int targetLayer = LayerMask.NameToLayer("Target");
        targetCount = CountObjectsWithLayer(targetLayer);
    }

    void Update()
    {
        // Handle Mouse Down or Touch Begin
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Vector3 inputPosition = GetInputPosition();
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(inputPosition.x, inputPosition.y, Camera.main.nearClipPlane));

            if (Vector2.Distance(worldPosition, crosshairTransform.position) < 0.5f)
            {
                isDragging = true;
            }
        }

        // Handle Mouse Drag or Touch Drag (moving crosshair)
        if ((Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)) && isDragging)
        {
            MoveCrosshair();
        }

        // Handle Mouse Up or Touch End (Firing)
        if (isDragging && (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)))
        {
            if (Input.touchCount > 0)
            {
                // For touch input, fire using the touch position
                Touch touch = Input.GetTouch(0);
                FireBullet(touch.position);
            }
            else
            {
                // For mouse input, fire using the mouse position
                FireBullet(Input.mousePosition);
            }

            isDragging = false;
            ReturnCrosshair();  // Return crosshair to its original position
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

        // Set the new crosshair position
        crosshairTransform.position = new Vector3(clampedX, clampedY, crosshairTransform.position.z);
    }
    private void ReturnCrosshair()
    {

        crosshairTransform.position = new Vector3(designatedPosition.x, designatedPosition.y, crosshairTransform.position.z);
        
    }

    private void FireBullet(Vector3 crosshairPosition)
    {
        audioSource.PlayOneShot(GunShot);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(crosshairPosition);
        Vector2 rayDirection = Vector2.zero;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(worldPosition, rayDirection, 50);
        

        if (raycastHit2D.collider != null)
        {

            
            if (raycastHit2D.collider.gameObject.layer == 6)
            {
                GameObject target = new GameObject();
                target = raycastHit2D.collider.gameObject;
                Vector2 direction = (target.transform.position - transform.position).normalized;
                Debug.DrawRay(transform.position, direction * 5, Color.red);

                GameObject targetToDestroy;
                if (raycastHit2D.collider.gameObject.transform.parent == null)
                {                    
                    targetToDestroy = raycastHit2D.collider.gameObject;
                }
                else
                {                    
                    targetToDestroy = raycastHit2D.collider.transform.parent.gameObject;
                }

                Debug.Log("target Hit : " + raycastHit2D.collider.name);
                Destroy(targetToDestroy);
                targetCount--;

                if (targetCount == 0)
                {
                    if (HasNextScene())
                    {
                        Invoke("LoadNextScene", GunShot.length);
                        //LoadNextScene();
                    }
                    else
                    {
                        Invoke("LoadNextScene", GunShot.length);
                        GameResult.text = "You WIN !!!";
                        menuPanel.SetActive(true);
                        Debug.Log("You Win !!! Game Over ");
                    }
                }
            }
            else
            {
                Debug.Log("you have hit environment");
            }
        }
    }

    int CountObjectsWithLayer(int layer)
    {        
        GameObject[] allObjects = GameObject.FindObjectsByType<GameObject>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);        
        int count = 0;
        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == layer)
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
}
