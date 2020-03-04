using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuButtons : MonoBehaviour {

    public void Quit() {
        Debug.Log("Closes the game");
        Application.Quit();
    }

    public void LoadScene(string levelName) {
        Debug.Log("Loads level specified");
        SceneManager.LoadScene(levelName);
    }

    public void NewGame() {
    StaticData.firstLoad = true;
    StaticData.characterUnlocks = new bool[] {false, false, false};
    StaticData.shavoUnlock = new bool[] {false, false};
    StaticData.daronUnlock = new bool[] {false, false};
    StaticData.serjUnlock = new bool[] {false, false, false};
    SaveSystem.SaveGame();
    }

    public void Continue() {
    SaveSystem.LoadGame();
    }
}
