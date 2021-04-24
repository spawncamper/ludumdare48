using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Location: on the BOOT object in the boot scene
    [SerializeField] string bootScene;
    [SerializeField] string mainMenu;
    [SerializeField] string gameScene;
    string activeScene;
    [SerializeField] float delay = 1f;

    Scene[] loadedScenes;

    public static SceneLoader Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.LogError("[SceneLoader] Second instance of GameManager detected and deleted");
        }

        DontDestroyOnLoad(this);

        StartCoroutine(OpenMainMenuFromBoot());
    }

    IEnumerator OpenMainMenuFromBoot()
    {
        LoadLevelAsync(mainMenu);
        Debug.Log("[SceneLoader] OpenMainMenuFromBoot ()");
        yield return new WaitForSeconds(delay);
    }

    public void LoadLevelAsync(string levelName)
    {
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);

        if (asyncOp == null)
        {
            Debug.LogError("[GameManager] Unable to load level " + levelName);

            return;
        }
    }

    public void UnloadLevelAsync(string levelName)
    {
        AsyncOperation asyncOp = SceneManager.UnloadSceneAsync(levelName);

        if (asyncOp == null)
        {
            Debug.LogError("[SceneLoader] Unable to unload scene " + levelName);

            return;
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public Scene[] GetOpenScenes()
    {
        int countLoaded = SceneManager.sceneCount;
        Scene[] loadedScenes = new Scene[countLoaded];

        for (int i = 0; i < countLoaded; i++)
        {
            loadedScenes[i] = SceneManager.GetSceneAt(i);
        }

        return loadedScenes;
    }
}