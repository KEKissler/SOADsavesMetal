using UnityEngine;

[CreateAssetMenu(menuName = "BossPhases/Sandaramet")]
public class SandarametPhase : BossPhase
{
    [SerializeField]
    private AttackOptions Default;
    protected override bool PlayerNearGround()
    {
        return Player.transform.position.y < 0;
    }

    protected override bool TooCloseToPlayer()
    {
        return (Player.transform.position - BossPosition.position).sqrMagnitude <= 0 * 0;
    }

    public AttackOptions SelectNextAttackOption()
    {
        return Default;
    }
}
