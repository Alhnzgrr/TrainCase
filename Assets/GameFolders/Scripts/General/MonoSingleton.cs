using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    static volatile T instance = null; // volatile -> Deðiþkenin deðerinin direkt bellekten alýnmasýný saðlar.

    public static T Instance
    {
        get
        {
            return instance;
        }
    }

    protected void Singleton(bool dontDestroyOnLoad = false)
    {
        if (dontDestroyOnLoad)
        {
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (instance == null)
            {
                instance = this as T;
            }
        }
    }
}