using UnityEngine;
using UnityEngine.SceneManagement;
public class StartMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Lvl_1");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Lvl_1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
