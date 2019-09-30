using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class BossAttacks : ScriptableObject
{
    public Animation animation;

    public ProjectileSpeed projectileSpeed;
    public ProjectileType projectileType;
    //public int speed;
    public int damage = 1;
    public float duration;


    public void ExecuteAttack()
    {
        OnStart();
        CoroutineRunner.instance.StartCoroutine(Update(duration));
        CoroutineRunner.instance.StartCoroutine(EndAttack());
    }

    protected abstract void OnStart();
    protected abstract IEnumerator Update(float duration);
    protected abstract void OnEnd();

    private IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(duration);
        OnEnd();
    }

}
