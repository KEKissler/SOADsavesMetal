using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/Tsovinar/AntennaBolt")]
public class AntennaBolt : TsovinarAttack
{
    public GameObject FireballPrefab;
    public ProjectileSpeed ProjectileSpeed;
    public ProjectileType ProjectileType;
    public float degreeModifier;
    public float fixedAngle = float.MinValue;
    [SerializeField]
    private float Y_offset;
    [HideInInspector]
    public Transform spawnPosition;
    [HideInInspector]
    public float angleOffset;

    private Transform attackParent;
    private Transform playerPosition;
    private GameObject[] fireballObject;

    public override void Initialize(TsovinarAttackData data)
    {
        attackParent = data.attackParent;
        playerPosition = data.player.transform;
        spawnPosition = data.tsovinar.transform;
        fireballObject = new GameObject[4];
    }

    protected override void OnStart()
    {
        for (int i = 0; i < 4; ++i)
        {
            fireballObject[i] = Instantiate(FireballPrefab, spawnPosition.position + new Vector3(0, Y_offset), Quaternion.identity, attackParent);
        }
    }

    protected override IEnumerator Execute(float duration)
    {
        yield return null;

        for (int i = 0; i < 4; ++i)
        {
            fixedAngle = (i*90 + angleOffset) % 360;
            if (fixedAngle == float.MinValue)
            {
                if (fireballObject[i].GetComponent<Projectile>() != null)
                    fireballObject[i].GetComponent<Projectile>().Configure(playerPosition.gameObject, ProjectileType, ProjectileSpeed, degreeModifier);
            }
            else
            {
                if (fireballObject[i].GetComponent<Projectile>() != null)
                    fireballObject[i].GetComponent<Projectile>().Configure(playerPosition.gameObject, ProjectileType, ProjectileSpeed, degreeModifier, fixedAngle);
            }
        }
    }

    protected override void OnEnd()
    {
        
    }
}
