using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] private GameObject pauseMenuObject;

    [SerializeField] private Options options;

    private bool isClosing;

    private void Start()
    {
        GameIsPaused = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Pause game"))
        {
            if(GameIsPaused)
            {
                if(options.isOpen)
                {
                    CloseOptions();
                    isClosing = true;
                }
                else Resume();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Pause();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        if(!options.isOpen && isClosing)
        {
            Resume();
        }
    }

    public void BackToMain()
    {
        Resume();
        GameManager.Instance.ReturnToMain();
    }

    public void ShowOptions()
    {
        options.OpenOptions();
    }

    private void CloseOptions()
    {
        options.CloseOptions();
    }

    public void Quit()
    {
        Debug.Log("Why are you quitting!?");
        Application.Quit();
    }

    public void Resume()
    {
        pauseMenuObject.SetActive(false);
        //Insert animation later?
        Time.timeScale = 1f;
        GameIsPaused = false;
        isClosing = false;
    }

    private void Pause()
    {
        pauseMenuObject.SetActive(true);
        //Insert animation later?
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
