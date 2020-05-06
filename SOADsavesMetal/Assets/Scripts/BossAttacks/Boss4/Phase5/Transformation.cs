using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/Sandaramet/Transformation")]
public class Transformation : SandarametAttack
{
    private Animator anim;
    private BossHit hit;

    public override void Initialize(SandarametAttackData data)
    {
        anim = data.animator;
        duration = data.transformationAnim.length;
        hit = data.sandaramet.GetComponentInChildren<BossHit>();
    }

    protected override IEnumerator Execute(float duration)
    {

        yield return new WaitForSeconds(duration);
    }

    protected override void OnEnd()
    {
        hit.damageMultiplier = 1;
    }

    protected override void OnStart()
    {
        hit.damageMultiplier = 0;
        anim.Play("sandaramet_phase2_transition");
    }
}
