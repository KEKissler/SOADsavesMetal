using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodClawAttack : SandarametAttack
{


    public override void Initialize(SandarametAttackData data)
    {
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
    }
}
