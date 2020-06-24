﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossAttackManager<T> : MonoBehaviour where T : BossPhase
{
    //all prior attack state
    private BossAttack attack;
    private AttackOptions options;
    private T phase;
    private float timer = 3;

    protected Coroutine attackManager;
    protected Transform attackParent;

    //switch phases depending on game state, for now just based on boss health
    public BossHealth BossHealth;
    public ScreenShake screenShake;
    public List<PhaseChangeThreshhold> PhaseChangeThreshholds = new List<PhaseChangeThreshhold>();
    public int phaseIndex;
   

    protected void Start()
    {
        if (PhaseChangeThreshholds.Count < 1)
        {
            Debug.LogWarning("AttackManager not setup.");
            return;
        }
        phase = GetNextPhase(PhaseChangeThreshholds, 0);
        /*
        if (phaseIndex < PhaseChangeThreshholds.Count - 1)
        {
            phaseIndex++;
        }
        */
    }

    private void Update()
    {
        if(timer > 0 && timer < 100)
        {
            timer -= Time.deltaTime;
        }
        else if(timer <= 0)
        {
            attackManager = StartCoroutine(ManageAttacks());
            timer = 1000;
        }
    }

    private IEnumerator ManageAttacks()
    {
        yield return new WaitForEndOfFrame();
        while (!BossHealth.isDead())
        {
            BossAttack newAttack = SelectNextAttack();
            if(attack == newAttack)
            {
                newAttack = SelectNextAttack();
            }
            attack = newAttack;
            attack.ExecuteAttack();
            yield return new WaitForSeconds(attack.duration);
        }
        stopCurrentAttack();
    }

    private BossAttack SelectNextAttack()
    {
        //get current attack options from phase
        options = GetNextOptions(phase);
        //get current attack from options
        return options.GetNextAttack();
    }

    public void bossHit()
    {
        //Debug.Log("The Phase is: " + phase);
        //if new phase condition reached, switch phase (or announce end of final phase)
        if (BossHealth.getHPPercentage() <= PhaseChangeThreshholds[phaseIndex].HealthPercentThreshhold)
        {
            if (phaseIndex < PhaseChangeThreshholds.Count - 1)
            {
                phaseIndex++;
                screenShake.shake(0.5f);
                Debug.Log("The Phase is: " + phase);
                
                if (PhaseChangeThreshholds[phaseIndex].ExecuteOnPhaseStart)
                {
                    PhaseChangeThreshholds[phaseIndex].ExecuteOnPhaseStart.ExecuteAttack();
                    stopCurrentAttack();
                    timer = PhaseChangeThreshholds[phaseIndex].ExecuteOnPhaseStart.duration;
                }
            }
            else
            {
                FinalPhaseEnded();
            }
            phase = GetNextPhase(PhaseChangeThreshholds, phaseIndex);
            Debug.Log("The Health% is: " + BossHealth.getHPPercentage());
        }
    }

    protected abstract AttackOptions GetNextOptions(T phase);
    protected abstract T GetNextPhase(List<PhaseChangeThreshhold> phaseChangeThreshhold, int phaseIndex);

    public void stopCurrentAttack()
    {
        StopCoroutine(attackManager);
        for (int i = 0; i < attackParent.childCount; ++i)
        {
            Destroy(attackParent.GetChild(i).gameObject);
        }
    }

    private void FinalPhaseEnded()
    {
        stopCurrentAttack();
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
