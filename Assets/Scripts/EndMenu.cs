using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public GameObject menuPanel;
    public void RestartCurrentScene()
    {
        menuPanel.SetActive(false);
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);

    }

    public void GotoLevel1()
    {
        menuPanel.SetActive(false);
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene("SampleScene");
    }
}
