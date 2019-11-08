using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/Static Beam Attack")]
public class StaticBeamAttack : TsovinarAttack
{
    private const string BEAM_OFF = "beam_off";
    private const string BEAM_ON = "beam_on";

    public GameObject boss;
    public float CycleTime;

    private Transform bossLocation;
    private Transform attackParent;

    private Animator beam;

    public override void Initialize(TsovinarAttackData data)
    {
        throw new System.NotImplementedException();
    }

    protected override IEnumerator Execute(float duration)
    {
        throw new System.NotImplementedException();
    }

    protected override void OnEnd()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnStart()
    {
        throw new System.NotImplementedException();
    }
}
