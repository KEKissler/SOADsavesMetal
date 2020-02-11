using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialMarker : MonoBehaviour
{
    public bool touched = false;

    void Awake()
    {
        touched = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            touched = true;
        }
    }
}
