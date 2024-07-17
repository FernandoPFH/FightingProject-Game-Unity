using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontStopMusic : MonoBehaviour
{
    private static DontStopMusic instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        instance = this;
    }
}
