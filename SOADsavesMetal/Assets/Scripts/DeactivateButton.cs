using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeactivateButton : MonoBehaviour
{
    public Button button;
    private string buttonName;
    private GameplayPause gameplayPause;
    //GameObject button;

    // Start is called before the first frame update
    void Start()
    {
        gameplayPause = FindObjectOfType<GameplayPause>();
        button.interactable = false;
        SaveSystem.LoadGame();
        if (StaticData.firstLoad.Equals(true))
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
                case "Song 2":
                    if (allTrue(StaticData.shavoUnlock))
                        button.interactable = true;
                    break;
                case "Song 3":
                    if (allTrue(StaticData.daronUnlock))
                        button.interactable = true;
                    break;
                case "Song 4":
                    if (allTrue(StaticData.serjUnlock))
                        button.interactable = true;
                    break;
                case "End_Scene":
                    if (!StaticData.firstPlay)
                        button.interactable = true;
                    break;
                default:
                    button.interactable = true;
                    break;
            }
        }

    }

    private bool allTrue(bool[] nameArray)
    {
        foreach(bool check in nameArray) if (!check) return false;
        return true;
    }
}
