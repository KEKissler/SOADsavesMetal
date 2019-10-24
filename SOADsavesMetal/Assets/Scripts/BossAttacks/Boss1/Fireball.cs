using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/Agas/Fireball")]
public class Fireball : AgasAttack
{
    public GameObject FireballPrefab;
    public ProjectileSpeed ProjectileSpeed;
    public ProjectileType ProjectileType;
    public float degreeModifier;

    private Transform attackParent;
    private Transform playerPosition;
    private Transform agasPosition;
    private GameObject fireballObject;

    public override void Initialize(AgasAttackData data)
    {
        attackParent = data.attackParent;
        playerPosition = data.player.transform;
        agasPosition = data.agasPosition;
    }

    protected override void OnStart()
    {
        fireballObject = Instantiate(FireballPrefab, agasPosition.position, Quaternion.identity, attackParent);
    }

    protected override IEnumerator Execute(float duration)
    {
        yield return null;
        fireballObject.GetComponent<Projectile>().Configure(playerPosition.gameObject, ProjectileType, ProjectileSpeed, degreeModifier);
    }

    protected override void OnEnd()
    {
        if(fireballObject != null)
        {
            Destroy(fireballObject);
        }
    }
}
