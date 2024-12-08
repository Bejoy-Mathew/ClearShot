using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public void CompleteLevel(int stars, int score)
    {
        
        GameManager.Instance.SetLevelData(stars, score);
        GameManager.Instance.SetLastLevel(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("TransitionScene");
    }
}
