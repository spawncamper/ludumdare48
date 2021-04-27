using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    #region EVENTS
    [SerializeField] GameEvent LoadDescentLevelEvent;
    [SerializeField] GameEvent LoadExplorationLevelEvent;
    [SerializeField] GameEvent LoadMainMenuEvent;

    #endregion

    #region VARIABLES
    // Location: on the BOOT object in the boot scene
    [SerializeField] string bootScene;
    [SerializeField] string mainMenu;
    [SerializeField] string exploration;
    [SerializeField] string descent;
    string levelSelection = "Exploration";
    [SerializeField] float delay = 1f;
    #endregion

    Scene[] loadedScenes;

    public static SceneLoader Instance;

    #region ON BOOT
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
    }

    private void Start()
    {
        LoadLevelAsync("MainMenu");
        Debug.Log("[SceneLoader] OpenMainMenuFromBoot ()");
    }

    #endregion

    private void Update()
    {
        // Exit game from Main menu on application quit
        if (Input.GetKeyDown("escape") && SceneManager.GetActiveScene().buildIndex == 1)
        {
            // other application quit stuff

            print("[SceneLoader] Update() Application.Quit()");

            Application.Quit();
        }
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
        StopAllCoroutines();
        
        Time.timeScale = 1;

        print("[Sceneloader] RestartLevel point 1");

        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);

        print("[Sceneloader] RestartLevel point 2");
    }

    public void StartGame()
    {
        SceneManager.LoadScene(levelSelection);

        if (levelSelection == exploration)
        {
            LoadExplorationLevelEvent.Raise();
        }
        else if (levelSelection == descent)
        {
            LoadDescentLevelEvent.Raise();
        }
    }

    public void LoadMainMenu()
    {
        print("[SceneLoader] LoadMainMenu()");
        SceneManager.LoadScene(mainMenu);
    }

    public void MainMenuExplorationSelection()
    {
        levelSelection = exploration;
    }

    public void MainMenuDescentSelection()
    {
        levelSelection = descent;
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