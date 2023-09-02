
using UnityEngine.SceneManagement;

public class SceneLoader {
    public static void ReloadScene() {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(sceneIndex);
    }


    public static void QuitGame() {
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
}
