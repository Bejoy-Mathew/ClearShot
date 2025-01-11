using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class TransitionScreen : MonoBehaviour
{
    public TMP_Text scoreText;
    public GameObject[] stars = new GameObject[3];

    void Start()
    {
        
        scoreText.text = GameManager.Instance.gameResult;
        if (GameManager.Instance.stars < 2)
        {
            Button btn = GameObject.Find("PlayBtn").GetComponent<Button>();
            btn.gameObject.SetActive(false);
        }

       

        for (int i = 0; i < GameManager.Instance.stars; i++) 
        {
            stars[i].SetActive(true);
        }

        int lastLevelIndex = GameManager.Instance.lastLevelBuildIndex;
        int nextLevelIndex = lastLevelIndex;
        if (nextLevelIndex == SceneManager.sceneCountInBuildSettings)
        {
            Button btn = GameObject.Find("PlayBtn").GetComponent<Button>();
            btn.gameObject.SetActive(false);
            scoreText.text = "You WON all of it !!!";
        }
        
    }


    public void LoadNextLevel()
    {
        int lastLevelIndex = GameManager.Instance.lastLevelBuildIndex+1;
        int nextLevelIndex = lastLevelIndex;
        
        if (nextLevelIndex < SceneManager.sceneCountInBuildSettings)
        {
            string nextLevelName = "Level " + (nextLevelIndex);
            SceneManager.LoadScene(nextLevelName);            
        }            
        
    }

    public void Replay()
    {
        int lastLevelIndex = GameManager.Instance.lastLevelBuildIndex;
        int nextLevelIndex = lastLevelIndex;        
        if (nextLevelIndex < SceneManager.sceneCountInBuildSettings)
        {
            string nextLevelName = "Level " + (nextLevelIndex);
            SceneManager.LoadScene(nextLevelName);
        }

    }


    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }
}
