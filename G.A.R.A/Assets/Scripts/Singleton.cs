using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
   private static T instance;

    public static T Instance
    {
        get { return instance; }
    }

    public static bool isInitialized
    {
        get { return instance != null; }
    }

    protected virtual void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Triyng to instantiate a second instance of a singelton class");
        }
        else
        {
            instance = (T)this;
        }
    }

    protected virtual void OnDestroy()
    {
        if(instance == this)
        {
            instance = null;
        }
    }
}