using UnityEngine;
using UnityEngine.SceneManagement;

public class ResumeScene : MonoBehaviour
{
    public void Resume()
    {
        Time.timeScale = 1f;
        SceneManager.UnloadSceneAsync(gameObject.scene);
        SceneManager.SetActiveScene(SceneManager.GetActiveScene());
        Cursor.visible = false;
    }
}
