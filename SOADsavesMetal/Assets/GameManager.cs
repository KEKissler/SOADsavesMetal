using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    void Awake()
    {
        SaveSystem.LoadGame();
        if (manager == null)
        {
            DontDestroyOnLoad(gameObject);
            manager = this;        
        }

        else if (manager != this)
        {
            Destroy(gameObject);
        }
    }
}
