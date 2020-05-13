using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/Nhang/BloodRain")]
public class NewCryScript : NhangAttack
{
    [SerializeField] private float minFrequency;
    [SerializeField] private float maxFrequency;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private GameObject prefab;

    private Transform attackParent;

    public override void Initialize(NhangAttackData data)
    {
        attackParent = data.attackParent;
    }

    protected override IEnumerator Execute(float duration)
    {
        yield return null;
        CoroutineRunner.instance.StartCoroutine(cryBlood());
    }

    protected override void OnEnd()
    {
    }

    protected override void OnStart()
    {
    }

    private IEnumerator cryBlood()
    {
        while (true)
        {
            Instantiate(prefab, new Vector3(Random.Range(minX, maxX), 8, 0), Quaternion.identity, attackParent);
            yield return new WaitForSeconds(Random.Range(minFrequency, maxFrequency));
        }
    }

}
