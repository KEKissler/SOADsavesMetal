using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/Sandaramet/SpawnStalactite")]
public class SpawnStalactite : SandarametAttack
{
    

    
    private GameObject StalactiteSpawnerSpawner;
    

    public override void Initialize(SandarametAttackData data)
    {
        StalactiteSpawnerSpawner = data.SpikeSpawnerSpawner;
    }

    protected override void OnStart()
    {
        if (StalactiteSpawnerSpawner.GetComponent<SpikeSpawnerSpawner>() != null)
        {
            StalactiteSpawnerSpawner.GetComponent<SpikeSpawnerSpawner>().Spawn();
        }
        
    }

    protected override IEnumerator Execute(float duration)
    {
        yield return null;

    }

    protected override void OnEnd()
    {

    }
}

