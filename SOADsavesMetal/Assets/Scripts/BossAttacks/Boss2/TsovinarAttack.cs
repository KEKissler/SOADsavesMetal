public abstract class TsovinarAttack : BossAttack
{
    public FMOD.Studio.EventInstance instance;
    public abstract void Initialize(TsovinarAttackData data);
}
