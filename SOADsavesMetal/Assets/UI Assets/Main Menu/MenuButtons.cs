using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuButtons : MonoBehaviour {

    public void Quit()
    {
        Debug.Log("Closes the game");
        Application.Quit();
    }

    public void LoadScene(string levelName) {
        Debug.Log("Loads level specified");
        SceneManager.LoadScene(levelName);
    }

    public void NewGame() {

        Debug.Log("Clean Slate here");
    }
    //public GameObject UIPanel;
    public void ControlsPanel(GameObject UIPanel) {
        Debug.Log("Use this for controls page");
        UIPanel.SetActive(true);
    }
}
