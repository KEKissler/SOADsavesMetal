using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/Nhang/AttackSequence")]
public class NhangAttackSequence : NhangAttack
{


    public List<SubAttack> Attacks;

    private readonly List<float> originalDurations = new List<float>();

    public override void Initialize(NhangAttackData data)
    {
        //assumes each attack in attacks is already initialized
    }

    protected override void OnStart()
    {
        foreach (var subAttack in Attacks)
        {
            originalDurations.Add(subAttack.Attack.duration);
            subAttack.Attack.duration = subAttack.duration;
        }
    }

    protected override IEnumerator Execute(float duration)
    {
        yield return new WaitForEndOfFrame();
        foreach (var subAttack in Attacks)
        {
            subAttack.Attack.ExecuteAttack();
            yield return new WaitForSeconds(subAttack.duration);
        }
    }

    protected override void OnEnd()
    {
        for (int i = 0; i < Attacks.Count; ++i)
        {
            Attacks[i].duration = originalDurations[i];
        }
    }
    [System.Serializable]
    public class SubAttack
    {
        public NhangAttack Attack;
        public float duration;
    }
}


