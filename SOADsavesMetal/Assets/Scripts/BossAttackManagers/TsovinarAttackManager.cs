﻿using System.Collections.Generic;

public class TsovinarAttackManager : BossAttackManager<TsovinarPhase>
{
    protected override AttackOptions GetNextOptions(TsovinarPhase phase)
    {
        return phase.SelectNextAttackOption();
    }

    protected override TsovinarPhase GetNextPhase(List<PhaseChangeThreshhold> phaseChangeThreshhold, int phaseIndex)
    {
        return (TsovinarPhase)phaseChangeThreshhold[phaseIndex].BossPhase;
    }
}
