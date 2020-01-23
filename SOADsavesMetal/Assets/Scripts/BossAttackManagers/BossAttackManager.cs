﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossAttackManager<T> : MonoBehaviour where T : BossPhase
{
    //all prior attack state
    private BossAttack attack;
    private AttackOptions options;
    private T phase;
    private int phaseIndex;

    private Coroutine attackManager;

    //switch phases depending on game state, for now just based on boss health
    public BossHealth BossHealth;
    public List<PhaseChangeThreshhold> PhaseChangeThreshholds = new List<PhaseChangeThreshhold>();
   

    private void Start()
    {
        if (PhaseChangeThreshholds.Count < 1)
        {
            Debug.LogWarning("AttackManager not setup.");
            return;
        }
        phase = GetNextPhase(PhaseChangeThreshholds, 0);
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

    private BossAttack SelectNextAttack()
    {
        //if new phase condition reached, switch phase (or announce end of final phase)
        if (BossHealth.getHPPercentage() < PhaseChangeThreshholds[phaseIndex].HealthPercentThreshhold)
        {
            if (phaseIndex < PhaseChangeThreshholds.Count - 1)
            {
                phaseIndex++;
                if(PhaseChangeThreshholds[phaseIndex].ExecuteOnPhaseStart)
                {
                    PhaseChangeThreshholds[phaseIndex].ExecuteOnPhaseStart.ExecuteAttack();
                }
            }
            else
            {
                FinalPhaseEnded();
            }
            phase = GetNextPhase(PhaseChangeThreshholds, phaseIndex);
        }
        //get current attack options from phase
        options = GetNextOptions(phase);
        //get current attack from options
        return options.GetNextAttack();
    }

    protected abstract AttackOptions GetNextOptions(T phase);
    protected abstract T GetNextPhase(List<PhaseChangeThreshhold> phaseChangeThreshhold, int phaseIndex);


    private void FinalPhaseEnded()
    {
        StopCoroutine(attackManager);
        attackManager = null;
        Debug.Log("Ran Out of Phases, could not get next attack.");
    }
}

[System.Serializable]
public class PhaseChangeThreshhold
{
    public BossPhase BossPhase;
    public int HealthPercentThreshhold;
    public BossAttack ExecuteOnPhaseStart;
}
