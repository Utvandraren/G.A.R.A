using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticLevelTransition : MonoBehaviour
{

    [SerializeField] private string LevelToGoTo;

    public void LoadScene()
    {
        //Debug.Log("new scene loaded!!!!");
        GameManager.Instance.GoToNextLevel(LevelToGoTo);
    }

    
    void OnEnable()
    {
        LoadScene();
    }
}
