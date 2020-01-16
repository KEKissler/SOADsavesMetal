using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveScript: MonoBehaviour
{
    static public string playerSelected = "";
    static public int levelSelect = 0;

    public void setPlayer(string name)
    {
        playerSelected = name;
    }

    public void setLevel(int level)
    {
        levelSelect = level;
    }

    public void printName(string name)
    {
        print(name);
    }

    public void loadLevel() //loads the selected level
    {
        if(levelSelect != 0)
        {
            SceneManager.LoadScene(levelSelect);
        }
        else
        {
            print("No level selected!");
        }
    }

    void Awake()
    {
        GameObject playerObject = GameObject.Find("Player"); //find an obejct in the scene named player
        if(playerObject != null)
        {
            Player playerScript = playerObject.GetComponent<Player>(); //gets Player component
            if(playerScript != null)
            {
                playerScript.currentBandMember = playerSelected; // sets player to selected player
            }
        }
    }
}