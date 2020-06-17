using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuButtons : MonoBehaviour {

    public void Quit() {
        Application.Quit();
    }

    public void LoadScene(string levelName) {
        SceneManager.LoadScene(levelName);
    }

    public void NewGame() {
        StaticData.firstLoad = true;
        StaticData.firstPlay = true;
        //StaticData.characterUnlocks = new bool[] {false, false, false};
        StaticData.shavoUnlock = false;
        StaticData.daronUnlock = false;
        StaticData.serjUnlock = false;
        SaveSystem.SaveGame();
    }

    public void Continue() {
        SaveSystem.LoadGame();
    }
}
