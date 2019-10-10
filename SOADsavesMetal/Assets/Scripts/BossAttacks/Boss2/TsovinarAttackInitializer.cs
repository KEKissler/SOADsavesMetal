using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TsovinarAttackInitializer : MonoBehaviour
{
    public TsovinarAttackData attackData;

    public List<TsovinarAttack> TsovinarAttacksToInitialize = new List<TsovinarAttack>();
    private void Start()
    {
        foreach(var attack in TsovinarAttacksToInitialize)
        {
            attack.Initialize(attackData);
        }
    }
}

