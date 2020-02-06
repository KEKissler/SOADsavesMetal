using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NhangAttackManager : BossAttackManager<NhangPhase>
{
    protected override AttackOptions GetNextOptions(NhangPhase phase)
    {
        return phase.SelectNextAttackOption();
    }

    protected override NhangPhase GetNextPhase(List<PhaseChangeThreshhold> phaseChangeThreshhold, int phaseIndex)
    {
        return (NhangPhase)phaseChangeThreshhold[phaseIndex].BossPhase;
    }
}
