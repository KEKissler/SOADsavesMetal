using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/Agas/Decay Dip")]
public class DecayDip : AgasAttackSequence
{
    public float distance;
    public float timeDown;
    public float timeStay;
    public float timeUp;

    private Transform agasPosition;
    private Vector3 startPosition;

    public override void Initialize(AgasAttackData data)
    {
        base.Initialize(data);
        agasPosition = data.agasPosition;
    }

    protected override void OnStart()
    {
        base.OnStart();
        startPosition = agasPosition.position;
    }

    protected override IEnumerator Execute(float duration)
    {
        agasPosition.LeanMoveY(agasPosition.position.y - distance, timeDown);
        yield return new WaitForSeconds(timeDown);
        yield return base.Execute(duration);
        agasPosition.LeanMoveY(agasPosition.position.y + distance, timeUp);
        yield return new WaitForSeconds(timeUp);
    }

    protected override void OnEnd()
    {
        base.OnEnd();
        agasPosition.position = startPosition;
    }
}
