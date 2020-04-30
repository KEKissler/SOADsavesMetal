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
    public float fixedAngle = float.MinValue;
    [SerializeField]
    private float Y_offset;

    private Transform attackParent;
    private Transform playerPosition;
    private Transform agasPosition;
    private GameObject fireballObject;

    [FMODUnity.EventRef]
    public string fireballEvent;

    public override void Initialize(AgasAttackData data)
    {
        attackParent = data.attackParent;
        playerPosition = data.player.transform;
        agasPosition = data.agasPosition;
    }

    protected override void OnStart()
    {
        fireballObject = Instantiate(FireballPrefab, agasPosition.position + new Vector3(0,Y_offset), Quaternion.identity, attackParent);
    }

    protected override IEnumerator Execute(float duration)
    {
        yield return null;
        if(fixedAngle == float.MinValue)
        {
            if(fireballObject.GetComponent<Projectile>() != null)
                fireballObject.GetComponent<Projectile>().Configure(playerPosition.gameObject, ProjectileType, ProjectileSpeed, degreeModifier);
        }
        else
        {
            if (fireballObject.GetComponent<Projectile>() != null)
                fireballObject.GetComponent<Projectile>().Configure(playerPosition.gameObject, ProjectileType, ProjectileSpeed, degreeModifier, fixedAngle);
        }
        FMOD.Studio.EventInstance fireballInstance = FMODUnity.RuntimeManager.CreateInstance(fireballEvent);
        fireballInstance.start();
        fireballInstance.release();
    }

    protected override void OnEnd()
    {
        
    }
}
