using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TsovinarAttackInitializer : MonoBehaviour
{
    public TsovinarAttackData AttackData;

    public List<TsovinarAttack> AttacksToInitialize = new List<TsovinarAttack>();

    private void Start()
    {
        foreach(var attack in AttacksToInitialize)
        {
            attack.Initialize(AttackData);
        }
    }
}

