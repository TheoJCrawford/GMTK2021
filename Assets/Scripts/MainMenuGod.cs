using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuGod : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }
    public void ReturnMain()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
