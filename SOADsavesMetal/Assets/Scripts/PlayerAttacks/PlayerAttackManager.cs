using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    // Figures out which attack script to enable
    public GameObject JohnAttack;
    public GameObject ShavoAttack;
    public GameObject DaronAttack;
    public GameObject SerjAttack;

    void Start()
    {
        JohnAttack.SetActive(false);
        ShavoAttack.SetActive(false);
        DaronAttack.SetActive(false);
        SerjAttack.SetActive(false);

        string currentBandMember = transform.parent.gameObject.GetComponent<Player>().currentBandMember;

        switch(currentBandMember)
        {
            case "John":
                JohnAttack.SetActive(true);
                break;
            case "Shavo":
                ShavoAttack.SetActive(true);
                break;
            case "Daron":
                DaronAttack.SetActive(true);
                break;
            case "Serj":
                SerjAttack.SetActive(true);
                break;
            default:
                JohnAttack.SetActive(true);
                break;
        }
    }
}
