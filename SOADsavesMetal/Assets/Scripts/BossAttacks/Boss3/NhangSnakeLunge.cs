using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/Nhang/Lunge")]
public class NhangSnakeLunge : NhangAttack
{
    GameObject player;
    GameObject snakeBase;

    public override void Initialize(NhangAttackData data)
    {
        player = data.player;
        snakeBase = data.NhangBase;
    }

    protected override IEnumerator Execute(float duration)
    {
        yield return null;
        if(snakeBase.GetComponent<Nhang>() != null)
            snakeBase.GetComponent<Nhang>().StartCoroutine(snakeBase.GetComponent<Nhang>().lunge(player));
    }

    protected override void OnEnd()
    {

    }

    protected override void OnStart()
    {
        
    }

    
}
