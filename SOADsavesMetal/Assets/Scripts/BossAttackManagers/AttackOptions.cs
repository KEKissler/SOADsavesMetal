using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AttackGroup")]
public class AttackOptions : ScriptableObject
{
    public AttackSelectionType AttackSelectionType;
    public List<BossAttacks> Attacks;
    private int lastAttackSelectedIndex = -1;
    private readonly System.Random RNG = new System.Random();

    public BossAttacks GetNextAttack()
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
                newAttackIndex = RNG.Next(0, Attacks.Count);
                break;
            default:
                newAttackIndex = lastAttackSelectedIndex;
                break;
        }
        lastAttackSelectedIndex = newAttackIndex;
        return Attacks[newAttackIndex];
    }
}

public enum AttackSelectionType { Sequential, Random };
