using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LogbookCounter : MonoBehaviour
{
    private GameObject[] logbooks;
    private int numberOfLogbooks;
    [SerializeField] private Text logbooksText;
    // Start is called before the first frame update
    void Start()
    {
        logbooks = GameObject.FindGameObjectsWithTag("Logbook");
        numberOfLogbooks = logbooks.Length;
        logbooksText.text = numberOfLogbooks.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FoundLogbook()
    {
        numberOfLogbooks--;
        logbooksText.text = numberOfLogbooks.ToString();
    }

    public int GetNumberOfLogbooks()
    {
        return numberOfLogbooks;
    }
}
