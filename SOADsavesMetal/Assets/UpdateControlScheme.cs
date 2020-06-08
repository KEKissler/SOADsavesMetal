using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateControlScheme : MonoBehaviour
{
    //Player Control Components
    public ControlSchemes controlSchemes;
    private KeyCode up;
    private KeyCode down;
    private KeyCode CAttack;
    private KeyCode RAttack;
    private KeyCode SAttack;
    private KeyCode pause;
    private string hori;
    private string vert;

    public void updateControlScheme()
    {
        StartCoroutine(ControllerUpdatePause());
    }

    public IEnumerator ControllerUpdatePause()
    {
        yield return new WaitForEndOfFrame();
        up = controlSchemes.up;
        yield return new WaitForEndOfFrame();
        down = controlSchemes.down;
        yield return new WaitForEndOfFrame();
        hori = controlSchemes.hori;
        yield return new WaitForEndOfFrame();
        pause = controlSchemes.pause;
        yield return new WaitForEndOfFrame();
        vert = controlSchemes.vert;
        yield return new WaitForEndOfFrame();
        CAttack = controlSchemes.CAttack;
        yield return new WaitForEndOfFrame();
        RAttack = controlSchemes.RAttack;
        yield return new WaitForEndOfFrame();
        SAttack = controlSchemes.SAttack;
        yield return new WaitForEndOfFrame();
    }
}
