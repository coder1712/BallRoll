using UnityEngine;
using UnityEngine.SceneManagement;
public class StartMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Lvl_1");
    }

    public void RestartLvl1()
    {
        SceneManager.LoadScene("Lvl_1");
    }

    public void Restart()
    {
        SceneManager.SetActiveScene(SceneManager.GetActiveScene());
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
