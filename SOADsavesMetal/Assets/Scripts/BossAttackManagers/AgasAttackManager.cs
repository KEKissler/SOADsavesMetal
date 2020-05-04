using System.Collections.Generic;

public class AgasAttackManager : BossAttackManager<AgasPhase>
{
    private new void Start()
    {
        base.Start();
        attackParent = FindObjectOfType<AgasAttackInitializer>().AttackData.attackParent;
    }

    protected override AttackOptions GetNextOptions(AgasPhase phase)
    {
        return phase.SelectNextAttackOption();
    }

    protected override AgasPhase GetNextPhase(List<PhaseChangeThreshhold> phaseChangeThreshhold, int phaseIndex)
    {
        return (AgasPhase)phaseChangeThreshhold[phaseIndex].BossPhase;
    }
}
