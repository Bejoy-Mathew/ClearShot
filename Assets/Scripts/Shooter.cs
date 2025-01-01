using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firingPoint;

    void FireProjectile()
    {
        
        Vector3 direction = Camera.main.transform.forward;
        GameObject projectile = Instantiate(projectilePrefab, firingPoint.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().direction = direction;
    }

    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FireProjectile();
        }

    }
}
