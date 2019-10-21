using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class BossAttack : ScriptableObject
{
    public float duration;

    private Coroutine executingAttack;


    public void ExecuteAttack()
    {
        OnStart();
        executingAttack = CoroutineRunner.instance.StartCoroutine(Execute(duration));
        CoroutineRunner.instance.StartCoroutine(EndAttack());
    }

    protected abstract void OnStart();
    protected abstract IEnumerator Execute(float duration);
    protected abstract void OnEnd();

    private IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(duration);
        CoroutineRunner.instance.StopCoroutine(executingAttack);
        OnEnd();
    }
}
