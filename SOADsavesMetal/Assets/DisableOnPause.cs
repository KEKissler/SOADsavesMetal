using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableOnPause : MonoBehaviour
{
    public ControlSchemes control;
    public Button backButton; 

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(control.pause))
            backButton.interactable = !backButton.interactable;
    }
}
