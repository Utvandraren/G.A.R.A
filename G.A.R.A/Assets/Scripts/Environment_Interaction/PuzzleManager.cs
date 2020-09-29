using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : Singleton<PuzzleManager>
{
    private Queue<GameObject> deletedObjects;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        deletedObjects = new Queue<GameObject>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log(deletedObjects.Count);
            GameObject temp = deletedObjects.Dequeue();
            temp.SetActive(true);
            if(temp.TryGetComponent<DestroyInteraction>(out DestroyInteraction di))
            {
                di.ResetObject();
            }
        }
    }

    public void AddDeletedObjectToQueue(GameObject gameObject)
    {
        deletedObjects.Enqueue(gameObject);
        Debug.Log(deletedObjects.Count);
    }
}
