using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class PauseMenu : MonoBehaviour
{
    public string pauseMenuSceneName = "PauseMenu";
    private bool isPaused = false;

    private void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.UnloadSceneAsync(pauseMenuSceneName);
        Cursor.visible = false;
    }

    void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
        SceneManager.LoadScene(pauseMenuSceneName, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(pauseMenuSceneName));
        Cursor.visible = true;
    }
}
