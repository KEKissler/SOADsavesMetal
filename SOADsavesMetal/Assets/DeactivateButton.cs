using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeactivateButton : MonoBehaviour
{
    public Button continueButton;
    //GameObject continueButton;

    // Start is called before the first frame update
    void Start()
    {
        SaveSystem.LoadGame();
        if (StaticData.firstLoad)
        {
            continueButton.interactable = false;
        }
        else
            continueButton.interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if(!buttonActive())
            
    }

    private bool buttonActive()
    {
        return StaticData.firstLoad;
    }
}
