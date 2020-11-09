using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPortal : MonoBehaviour
{
    [SerializeField] private string nextSceneForTesting; //Only in editor

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.GoToNextLevel(nextSceneForTesting);
        }
    }
}
