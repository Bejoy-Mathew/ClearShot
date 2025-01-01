using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject OptionPanel;
    
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
}
