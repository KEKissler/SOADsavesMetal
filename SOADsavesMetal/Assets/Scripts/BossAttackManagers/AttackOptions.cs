using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AttackGroup")]
public class AttackOptions : ScriptableObject
{
    public AttackSelectionType AttackSelectionType;
    public List<WeightedAttack> Attacks;
    private int lastAttackSelectedIndex = -1;
    private readonly System.Random RNG = new System.Random();

    [System.Serializable]
    public class WeightedAttack
    {
        public BossAttack attack;
        public float percentThreshold;
    }

    public BossAttack GetNextAttack()
    {
        if (Attacks.Count < 1)
        {
            return null;
        }
        int newAttackIndex;
        switch (AttackSelectionType)
        {
            case AttackSelectionType.Sequential:
                newAttackIndex = (lastAttackSelectedIndex + 1) % Attacks.Count;
                break;
            case AttackSelectionType.Random:
                newAttackIndex = 0;
                double percent = RNG.NextDouble() * 100;
                for(int i = 0; i < Attacks.Count; ++i)
                {
                    if(percent <= Attacks[i].percentThreshold)
                    {
                        newAttackIndex = i;
                        break;
                    }
                }
                break;
            default:
                newAttackIndex = lastAttackSelectedIndex;
                break;
        }
        lastAttackSelectedIndex = newAttackIndex;
        return Attacks[newAttackIndex].attack;
    }
}

public enum AttackSelectionType { Sequential, Random };
