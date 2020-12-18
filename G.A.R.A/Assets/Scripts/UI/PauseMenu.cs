using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] private GameObject pauseMenuObject;

    [SerializeField] private Options options;
    [SerializeField] private GameObject controlsImage;

    //Used to play sounds of menues that close themselves, so they cannot play any sounds when deactivating in hierarchy.
    private AudioSource audio;

    private bool isClosing;

    private void Start()
    {
        GameIsPaused = false;
        audio = GetComponent<AudioSource>();
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
            }
            else
            {
                Pause();
            }
        }

        if(!options.isOpen && isClosing)
        {
            Resume();
        }

        if(options.isOpen && !GameIsPaused)
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

    public void ShowControls()
    {
        controlsImage.SetActive(true);
    }

    public void CloseControls()
    {
        audio.Play();
        controlsImage.SetActive(false);
    }

    public void Quit()
    {
        Debug.Log("Why are you quitting!?");
        Application.Quit();
    }

    public void Resume()
    {
        audio.Play();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuObject.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        isClosing = false;
        MusicManager.Instance.ResetSnapshot();
    }

    private void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenuObject.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        MusicManager.Instance.TransitionToSoftSnapshot();
    }
}
