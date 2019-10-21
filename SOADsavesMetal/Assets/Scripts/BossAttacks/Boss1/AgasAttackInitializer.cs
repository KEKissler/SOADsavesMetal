using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgasAttackInitializer : MonoBehaviour
{
    public AgasAttackData AttackData;

    public List<AgasAttack> AttacksToInitialize = new List<AgasAttack>();

    private void Start()
    {
        foreach(var attack in AttacksToInitialize)
        {
            attack.Initialize(AttackData);
        }
    }
}
