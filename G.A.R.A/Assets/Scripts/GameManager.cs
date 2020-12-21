using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameObject[] systemPrefabs;

    [SerializeField] private string loadingScene;
    [SerializeField] private UIManager uiManager;

    private string currentLevel;
    private string previousLevel;
    private string nextLevel;
    private List<AsyncOperation> loadOperations;
    private List<GameObject> instancedSystemPrefabs;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        loadOperations = new List<AsyncOperation>();
        instancedSystemPrefabs = new List<GameObject>();

        InstantiateSystemPrefabs();

        LoadScene("MainMenu");
    }

    void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;
        for (int i = 0; i < systemPrefabs.Length; i++)
        {
            prefabInstance = Instantiate(systemPrefabs[i]);
            instancedSystemPrefabs.Add(prefabInstance);
        }
    }

    void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (loadOperations.Contains(ao))
        {
            loadOperations.Remove(ao);
            if (currentLevel == loadingScene)
            {
                UnloadScene(previousLevel);
            }
            else if (currentLevel != loadingScene && SceneManager.GetSceneByName(loadingScene).isLoaded)
            {
                UnloadScene(loadingScene);
            }
            //transition between scenes here
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentLevel)); //Required for SceneManager.GetActiveScene to work properly

        try
        {
            uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        }
        catch
        {
            Debug.Log("GameManager.cs could not find the UIManager. Was this intentional?");
        }

        Debug.Log("Load complete");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unload complete");

        if (currentLevel == loadingScene)
        {
            LoadScene(nextLevel);
        }
    }

    /// <summary>
    /// Loads a level additively on top of already loaded scenes.
    /// </summary>
    /// <param name="sceneName"></param>
    private void LoadScene(string sceneName)
    {
        previousLevel = currentLevel;
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        if(ao == null)
        {
            Debug.LogError("[GameManager] unable to load level: " + sceneName);
            return;
        }

        ao.completed += OnLoadOperationComplete;
        loadOperations.Add(ao);
        currentLevel = sceneName;
    }

    /// <summary>
    /// Unloads one of the scenes that are currently loaded.
    /// </summary>
    /// <param name="sceneName"></param>
    private void UnloadScene(string sceneName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(sceneName);

        if (ao == null)
        {
            Debug.LogError("[Gamemanager] unable to unload level " + sceneName);
            return;
        }

        ao.completed += OnUnloadOperationComplete;
    }

    public void GoToNextLevel(string testScene)
    {
        //ONLY FOR DEBUG IN EDITOR
        if (SceneManager.GetSceneByBuildIndex((SceneManager.GetActiveScene().buildIndex + 1)).name == null)
        {
            nextLevel = testScene;
            LoadScene(loadingScene);
        }
        else //This is for builds
        {
            nextLevel = SceneManager.GetSceneByBuildIndex((SceneManager.GetActiveScene().buildIndex + 1)).name;
            LoadScene(loadingScene);
        }
    }

    public void RestartLevel()
    {
        nextLevel = SceneManager.GetActiveScene().name;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().ResetStats();
        LoadScene(loadingScene);
    }

    public void GameOver()
    {
        uiManager.SetLoseUI();
    }

    public void ReturnToMain()
    {
        nextLevel = "MainMenu";
        LoadScene(loadingScene);
    }

    public void Win()
    {
        //Load timelineendingscene and credit
        Debug.Log("Game won!!!");
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        for (int i = 0; i < instancedSystemPrefabs.Count; i++)
        {
            Destroy(instancedSystemPrefabs[i]);
        }

        instancedSystemPrefabs.Clear();
    }
}
