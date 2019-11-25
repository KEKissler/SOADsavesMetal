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
    public Color startingColor;
    public Color attackColor;
    public Color flashColor;


    
    private Transform tsovinarLocation;
    private Transform playerLocation;
    private Transform attackParent;


    public override void Initialize(TsovinarAttackData data)
    {
        playerLocation = data.player.transform;
        tsovinarLocation = data.tsovinar.transform;
        attackParent = data.attackParent;     
    }

    protected override IEnumerator Execute(float duration)
    {
        yield return new WaitForEndOfFrame();
    }

    protected override void OnEnd()
    {
        Debug.Log("Beam gone");
    }

    protected override void OnStart()
    {
        Debug.Log("Charge the weapon!");
        var beamObject = Instantiate(BeamPrefab, tsovinarLocation.position, Quaternion.identity, attackParent).GetComponent<StaticBeamAnimation>();

        beamObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
        beamObject.preYLength = preYLength;
        beamObject.yAttackSize = yAttackSize;
        beamObject.chargeDuration = chargeDuration;
        beamObject.attackDuration = attackDuration;
        beamObject.flashDuration = flashDuration;
        beamObject.endDuration = endDuration;
        beamObject.startingColor = startingColor;
        beamObject.attackColor = attackColor;
        beamObject.flashColor = flashColor;
        beamObject.player = playerLocation;

    }
}
