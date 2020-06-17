using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/Sandaramet/Transformation")]
public class Transformation : SandarametAttack
{
    private Animator anim;
    private BossHit hit;
    private Sidescroll sidescroll;
    private float citySpeed;
    private float groundSpeed;
    private float skySpeed;

    public override void Initialize(SandarametAttackData data)
    {
        anim = data.animator;
        duration = data.transformationAnim.length;
        hit = data.sandaramet.GetComponentInChildren<BossHit>();
        sidescroll = FindObjectOfType<Sidescroll>();
    }

    protected override IEnumerator Execute(float duration)
    {
        yield return new WaitForSeconds(duration);
    }

    protected override void OnEnd()
    {
        hit.damageMultiplier = 1;

        sidescroll.citySpeed = -citySpeed;

        sidescroll.groundSpeed = -groundSpeed;
        
        sidescroll.skySpeed = -skySpeed;
    }

    protected override void OnStart()
    {
        hit.damageMultiplier = 0;

        citySpeed = sidescroll.citySpeed;
        sidescroll.citySpeed = 0;

        groundSpeed = sidescroll.groundSpeed;
        sidescroll.groundSpeed = 0;
        
        skySpeed = sidescroll.skySpeed;
        sidescroll.skySpeed = 0;

        anim.Play("sandaramet_phase2_transition");
    }
}
