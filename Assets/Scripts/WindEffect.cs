using UnityEngine;

public class WindEffect : MonoBehaviour
{
    public float maxWindStrength = 5f;
    public float windChangeSpeed = 0.5f;
    private float currentWindStrength;

    void Update()
    {
        // Smoothly change the wind strength over time
        currentWindStrength = Mathf.Lerp(currentWindStrength, Random.Range(-maxWindStrength, maxWindStrength), windChangeSpeed * Time.deltaTime);

        // Apply the wind effect to all bottles
        foreach (var bottle in FindObjectsByType<BottleSway>(FindObjectsSortMode.None))
        {
            bottle.windStrength = currentWindStrength;
        }
    }
}
