using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaticData: MonoBehaviour
{
    public AnimationClip FadeOutAnim;
    public AnimationClip FadeInAnim;

    static public string playerSelected = "";
    static public string levelSelect = "";
    static public int controlScheme = 0;
    static public SpriteRenderer selectedCharacter;
    static public bool firstLoad = true;
    static public bool firstPlay = true;
    //static public bool[] characterUnlocks = {false, false, false};
    static public bool shavoUnlock = false;
    static public bool daronUnlock = false;
    static public bool serjUnlock = false;
    

    [SerializeField] bool changeCharacter = true;

    public void setPlayer(string name)
    {
        playerSelected = name;
    }

    public void setLevel(string levelName)
    {
        levelSelect = levelName;
    }

    public void setSprite(SpriteRenderer characterSprite)
    {
        selectedCharacter = characterSprite;
    }

    public void printName(string name) //debuging
    {
        Debug.Log(name);
    }

    public void loadLevel() //loads the selected level
    {
        StartCoroutine(loadLevelFade());
        /*SceneManager.LoadScene(levelSelect);*/
    }

    IEnumerator loadLevelFade()
    {
        if (!SceneManager.GetActiveScene().Equals("Main Menu") && !levelSelect.Equals("Main Menu"))
        {
            yield return new WaitForSeconds(FadeOutAnim.length);

        }
        SceneManager.LoadScene(levelSelect);
    }

    IEnumerator loadLevelFade(string sceneName)
    {
        if (!SceneManager.GetActiveScene().Equals("Main Menu") && !sceneName.Equals("Main Menu"))
        {
            yield return new WaitForSeconds(FadeOutAnim.length);

        }
        SceneManager.LoadScene(sceneName);
    }

    public void loadLevel(string sceneName) //loads a level by name
    {
        StartCoroutine(loadLevelFade(sceneName));
    }

    public void reloadLevel()
    {
        SaveGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public int getControlScheme()
    {
        return controlScheme;
    }

    // 0 is arrows, 1 is WASD, 2 is controller
    public void setControlScheme(int newControlScheme)
    {
        controlScheme = newControlScheme;
    }

    public void SaveGame() //saves current static data
    {
        SaveSystem.SaveGame();
        waitForSave();
    }

    public void LoadGame() //loads saved data onto static data
    {
        SaveData data = SaveSystem.LoadGame();
        firstLoad = data.firstLoad;
        firstPlay = data.firstPlay;
        //characterUnlocks = data.characterUnlocks;
        shavoUnlock = data.shavoUnlock;
        daronUnlock = data.daronUnlock;
        serjUnlock = data.serjUnlock;
    }

    public void Quit()
    {
        SaveGame();

        Application.Quit();
        Debug.Log("quit game");
    }

    IEnumerator waitForSave()
    {
        yield return new WaitForSeconds(3f);
    }

    void Awake()
    {
        if(changeCharacter)
        {
            Player playerScript = FindObjectOfType<Player>(); //gets Player component
            //SpriteRenderer playerSprite = FindObjectOfType<SpriteRenderer>();
            if(playerScript != null)
            {
                playerScript.currentBandMember = playerSelected; // sets player to selected player
                //playerSprite = selectedCharacter;
                //Debug.Log(playerSprite);
            }
        }
    }
}