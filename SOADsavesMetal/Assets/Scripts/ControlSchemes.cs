using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class ControlSchemes: MonoBehaviour
{
    public KeyCode up = KeyCode.UpArrow;
    public KeyCode down = KeyCode.DownArrow;
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode right = KeyCode.RightArrow;
    public KeyCode jump = KeyCode.UpArrow;
    public KeyCode CAttack = KeyCode.Z;
    public KeyCode RAttack = KeyCode.X;
    public KeyCode SAttack = KeyCode.C;

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
                left = KeyCode.LeftArrow;
                right = KeyCode.RightArrow;
                jump = KeyCode.UpArrow;
                CAttack = KeyCode.Z;
                RAttack = KeyCode.X;
                SAttack = KeyCode.C;

                break;

            //WASD controls
            case 1:
                up = KeyCode.W;
                down = KeyCode.S;
                left = KeyCode.A;
                right = KeyCode.D;
                jump = KeyCode.W;
                CAttack = KeyCode.J;
                RAttack = KeyCode.K;
                SAttack = KeyCode.L;
                break;

            //Controller controls
            case 2:
                break;


            //Also Arrow keys
            default:
                up = KeyCode.UpArrow;
                down = KeyCode.DownArrow;
                left = KeyCode.LeftArrow;
                right = KeyCode.RightArrow;
                jump = KeyCode.UpArrow;
                CAttack = KeyCode.Z;
                RAttack = KeyCode.X;
                SAttack = KeyCode.C;
                break;
        }    
    }
}
