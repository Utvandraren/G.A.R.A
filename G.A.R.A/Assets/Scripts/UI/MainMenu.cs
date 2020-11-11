using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator optionsAnimator;
    [SerializeField] private string preferredTestScene;

    public void Play()
    {
        GameManager.Instance.GoToNextLevel(preferredTestScene);
    }

    //Looking for the options? They're in a separate script called Options.

    public void Quit()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
