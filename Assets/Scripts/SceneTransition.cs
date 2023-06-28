using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    int sceneIndex;
    int nextSceneIndex;
    Scene scene;

    public void Start()
    {
        scene = SceneManager.GetActiveScene();
        sceneIndex = scene.buildIndex;
        nextSceneIndex = sceneIndex + 1;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("SceneTransition"))
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

}
