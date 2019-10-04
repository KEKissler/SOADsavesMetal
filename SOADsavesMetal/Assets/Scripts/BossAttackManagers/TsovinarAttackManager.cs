using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TsovinarAttackManager : BossAttackManager
{
    //all prior attack state
    private BossAttacks attack;
    private AttackOptions options;
    private TsovinarPhase phase;
    private int phaseIndex;

    private Coroutine attackManager;

    void Start()
    {
        if(PhaseChangeThreshholds.Count < 1)
        {
            Debug.LogWarning("TsovinarAttackManager not setup.");
            return;
        }
        phase = (TsovinarPhase)PhaseChangeThreshholds[0].BossPhase;
        if (phaseIndex < PhaseChangeThreshholds.Count - 1)
        {
            phaseIndex++;
        }
        attackManager = StartCoroutine(ManageAttacks());
    }

    private IEnumerator ManageAttacks()
    {
        yield return new WaitForEndOfFrame();
        while (true)
        {
            attack = SelectNextAttack();
            attack.ExecuteAttack();
            yield return new WaitForSeconds(attack.duration);
        }
    }

    private BossAttacks SelectNextAttack()
    {
        //if new phase condition reached, switch phase (or announce end of final phase)
        if (BossHealth.getHPPercentage() < PhaseChangeThreshholds[phaseIndex].HealthPercentThreshhold)
        {
            if (phaseIndex < PhaseChangeThreshholds.Count - 1)
            {
                phaseIndex++;
            }
            else
            {
                FinalPhaseEnded();
            }
            phase = (TsovinarPhase)PhaseChangeThreshholds[phaseIndex].BossPhase;
        }
        //get current attack options from phase
        options = phase.SelectNextAttackOption();
        //get current attack from options
        return options.GetNextAttack();
    }

    private void FinalPhaseEnded()
    {
        StopCoroutine(attackManager);
        attackManager = null;
        Debug.Log("Ran Out of Phases, could not get next attack.");
    }
}
