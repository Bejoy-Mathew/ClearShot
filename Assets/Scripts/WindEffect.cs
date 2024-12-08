using UnityEngine;

public class WindEffect : MonoBehaviour
{
    public float maxWindStrength = 5f;
    public float windChangeSpeed = 0.5f;
    private float currentWindStrength;

    void Update()
    {
        currentWindStrength = Mathf.Lerp(currentWindStrength, Random.Range(-maxWindStrength, maxWindStrength), windChangeSpeed * Time.deltaTime);
        foreach (var bottle in FindObjectsByType<BottleSway>(FindObjectsSortMode.None))
        {
            bottle.windStrength = currentWindStrength;
        }
    }
}
