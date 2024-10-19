using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class BulletManager : MonoBehaviour
{
    public int totalBullets;
    private int currentBullets;      //bulletleft     
    public Image[] bulletIcons;
    [Serialize] private LayerMask targetLayer;
    public float rayDistance = 10f;
    public Transform firePoint;
    public int score = 0;


    void Start()
    {
        /// currentBullets -> the bullets remaining after shooting 
        /// totalBullets -> the total number of bullets u have for a current level
        currentBullets = totalBullets;

        /// this is to handle the bullet image which is being handled in the UI
        UpdateBulletUI();
    }

    void Update()
    {
        /// this is condition is just to check that we can only play or shoot if we have at least 1 bullet remaining 
        if (currentBullets > 0)
        {

            /// handling the touch event to shoot for the mobile screen
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    /// calling the shoot mechanism
                    ShootRayFromTap(touch.position);
                }
            }
            /// hanlding the mouse button click
            else if (Input.GetMouseButtonDown(0))
            {
                /// calling the shoot mechanism
                ShootRayFromTap(Input.mousePosition);
            }
        }
        else
        {

            //Debug.Log("No bullets left!");
            /// pausing the game currently 
            Time.timeScale = 0;
        }
    }

    void ShootRayFromTap(Vector2 tapPosition)
    {
        /// converting the tapping position of the phone screen/(phone resolution) into the unity's cordinates
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(tapPosition);
        /// this is for shooting direction.. since its on the tap .. so kept it 0
        Vector2 rayDirection = Vector2.zero;

        /// the shooting mechanism using ray cast method 
        RaycastHit2D raycastHit2D = Physics2D.Raycast(worldPosition, rayDirection, rayDistance);

        /// bullet counter
        UseBullet();


        if (raycastHit2D.collider != null)
        {

            /// to make sure we hit the target only and not environment as i have set the bunny as the target layer and trees and grass as environment layers
            if (raycastHit2D.collider.gameObject.layer == 6)
            {
                /// head shot detection
                if (raycastHit2D.collider.name == "Head")
                {
                    score += 50;
                    Debug.Log("Headshot! +50 points");

                }
                /// body shot detection
                else
                {
                    score += 20;
                    Debug.Log("Body shot! +20 points");
                }

                /// this has been added because of the head collider which is the child of the main bunnny game object
                GameObject targetToDestroy;
                if (raycastHit2D.collider.gameObject.transform.parent == null)
                {
                    /// getting the game object ...its the parentobject.. in this case Body shot 
                    targetToDestroy = raycastHit2D.collider.gameObject;
                }
                else
                {
                    /// getting the game object of parent ....in this case it because of headshot  and make sure the child colider is also layer tagged as target 
                    targetToDestroy = raycastHit2D.collider.transform.parent.gameObject;
                }

                Debug.Log("target Hit : " + raycastHit2D.collider.name);
                Destroy(targetToDestroy);
            }
            else
            {
                Debug.Log("you have hit environment");
            }
        }
    }

    void UseBullet()
    {
        currentBullets--;
        UpdateBulletUI();
    }

    void UpdateBulletUI()
    {
        for (int i = 0; i < bulletIcons.Length; i++)
        {
            bulletIcons[i].enabled = (i < currentBullets);
        }
    }
}
