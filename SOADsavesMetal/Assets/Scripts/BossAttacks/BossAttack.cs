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
        IEnumerator temp = Execute(duration);
        if(temp != null)
            executingAttack = CoroutineRunner.instance.StartCoroutine(temp);
        CoroutineRunner.instance.StartCoroutine(EndAttack());
    }

    protected abstract void OnStart();
    protected abstract IEnumerator Execute(float duration);
    protected abstract void OnEnd();

    private IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(duration);
        if (executingAttack != null)
        {
            CoroutineRunner.instance.StopCoroutine(executingAttack);
        }
        else
        {
            Debug.LogWarning("Cannot stop a null Coroutine");
        }
        OnEnd();
    }
}
