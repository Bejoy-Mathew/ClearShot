using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BottleDestroy : MonoBehaviour
{
    int destroyedCount;


    void Update()
    {
        float cameraLowerLimit = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;

        if (transform.position.y < cameraLowerLimit)
        {
            StartCoroutine(playDestroy()); 
        }
    }

    IEnumerator playDestroy()
    {
        AudioSource soundEffect = GetComponent<AudioSource>();
        if (soundEffect != null)
        {
            soundEffect.Play();
        }
        else
        {
            Debug.LogWarning("No AudioSource found on the GameObject.");
        }

       
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }

       
        ParticleSystem particleEffect = GetComponentInChildren<ParticleSystem>();
        if (particleEffect != null)
        {
            particleEffect.Play();
            yield return new WaitForSeconds(particleEffect.main.startLifetime.constantMax);
            Destroy(gameObject);
            destroyedCount++;
            Debug.Log(destroyedCount);
            GameManager.Instance.RegisterTargetDestroyed(gameObject);
        }
        else
        {
            Debug.LogWarning("No ParticleSystem found in children.");
        }

        
        
    }
}
