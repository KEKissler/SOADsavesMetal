using System.Collections.Generic;

public class TsovinarAttackManager : BossAttackManager<TsovinarPhase>
{
    private new void Start()
    {
        base.Start();
        attackParent = FindObjectOfType<TsovinarAttackInitializer>().AttackData.attackParent;
    }

    protected override AttackOptions GetNextOptions(TsovinarPhase phase)
    {
        return phase.SelectNextAttackOption();
    }

    protected override TsovinarPhase GetNextPhase(List<PhaseChangeThreshhold> phaseChangeThreshhold, int phaseIndex)
    {
        return (TsovinarPhase)phaseChangeThreshhold[phaseIndex].BossPhase;
    }
}
