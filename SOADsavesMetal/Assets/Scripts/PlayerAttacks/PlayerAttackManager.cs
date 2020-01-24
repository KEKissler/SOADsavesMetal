using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    public GameObject JohnAttack;
    public GameObject ShavoAttack;
    public GameObject DaronAttack;
    public GameObject SerjAttack;

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
