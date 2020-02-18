using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/Nhang/TrackBolt")]
public class TrackBolt : NhangAttack
{
    [SerializeField]
    private GameObject fireboltPrefab;
    [SerializeField]
    private ProjectileSpeed ProjectileSpeed;
    [SerializeField]
    private ProjectileType ProjectileType;
    [SerializeField]
    private float degreeModifier;
    [SerializeField]
    public float fixedAngle;

    private Transform playerPosition;
    private GameObject attackParent;
    private Transform snakeHeadPosition;
    private GameObject fireboltObject;

    public override void Initialize(NhangAttackData data)
    {
        attackParent = data.Nhang;
        playerPosition = data.player.transform;
        snakeHeadPosition = data.SnakePosition;
    }

    protected override IEnumerator Execute(float duration)
    {
        yield return null;
        if (fireboltObject.GetComponent<Projectile>() != null)
            fireboltObject.GetComponent<Projectile>().Configure(playerPosition.gameObject, ProjectileType, ProjectileSpeed,
                degreeModifier);

    }

    protected override void OnEnd()
    {
        //
    }

    protected override void OnStart()
    {
        fireboltObject = Instantiate(fireboltPrefab, snakeHeadPosition.position, Quaternion.identity, attackParent.transform);
    }

    
}
