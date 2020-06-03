using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class ControlSchemes: MonoBehaviour
{
    public KeyCode up = KeyCode.UpArrow;
    public KeyCode down = KeyCode.DownArrow;
    public string hori = "Horizontal";
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode right = KeyCode.RightArrow;
    public KeyCode jump = KeyCode.UpArrow;
    public KeyCode CAttack = KeyCode.Z;
    public KeyCode RAttack = KeyCode.X;
    public KeyCode SAttack = KeyCode.C;
    public KeyCode pause = KeyCode.Escape;

    public int scheme;
    
    private void Start()
    {
        scheme = StaticData.controlScheme;
    }
    void Update()
    {
        //Assisgns the current scheme that is being held in saveData
        scheme = StaticData.controlScheme;

        switch(scheme)
        {
            //Arrow controls
            case 0:
                up = KeyCode.UpArrow;
                down = KeyCode.DownArrow;
                hori = "Horizontal";
                left = KeyCode.LeftArrow;
                right = KeyCode.RightArrow;
                jump = KeyCode.UpArrow;
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
                left = KeyCode.A;
                right = KeyCode.D;
                jump = KeyCode.W;
                CAttack = KeyCode.J;
                RAttack = KeyCode.K;
                SAttack = KeyCode.L;
                pause = KeyCode.Escape;
                break;

            //Controller controls
            case 2:
                up = KeyCode.W;
                down = KeyCode.S;
                hori = "ContHori";
                left = KeyCode.A;
                right = KeyCode.D;
                jump = KeyCode.JoystickButton0;
                CAttack = KeyCode.JoystickButton1;
                RAttack = KeyCode.JoystickButton2;
                SAttack = KeyCode.JoystickButton3;
                pause = KeyCode.JoystickButton7;
                break;


            //Also Arrow keys
            default:
                up = KeyCode.UpArrow;
                down = KeyCode.DownArrow;
                hori = "Horizontal";
                left = KeyCode.LeftArrow;
                right = KeyCode.RightArrow;
                jump = KeyCode.UpArrow;
                CAttack = KeyCode.Z;
                RAttack = KeyCode.X;
                SAttack = KeyCode.C;
                pause = KeyCode.Escape;
                break;
        }    
    }
}
