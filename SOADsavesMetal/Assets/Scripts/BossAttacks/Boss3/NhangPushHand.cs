using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/Nhang/PushStart")]
public class NhangPushHand : NhangAttack
{
    [FMODUnity.EventRef]
    public string groundShake;
    [SerializeField] private float speed = 0;
    [SerializeField] private float pushDuration = 1;

    private GameObject pushHand;
    private GameObject player;
    private bool onLeft;

    public override void Initialize(NhangAttackData data)
    {
        player = data.player;
        pushHand = data.NhangPushHand[0];
    }

    protected override IEnumerator Execute(float duration)
    {
        //FMOD.Studio.EventInstance instance = FMODUnity.RuntimeManager.CreateInstance(groundShake);
        //instance.start();
        //instance.release();

        yield return new WaitForEndOfFrame();

        PushScript pushScript = pushHand.GetComponent<PushScript>();
        if (!pushScript.isPushing && !pushScript.isAboveGround)
        {
            onLeft = player.transform.position.x < 0;
            pushHand.transform.position = new Vector3(player.transform.position.x + (onLeft ? -2 : 2), pushHand.transform.position.y, pushHand.transform.position.z);

            pushScript.Push(true, pushLeft: onLeft);
            pushScript.setSpeed(speed);
        }
    }

    private IEnumerator stopPushing(float delay)
    {
        yield return new WaitForSeconds(delay);
        pushHand.GetComponent<PushScript>().Push(false, pushLeft: onLeft);
    }

    protected override void OnEnd()
    {
        CoroutineRunner.instance.StartCoroutine(stopPushing(pushDuration));
    }

    protected override void OnStart()
    {
    }

    
}
