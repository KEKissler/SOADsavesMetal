using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaticData: MonoBehaviour
{
    static public string playerSelected = "";
    static public string levelSelect = "";
    static public bool firstLoad = true;
    static public bool[] characterUnlocks = {false, false, false};
    static public bool[] shavoUnlock = {false, false};
    static public bool[] daronUnlock = {false, false};
    static public bool[] serjUnlock = {false, false, false};

    [SerializeField] bool changeCharacter = true;

    public void setPlayer(string name)
    {
        playerSelected = name;
    }

    public void setLevel(string levelName)
    {
        levelSelect = levelName;
    }

    public void printName(string name) //debuging
    {
        Debug.Log(name);
    }

    public void loadLevel() //loads the selected level
    {
        SceneManager.LoadScene(levelSelect);
    }

    public void loadLevel(string sceneName) //loads a level by name
    {
        SceneManager.LoadScene(sceneName);
    }

    public void reloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SaveGame() //saves current static data
    {
        SaveSystem.SaveGame();
    }

    public void LoadGame() //loads saved data onto static data
    {
        SaveData data = SaveSystem.LoadGame();
        firstLoad = data.firstLoad;
        characterUnlocks = data.characterUnlocks;
        shavoUnlock = data.shavoUnlock;
        daronUnlock = data.daronUnlock;
        serjUnlock = data.serjUnlock;
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("quit game");
    }

    void Awake()
    {
        Debug.Log("I RUN");
        if(changeCharacter)
        {
            Player playerScript = FindObjectOfType<Player>(); //gets Player component
            if(playerScript != null)
            {
                playerScript.currentBandMember = playerSelected; // sets player to selected player
            }
        }
    }
}