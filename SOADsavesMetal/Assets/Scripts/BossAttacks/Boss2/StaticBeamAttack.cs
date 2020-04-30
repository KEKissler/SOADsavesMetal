using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/Static Beam Attack")]
public class StaticBeamAttack : TsovinarAttack
{
    public GameObject BeamPrefab;
    public float CycleTime;
    public float preYLength;
    public float yAttackSize;
    public float chargeDuration;
    public float attackDuration;
    public float flashDuration;
    public float endDuration;
    public float trackingSpeed;
    public float laserHeight = 10;
    public Color startingColor;
    public Color attackColor;
    public Color flashColor;


    
    private Transform tsovinarLocation;
    private Transform playerLocation;
    private Transform attackParent;

    private Vector3 topMiddle;

    [FMODUnity.EventRef]
    public string laserEvent;


    public override void Initialize(TsovinarAttackData data)
    {
        playerLocation = data.player.transform;
        tsovinarLocation = data.tsovinar.transform;
        attackParent = data.attackParent;
        topMiddle = new Vector3(0, laserHeight, 0);
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
        FMOD.Studio.EventInstance instance = FMODUnity.RuntimeManager.CreateInstance(laserEvent);
        instance.start();
        instance.release();
        var beamObject = Instantiate(BeamPrefab, topMiddle, Quaternion.identity, attackParent).GetComponent<StaticBeamAnimation>();

        beamObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
        beamObject.preYLength = preYLength;
        beamObject.yAttackSize = yAttackSize;
        beamObject.chargeDuration = chargeDuration;
        beamObject.attackDuration = attackDuration;
        beamObject.flashDuration = flashDuration;
        beamObject.endDuration = endDuration;
        beamObject.trackingSpeed = trackingSpeed;
        beamObject.startingColor = startingColor;
        beamObject.attackColor = attackColor;
        beamObject.flashColor = flashColor;
        beamObject.player = playerLocation;
    }
}
