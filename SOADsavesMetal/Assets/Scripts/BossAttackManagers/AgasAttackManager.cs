using System.Collections.Generic;

public class AgasAttackManager : BossAttackManager<AgasPhase>
{
    protected override AttackOptions GetNextOptions(AgasPhase phase)
    {
        return phase.SelectNextAttackOption();
    }

    protected override AgasPhase GetNextPhase(List<PhaseChangeThreshhold> phaseChangeThreshhold, int phaseIndex)
    {
        return (AgasPhase)phaseChangeThreshhold[phaseIndex].BossPhase;
    }
}
