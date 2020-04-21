using System.Collections.Generic;

public class SandarametAttackManager : BossAttackManager<SandarametPhase>
{
    protected override AttackOptions GetNextOptions(SandarametPhase phase)
    {
        return phase.SelectNextAttackOption();
    }

    protected override SandarametPhase GetNextPhase(List<PhaseChangeThreshhold> phaseChangeThreshhold, int phaseIndex)
    {
        return (SandarametPhase)phaseChangeThreshhold[phaseIndex].BossPhase;
    }
}
