using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/Sandaramet/Beam Attack")]
public class HorizontalBeamAttack : SandarametAttack
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


    public override void Initialize(SandarametAttackData data)
    {
        playerLocation = data.player.transform;
        tsovinarLocation = data.sandaramet.transform;
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
        var beamObject = Instantiate(BeamPrefab, topMiddle, Quaternion.identity, attackParent).GetComponentInChildren<HorizontalBeamAnimation>();

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
