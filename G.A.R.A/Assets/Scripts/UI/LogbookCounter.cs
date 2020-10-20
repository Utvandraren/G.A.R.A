using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LogbookCounter : MonoBehaviour
{
    private GameObject[] logbooks;
    private int numberOfLogbooks;
    // Start is called before the first frame update
    void Start()
    {
        logbooks = GameObject.FindGameObjectsWithTag("Logbook");
        numberOfLogbooks = logbooks.Length;
        gameObject.GetComponent<Text>().text = numberOfLogbooks.ToString();
        Debug.Log(numberOfLogbooks);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FoundLogbook()
    {
        numberOfLogbooks--;
        gameObject.GetComponent<Text>().text = numberOfLogbooks.ToString();
        Debug.Log(numberOfLogbooks);
    }

    public int GetNumberOfLogbooks()
    {
        return numberOfLogbooks;
    }
}
