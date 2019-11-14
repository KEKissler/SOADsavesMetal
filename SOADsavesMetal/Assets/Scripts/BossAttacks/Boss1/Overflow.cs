using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/Agas/Overflow")]

public class Overflow : AgasAttack
{
    public float floodTime;

    private DecayOverflow overflowLiquid;

    public override void Initialize(AgasAttackData data)
    {
        overflowLiquid = data.DecayOverflow;
    }

    protected override IEnumerator Execute(float duration)
    {
        yield return new WaitForEndOfFrame();
    }

    protected override void OnEnd()
    {

    }

    protected override void OnStart()
    {
        CoroutineRunner.instance.StartCoroutine(overflowLiquid.flood(floodTime));
    }
}
