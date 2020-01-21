using UnityEngine;

[CreateAssetMenu(menuName = "BossPhases/Agas")]
public class AgasPhase : BossPhase
{
    public float CloseUpDistance;
    public float NearGroundCutoff;
    public bool HasLeft;
    public AttackOptions DefaultAttacks;
    public AttackOptions CloseUpAttacks;
    public AttackOptions LowPlayerHPAttacks;
    public AttackOptions PlayerOnPlatformAttacks;
    public AttackOptions LeftDefaultAttacks;
    public AttackOptions LeftCloseUpAttacks;
    public AttackOptions LeftLowPlayerHPAttacks;
    public AttackOptions LeftPlayerOnPlatformAttacks;
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
        if(BossPosition.position.x < 0 && HasLeft)
        {

            if (TooCloseToPlayer())
            {
                return LeftCloseUpAttacks;
            }
            if (PlayerIsLowHealth())
            {
                return LeftLowPlayerHPAttacks;
            }
            if (PlayerOnPlatform())
            {
                return LeftPlayerOnPlatformAttacks;
            }
            return LeftDefaultAttacks;
        }
        else
        {
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
}
