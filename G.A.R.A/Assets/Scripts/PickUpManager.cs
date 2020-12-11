using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpManager : MonoBehaviour
{
    private static Queue<GameObject> queue;
    private static int maxPickUps;

    // Start is called before the first frame update
    void Start()
    {
        queue = new Queue<GameObject>();
        maxPickUps = 40;
    }

    public static void AddPickUp(GameObject go)
    {
        if(queue.Count < maxPickUps)
        {
            queue.Enqueue(go);
        }
        else
        {
            GameObject temp = queue.Dequeue();
            Destroy(temp);
            queue.Enqueue(go);
        }
    }
}
