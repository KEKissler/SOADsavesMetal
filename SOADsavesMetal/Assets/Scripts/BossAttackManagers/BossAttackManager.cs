using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackManager : MonoBehaviour
{
    //switch phases depending on game state, for now just based on boss health
    public BossHealth BossHealth;
    public List<PhaseChangeThreshhold> PhaseChangeThreshholds = new List<PhaseChangeThreshhold>();
}

[System.Serializable]
public class PhaseChangeThreshhold
{
    public BossPhase BossPhase;
    public int HealthPercentThreshhold;
}
