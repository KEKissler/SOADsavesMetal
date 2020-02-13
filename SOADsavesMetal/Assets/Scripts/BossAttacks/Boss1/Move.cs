using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "New Attack/Agas/Move")]
public class Move : AgasAttack
{
    [Header("Duration is the sum of all delays below")]
    public List<PlannedPosition> PlannedPositions = new List<PlannedPosition>();
    private GameObject agas;
    private Vector2 startingPosition;

    public override void Initialize(AgasAttackData data)
    {
        agas = data.agas;
        duration = PlannedPositions.Select(a => a.delayUntilMovingAgain).Aggregate(0f, (a, c) => a + c);
    }

    protected override void OnStart()
    {
        
    }

    protected override IEnumerator Execute(float duration)
    {
        yield return new WaitForEndOfFrame();
        foreach (var pp in PlannedPositions)
        {
            LeanTween.move(agas.gameObject, pp.position, pp.timeSpentMoving);
            yield return new WaitForSeconds(pp.delayUntilMovingAgain);
        }
    }

    protected override void OnEnd()
    {

    }

    [System.Serializable]
    public struct PlannedPosition
    {
        [Tooltip("How long in seconds to wait before moving again")]
        public float delayUntilMovingAgain;
        [Tooltip("How long in seconds to move from old to new position")]
        public float timeSpentMoving;
        [Tooltip("The end position of this movement")]
        public Vector2 position;
    }
}
