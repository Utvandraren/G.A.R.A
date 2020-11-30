using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SubSpawner : MonoBehaviour
{
    public void Spawn(GameObject gameObject)
    {
        Instantiate(gameObject, transform.position + Random.insideUnitSphere, transform.rotation);
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 1);
        //Gizmos.DrawWireSphere(transform.position, 5);
    }
}
