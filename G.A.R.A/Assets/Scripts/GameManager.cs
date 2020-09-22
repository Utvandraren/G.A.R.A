using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameObject[] systemPrefabs;

    private string currentLevel;
    private List<AsyncOperation> loadOperations;
    private List<GameObject> instancedSystemPrefabs;
    

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        loadOperations = new List<AsyncOperation>();
        instancedSystemPrefabs = new List<GameObject>();

        InstantiateSystemPrefabs();

        //LoadLevel("Start");
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
            //transition between scenes here
        }
        Debug.Log("Load complete");
    }

    void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unload complete");
    }


    public void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);

        if(ao == null)
        {
            Debug.LogError("[Gamemanager] unable to load level " + levelName);
            return;
        }

        ao.completed += OnLoadOperationComplete;
        loadOperations.Add(ao);
        currentLevel = levelName;
    }

    public void UnloadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);

        if (ao == null)
        {
            Debug.LogError("[Gamemanager] unable to unload level " + levelName);
            return;
        }

        ao.completed += OnUnloadOperationComplete;
    }

    public void GameOver()
    {
        //UIManager.setLoseUI;
    }

    public void ResetGame()
    {
        //LoadLevel("MainMeny");
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
