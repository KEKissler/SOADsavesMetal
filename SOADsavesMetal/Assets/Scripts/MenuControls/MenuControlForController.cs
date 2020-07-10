using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/* This script is for controller controls on menu buttons
 * The intention is to attach this script to buttons for controller support
 */

public class MenuControlForController : MonoBehaviour
{
    private static KeyCode BACK = KeyCode.JoystickButton1, CONFIRM = KeyCode.JoystickButton0, LBUMP = KeyCode.JoystickButton4, RBUMP = KeyCode.JoystickButton5;
    private static string HORI = "DPadHori", VERT = "DPadVert";
    private static int PAUSECOUNT = 6, CONTROLSCOUNT = 4, DEATHCOUNT = 3, AUDIOCOUNT = 3;

    public AudioMenuData AMD; 
    public DeathMenuData DMD; 
    public MainMenuData MMD; 
    public PauseMenuData PMD; 
    public ControlMenuData CMD; 
    public StageSelectData SSD;

    private Dictionary<int, Button> menuButtonMap = new Dictionary<int, Button>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AssignButtons(int count)
    {
        for(int i = 0; i < count; ++i)
        { }
    }

    private void ClearButtons()
    {
        for (int i = 0; i < menuButtonMap.Count; ++i)
            menuButtonMap.Remove(i);
    }
}
