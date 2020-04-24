using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/Nhang/PushStart")]
public class NhangPushHand : NhangAttack
{
    [SerializeField]
    float speed = 0;
    GameObject[] PushHand;
    [FMODUnity.EventRef]
    public string groundShake;

    public override void Initialize(NhangAttackData data)
    {
        PushHand = data.NhangPushHand;
    }

    protected override IEnumerator Execute(float duration)
    {
        yield return null;

        Debug.Log("Push Damn it");
        for (int i = 0; i < PushHand.Length; i++)
        {
            FMOD.Studio.EventInstance instance = FMODUnity.RuntimeManager.CreateInstance(groundShake);
            instance.start();
            PushHand[i].GetComponent<PushScript>().Push();
            PushHand[i].GetComponent<PushScript>().setSpeed(speed);
        }
    }

    protected override void OnEnd()
    {
    }

    protected override void OnStart()
    {
    }

    
}
