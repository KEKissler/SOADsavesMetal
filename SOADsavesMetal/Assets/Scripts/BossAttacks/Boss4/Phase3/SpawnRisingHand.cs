using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/Sandaramet/SpawnRisingHand")]
public class SpawnRisingHand : SandarametAttack
{
    public GameObject AttackPrefab;
    public ProjectileSpeed ProjectileSpeed;
    public ProjectileType ProjectileType;
    public float angle;
    [SerializeField]
    private float Y_offset;

    private Transform attackParent;
    private Transform playerPosition;
    private Transform spawnPosition;
    private GameObject fireballObject;
    private Collider2D topCollider;
    private Collider2D bottomCollider;

    public override void Initialize(SandarametAttackData data)
    {
        attackParent = data.attackParent;
        playerPosition = data.player.transform;
        spawnPosition = data.player.transform;
        topCollider = data.screenTop;
        bottomCollider = data.screenBottom;
    }

    protected override void OnStart()
    {
        fireballObject = Instantiate(AttackPrefab, spawnPosition.position + new Vector3(0, Y_offset), Quaternion.identity, attackParent);
        if(fireballObject.GetComponent<SandHandMovement>() != null)
        {
            fireballObject.GetComponent<SandHandMovement>().goUpNow(playerPosition, Y_offset);
        }
    }

    protected override IEnumerator Execute(float duration)
    {
        yield return null;
        
    }

    protected override void OnEnd()
    {

    }
}
