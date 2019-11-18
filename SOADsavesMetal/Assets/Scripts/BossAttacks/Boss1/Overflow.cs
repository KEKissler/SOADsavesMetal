using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/Agas/Overflow")]

public class Overflow : AgasAttack
{
    public float floodTime;

    private const string SPILL = "goblet_spill";
    private const string SPILL_DEFAULT = "goblet_end_flow";
    private const string SPLASH = "goblet_splash";
    //private const string SPLASH_DEFAULT = "goblet_no_splash";

    private DecayOverflow overflowLiquid;
    private Animator Spill, Splash;

    public override void Initialize(AgasAttackData data)
    {
        overflowLiquid = data.DecayOverflow;
        Splash = data.Splash;
        Spill = data.Spill;
    }

    protected override IEnumerator Execute(float duration)
    {
        yield return new WaitForEndOfFrame();
    }

    protected override void OnEnd()
    {

    }

    protected override void OnStart()
    {
        CoroutineRunner.instance.StartCoroutine(overflowLiquid.flood(floodTime, Spill, SPILL_DEFAULT));
        Splash.Play(SPLASH);
        Spill.Play(SPILL);
    }
}
