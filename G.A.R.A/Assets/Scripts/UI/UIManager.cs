using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject loseUI;

    void Start()
    {
        Cursor.visible = false;
    }
    public void SetLoseUI()
    {
        loseUI.gameObject.SetActive(true);
    }
}
