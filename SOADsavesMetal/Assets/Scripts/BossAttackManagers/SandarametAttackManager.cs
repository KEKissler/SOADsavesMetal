using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandarametAttackManager : BossAttackManager<SandarametPhase>
{
    private new void Start()
    {
        base.Start();
        attackParent = FindObjectOfType<SandarametAttackInitializer>().AttackData.attackParent;
    }

    protected override AttackOptions GetNextOptions(SandarametPhase phase)
    {
        return phase.SelectNextAttackOption();
    }

    protected override SandarametPhase GetNextPhase(List<PhaseChangeThreshhold> phaseChangeThreshhold, int phaseIndex)
    {
        return (SandarametPhase)phaseChangeThreshhold[phaseIndex].BossPhase;
    }

    public void deathScreenShake()
    {
        StartCoroutine(infiniteScreenShake());
    }

    private IEnumerator infiniteScreenShake()
    {
        while(true)
        {
            yield return new WaitForEndOfFrame();
            screenShake.shake(.25f);
        }
    }
}
