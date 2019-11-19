using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/Static Beam Attack")]
public class StaticBeamAttack : TsovinarAttack
{
    private const string BEAM_CHARGE = "beam_charge";
    private const string BEAM_FIRE = "beam_fire";
    private const string BEAM_STOP = "beam_stop";

    public GameObject BeamPrefab;
    public float CycleTime;
    public AnimationClip beamShrinkClip;
    public AnimationClip beamChargeClip;

    private Animator beamAnim;
    private Transform tsovinarLocation;
    private Transform playerLocation;
    private Transform attackParent;
    private GameObject beamObject;

    public override void Initialize(TsovinarAttackData data)
    {
        playerLocation = data.player.GetComponent<Transform>();
        tsovinarLocation = data.tsovinar.GetComponent<Transform>();
        attackParent = data.attackParent;
        
    }

    protected override IEnumerator Execute(float duration)
    {
        yield return new WaitForEndOfFrame();

        beamAnim.Play(BEAM_STOP);
        yield return new WaitForSeconds(beamShrinkClip.length);
    }

    protected override void OnEnd()
    {
        Destroy(beamObject.gameObject);
        Debug.Log("Beam gone");
    }

    protected override void OnStart()
    {
        Debug.Log("Charge the weapon!");
        beamObject = Instantiate(BeamPrefab, tsovinarLocation.position, Quaternion.identity, attackParent);
        beamAnim = Instantiate(BeamPrefab, tsovinarLocation.position, Quaternion.identity, attackParent).GetComponent<Animator>();

        beamAnim.Play(BEAM_CHARGE);

    }
}
