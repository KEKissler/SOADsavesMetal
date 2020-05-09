using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/Sandaramet/Blood Claw")]
public class BloodClawAttack : SandarametAttack
{
    public float spawnTime;
    public GameObject bloodWavePrefab;

    private GameObject arm;
    private Transform spawnPoint;
    private Transform attackParent;

    public override void Initialize(SandarametAttackData data)
    {
        arm = data.arm;
        spawnPoint = data.secondFormHand;
        attackParent = data.attackParent;
    }

    protected override IEnumerator Execute(float duration)
    {
        yield return new WaitForEndOfFrame();
        Animator anim = arm.GetComponent<Animator>();
        anim.Play("armattack");
        yield return new WaitForSeconds(spawnTime);
        GameObject wave = Instantiate(bloodWavePrefab, spawnPoint.position, Quaternion.identity, attackParent);
        yield return new WaitForEndOfFrame();
        wave.GetComponentInChildren<Projectile>().Configure(attackParent.gameObject, ProjectileType.Linear, ProjectileSpeed.Stop, 0, -90f);
        yield return new WaitForSeconds(duration-spawnTime);
    }

    protected override void OnEnd()
    {
    }

    protected override void OnStart()
    {
    }
}
