using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 0f;
        PauseMenu.GameIsPaused = true;
    }

    private void Awake()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 1f;
            PauseMenu.GameIsPaused = false;
            GameManager.Instance.RestartLevel();
        }
    }
}
