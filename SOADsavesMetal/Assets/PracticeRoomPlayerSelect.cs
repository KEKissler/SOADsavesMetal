using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


/* This script is meant for use with the player in the PRACTICE ROOM ONLY.
 * The player can use just the keyboard or a controller to select a new character to practice with.
 */

public class PracticeRoomPlayerSelect : MonoBehaviour
{
    //Map of the characters in the practice room
    private Dictionary<int, string> characterMap = new Dictionary<int, string>();
    
    //Map to see if a character is unlocked
    private Dictionary<int, bool> unlockMap = new Dictionary<int, bool>();
    
    //Holds the new int and the current int respectively  
    private int charInt, currInt;

    //Holds left and right bumper keycodes for a controller
    private KeyCode cycleLeft = KeyCode.JoystickButton4 , cycleRight = KeyCode.JoystickButton5;

    public Player currentCharacter;

    // Start is called before the first frame update
    void Start()
    {
        currentCharacter = FindObjectOfType<Player>();
        charInt = currInt = 1;
        

        //Mapping characters to numbers for easier alteration
        characterMap.Add(1, "John");
        characterMap.Add(2, "Shavo");
        characterMap.Add(3, "Daron");
        characterMap.Add(4, "Serj");

        //Mapping unlocks
        unlockMap.Add(1, true);
        unlockMap.Add(2, StaticData.shavoUnlock);
        unlockMap.Add(3, StaticData.daronUnlock);
        unlockMap.Add(4, StaticData.serjUnlock);
    }

    // Update is called once per frame
    void Update()
    {
        //Creates a variable checking the active band members name
        var playerID = currentCharacter.currentBandMember;


        #region Character change using numeric keys
        if (Input.GetKeyDown("1") && playerID != "John")
        {
            charInt = 1;
        }
        if(Input.GetKeyDown("2") && playerID != "Shavo" && StaticData.shavoUnlock)
        {
            charInt = 2;
        }
        if(Input.GetKeyDown("3") && playerID != "Daron" && StaticData.daronUnlock)
        {
            charInt = 3;
        }
        if(Input.GetKeyDown("4") && playerID != "Serj" && StaticData.serjUnlock)
        {
            charInt = 4;
        }
        #endregion

        #region Character change using Left and Right Bumpers
        if (Input.GetKeyDown(cycleLeft))
        {
            do
            {
                if (charInt > 1)
                {
                    --charInt;
                }

                else
                    charInt = 4;
            }
            while (!unlockMap[charInt]);
        }

        if (Input.GetKeyDown(cycleRight))
        {
            if (charInt < 4)
                ++charInt;
            else
                charInt = 1;
        }
        #endregion

        if (charInt != currInt)
        {
            //Set the current character
            currInt = charInt;
            SetPlayerID(charInt);
        }
    }

    private void SetPlayerID(int key)
    {
        currentCharacter.SetCurrentBandMember(characterMap[key]);
    }

    


}
