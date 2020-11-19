using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Spawner : MonoBehaviour
{
    public enum Type
    {
        ROOM,
        WALL
    }
    public Type type;
    public int index;
    public void Spawn(GameObject gameObject)
    {
        Instantiate(gameObject, transform.position + Random.insideUnitSphere, transform.rotation);
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 10);
        //Gizmos.DrawWireSphere(transform.position, 50);
    }
}
