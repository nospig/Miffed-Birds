using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
