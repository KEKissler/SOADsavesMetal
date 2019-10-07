using UnityEngine;

public abstract class BossPhase : ScriptableObject
{
    [HideInInspector]
    public Transform BossPosition;
    [HideInInspector]
    public Player Player;
    protected abstract bool TooCloseToPlayer();//abstract to let specific boss define what distance is too close for that boss
    protected abstract bool PlayerNearGround();//dunno if height of ground is fixed or not

    protected bool PlayerOnPlatform()
    {
        return !(Player.inAir || PlayerNearGround());
    }

    protected bool PlayerIsLowHealth()
    {
        return Player.Health == 1;
    }   
}
