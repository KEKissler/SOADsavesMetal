﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/Tsovinar/MultiAttack")]
public class TsovinarMultiAttack : TsovinarAttack
{
    public List<TsovinarAttack> Attacks;

    public override void Initialize(TsovinarAttackData data)
    {
        //assumes each attack in attacks is already initialized
    }

    protected override void OnStart()
    {
        foreach(var subAttack in Attacks)
        {
            if (duration < subAttack.duration) duration = subAttack.duration;
        }
    }

    protected override IEnumerator Execute(float duration)
    {
        yield return new WaitForEndOfFrame();
        foreach(var subAttack in Attacks)
        {
            subAttack.ExecuteAttack();
        }
    }

    protected override void OnEnd()
    {
    }
}
