using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/Nhang/Cry")]
public class StartCryingScript : NhangAttack
{
    GameObject[] TearSpawners;
    int numberOfTearSpawners;

    public override void Initialize(NhangAttackData data)
    {
        numberOfTearSpawners = data.tearEmitters.Length;
        TearSpawners = data.tearEmitters;
    }

    protected override IEnumerator Execute(float duration)
    {
        for (int i = 0; i < numberOfTearSpawners; i++)
        {
            TearSpawners[i].GetComponent<TearSpawner>().Cry();
        }
        yield return null;
        
    }

    protected override void OnEnd()
    {
    }

    protected override void OnStart()
    {
    }


}
