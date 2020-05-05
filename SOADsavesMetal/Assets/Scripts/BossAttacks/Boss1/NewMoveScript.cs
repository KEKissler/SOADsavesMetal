using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/Agas/Move")]
public class NewMoveScript : AgasAttack
{
    public bool isReverse;

    private List<Transform> originalPathVertices;
    private List<Transform> pathVertices;
    private GameObject agas;
    private float totalDistance;

    public override void Initialize(AgasAttackData data)
    {
        agas = data.agas;
        originalPathVertices = data.movePathVertices;
        Transform currentTransform = null;
        totalDistance = 0;

        foreach (Transform t in originalPathVertices)
        {
            if(currentTransform == null)
            {
                currentTransform = t;
                continue;
            }

            totalDistance += Vector3.Distance(t.position, currentTransform.position);
            currentTransform = t;
        }
    }

    protected override void OnStart()
    {
        pathVertices = new List<Transform>(originalPathVertices);
        if (isReverse)
        {
            pathVertices.Reverse();
        }
        agas.transform.position = pathVertices[0].position;
    }

    protected override IEnumerator Execute(float duration)
    {
        yield return new WaitForEndOfFrame();
        float speed = totalDistance / duration * Time.fixedDeltaTime;
        float movementLeftover = 0;
        Rigidbody2D rb = agas.GetComponent<Rigidbody2D>();
        foreach (Transform t in pathVertices)
        {
            float angle = Mathf.Atan2(t.position.y - agas.transform.position.y, t.position.x - agas.transform.position.x);

            if(movementLeftover > 0)
            {
                rb.MovePosition(new Vector2(movementLeftover * Mathf.Cos(angle), movementLeftover * Mathf.Sin(angle)));
                movementLeftover = 0;
            }

            while(true)
            {
                if (Vector2.Distance(t.position, agas.transform.position) < speed)
                {
                    movementLeftover = speed - Vector2.Distance(t.position, agas.transform.position);
                    rb.MovePosition(t.position);
                    break;
                }
                else
                {
                    rb.MovePosition((Vector2)agas.transform.position + new Vector2(speed * Mathf.Cos(angle), speed * Mathf.Sin(angle)));

                    yield return new WaitForFixedUpdate();
                }
            }
        }
    }

    protected override void OnEnd()
    {
        agas.transform.position = pathVertices[pathVertices.Count-1].position;
    }
}
