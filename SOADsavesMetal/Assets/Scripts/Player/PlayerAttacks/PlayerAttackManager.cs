using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    // Figures out which attack script to enable

    [Header("Player-specific Attack GameObjects")]
    public GameObject JohnAttack;
    public GameObject ShavoAttack;
    public GameObject DaronAttack;
    public GameObject SerjAttack;

    public PlayerAttack pa;

    private void Awake()
    {
        JohnAttack.SetActive(false);
        ShavoAttack.SetActive(false);
        DaronAttack.SetActive(false);
        SerjAttack.SetActive(false);

        string currentBandMember = transform.parent.gameObject.GetComponent<Player>().currentBandMember;

        switch (currentBandMember)
        {
            case "John":
                JohnAttack.SetActive(true);
                pa = JohnAttack.GetComponent<JohnAttack>();
                break;
            case "Shavo":
                ShavoAttack.SetActive(true);
                pa = ShavoAttack.GetComponent<ShavoAttack>();
                break;
            case "Daron":
                DaronAttack.SetActive(true);
                pa = DaronAttack.GetComponent<DaronAttack>();
                break;
            case "Serj":
                SerjAttack.SetActive(true);
                pa = SerjAttack.GetComponent<SerjAttack>();
                break;
            default:
                JohnAttack.SetActive(true);
                pa = JohnAttack.GetComponent<JohnAttack>();
                break;
        }
    }

    void Start()
    {
        
    }
}
