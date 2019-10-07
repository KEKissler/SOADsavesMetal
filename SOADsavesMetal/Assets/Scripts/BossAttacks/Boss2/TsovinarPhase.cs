using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BossPhases/Tsovinar")]
public class TsovinarPhase : BossPhase
{
    public float CloseUpDistance;
    public float NearGroundCutoff;
    public AttackOptions DefaultAttacks;
    public AttackOptions CloseUpAttacks;
    public AttackOptions LowPlayerHPAttacks;
    public AttackOptions PlayerOnPlatformAttacks;
    //switch active attackOption depending on 3 factors: distance to player, player on a platform above ground, player hp gets low

    protected override bool PlayerNearGround()
    {
        return Player.transform.position.y < NearGroundCutoff;
    }

    protected override bool TooCloseToPlayer()
    {
        return (Player.transform.position - BossPosition.position).sqrMagnitude <= CloseUpDistance * CloseUpDistance;
    }

    //meant to be called when selecting a new attack, decides if the boss changes the set of attacks it pulls from to decide its next attack
    public AttackOptions SelectNextAttackOption()
    {
        //todo maybe: make priorities reorderable
        if (TooCloseToPlayer())
        {
            return CloseUpAttacks; 
        }
        if (PlayerIsLowHealth())
        {
            return LowPlayerHPAttacks;
        }
        if (PlayerOnPlatform())
        {
            return PlayerOnPlatformAttacks;
        }
        return DefaultAttacks;
    }
}
