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

    [Header("Player super meter max charge (default 100)")]
    public float johnMaxCharge;
    public float shavoMaxCharge, daronMaxCharge, serjMaxCharge;

    private Player ps;
    public PlayerAttack pa;

    void Start()
    {
        StartCoroutine(SetPlayerName());
    }

    public IEnumerator SetPlayerName()
    {
        yield return new WaitForSeconds(0.1f);
        JohnAttack.SetActive(false);
        ShavoAttack.SetActive(false);
        DaronAttack.SetActive(false);
        SerjAttack.SetActive(false);

        ps = GameObject.FindWithTag("Player").GetComponent<Player>();

        string currentBandMember = transform.parent.gameObject.GetComponent<Player>().currentBandMember;

        switch (currentBandMember)
        {
            case "John":
                JohnAttack.SetActive(true);
                pa = JohnAttack.GetComponent<JohnAttack>();
                ps.maxSuperCharge = johnMaxCharge;
                break;
            case "Shavo":
                ShavoAttack.SetActive(true);
                pa = ShavoAttack.GetComponent<ShavoAttack>();
                ps.maxSuperCharge = shavoMaxCharge;
                break;
            case "Daron":
                DaronAttack.SetActive(true);
                pa = DaronAttack.GetComponent<DaronAttack>();
                ps.maxSuperCharge = daronMaxCharge;
                break;
            case "Serj":
                SerjAttack.SetActive(true);
                pa = SerjAttack.GetComponent<SerjAttack>();
                ps.maxSuperCharge = serjMaxCharge;
                break;
            default:
                JohnAttack.SetActive(true);
                pa = JohnAttack.GetComponent<JohnAttack>();
                ps.maxSuperCharge = johnMaxCharge;
                break;
        }
    }
}
