using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject OptionPanel;
    [SerializeField]
    GameObject LevelPanel;

    public void PlayGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void ShowOptions()
    {
        OptionPanel.SetActive(true);
    }
    public void HideOptions()
    {
        OptionPanel.SetActive(false);
    }

    public void ShowLevels()
    {
        LevelPanel.SetActive(true);
    }
    public void HideLevels()
    {
        LevelPanel.SetActive(false);
    }

    public void LoadLevel(Button button)
    {
        TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
        string LevelName = buttonText.text;
        SceneManager.LoadScene(LevelName);
    }

    public void Exit()
    {
        Application.Quit();
    }

}
