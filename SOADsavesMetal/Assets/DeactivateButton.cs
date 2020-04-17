using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeactivateButton : MonoBehaviour
{
    public Button button;


    private string buttonName;
    //GameObject button;

    // Start is called before the first frame update
    void Start()
    {
        button.interactable = false;
        SaveSystem.LoadGame();
        if (!allTrue(StaticData.shavoUnlock))
        {
            button.interactable = false;
        }
        else
        {
            buttonName = gameObject.name;
            Debug.Log(buttonName);

            switch (buttonName)
            {
                case "Shavo Button":
                    if (allTrue(StaticData.shavoUnlock))
                        button.interactable = true;
                    break;
                case "Daron  Button":
                    Debug.Log("Igothere");
                    if (allTrue(StaticData.daronUnlock))
                        button.interactable = true;
                    break;
                case "Serj  Button":
                    if (allTrue(StaticData.serjUnlock))
                        button.interactable = true;
                    break;
                case "Level 2":
                    if (allTrue(StaticData.shavoUnlock))
                        button.interactable = true;
                    break;
                case "Level 3":
                    if (allTrue(StaticData.daronUnlock))
                        button.interactable = true;
                    break;
                case "Level 4":
                    if (allTrue(StaticData.serjUnlock))
                        button.interactable = true;
                    break;
                default:
                    button.interactable = true;
                    break;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool allTrue(bool[] nameArray)
    {
        foreach(bool check in nameArray) if (!check) return false;
        return true;
    }
}
