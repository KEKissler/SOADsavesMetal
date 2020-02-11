using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NhangAttackInitializer : MonoBehaviour
{
    public NhangAttackData AttackData;

    public List<NhangAttack> AttacksToInitialize = new List<NhangAttack>();

    private void Start()
    {
        foreach (var attack in AttacksToInitialize)
        {
            attack.Initialize(AttackData);
        }
    }
}
