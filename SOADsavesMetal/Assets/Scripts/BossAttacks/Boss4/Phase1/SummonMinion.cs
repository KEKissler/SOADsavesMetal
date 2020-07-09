using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/Sandaramet/SummonMinion")]
public class SummonMinion : SandarametAttack
{
    public GameObject AttackPrefab;
    public ProjectileSpeed ProjectileSpeed;
    public ProjectileType ProjectileType;
    public float angle;
    [SerializeField]
    private float Y_offset;
    [SerializeField]
    private bool isChargeMinion;

    private Transform attackParent;
    private Transform playerPosition;
    private Transform spawnPosition;
    private GameObject fireballObject;
    private Collider2D topCollider;
    private Collider2D bottomCollider;

    [FMODUnity.EventRef]
    public string minionChargeEvent;

    public override void Initialize(SandarametAttackData data)
    {
        attackParent = data.attackParent;
        playerPosition = data.player.transform;
        spawnPosition = data.topHand;
        topCollider = data.screenTop;
        bottomCollider = data.screenBottom;
    }

    protected override void OnStart()
    {
        fireballObject = Instantiate(AttackPrefab, spawnPosition.position + new Vector3(0, Y_offset), Quaternion.identity, attackParent);
    }

    protected override IEnumerator Execute(float duration)
    {
        yield return null;
        if (fireballObject.GetComponent<Projectile>() != null)
        {
            fireballObject.GetComponent<Projectile>().Configure(playerPosition.gameObject, ProjectileType, ProjectileSpeed, 0f, angle);
            if (isChargeMinion)
            {
                EventInstance minionInstance = FMODUnity.RuntimeManager.CreateInstance(minionChargeEvent);
                minionInstance.start();
            }
        }
    }

    protected override void OnEnd()
    {

    }
}
