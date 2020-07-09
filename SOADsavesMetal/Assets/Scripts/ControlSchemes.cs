using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class ControlSchemes: MonoBehaviour
{
    public KeyCode up;
    public KeyCode down;
    public string hori;
    public string vert;
    public KeyCode CAttack;
    public KeyCode RAttack;
    public KeyCode SAttack;
    public KeyCode pause;

    public int scheme;
    
    private void Awake()
    {
        scheme = StaticData.controlScheme;
    }
    void Update()
    {
        //Assisgns the current scheme that is being held in saveData

        do 
        {
            scheme = StaticData.controlScheme;

            switch (scheme)
            {
                //Arrow controls
                case 0:
                    up = KeyCode.UpArrow;
                    down = KeyCode.DownArrow;
                    hori = "Horizontal";
                    vert = "Vertical";
                    CAttack = KeyCode.Z;
                    RAttack = KeyCode.X;
                    SAttack = KeyCode.C;
                    pause = KeyCode.Escape;
                    break;

                //WASD controls
                case 1:
                    up = KeyCode.W;
                    down = KeyCode.S;
                    hori = "WASDHori";
                    vert = "WASDVert";
                    CAttack = KeyCode.J;
                    RAttack = KeyCode.K;
                    SAttack = KeyCode.L;
                    pause = KeyCode.Escape;
                    break;

                //Controller controls
                case 2:
                    up = KeyCode.JoystickButton0;
                    down = KeyCode.S;
                    hori = "ContHori";
                    vert = "ContVert";
                    CAttack = KeyCode.JoystickButton1;
                    RAttack = KeyCode.JoystickButton2;
                    SAttack = KeyCode.JoystickButton3;
                    pause = KeyCode.JoystickButton7;
                    break;
            }
            //StartCoroutine(ControllerUpdatePause());

        } while (!scheme.Equals(StaticData.controlScheme));
    }

    public void updateControlScheme()
    {
        StartCoroutine(ControllerUpdatePause());
    }

    public IEnumerator ControllerUpdatePause()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        yield return new WaitForEndOfFrame();
        if (StaticData.firstLoad)
            StaticData.firstLoad = false;
        SaveSystem.SaveGame();

    }
}
