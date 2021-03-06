﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/Agas/MultiAttack")]
public class AgasMultiAttack : AgasAttack
{
    public List<AgasAttack> Attacks;

    private readonly List<float> originalDurations = new List<float>();

    public override void Initialize(AgasAttackData data)
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
        Debug.Log(duration);
        yield return new WaitForEndOfFrame();
        foreach(var subAttack in Attacks)
        {
            subAttack.ExecuteAttack();
        }
    }

    protected override void OnEnd()
    {
        for (int i = 0; i < Attacks.Count; ++i)
        {
            Attacks[i].duration = originalDurations[i];
        }
    }
}
