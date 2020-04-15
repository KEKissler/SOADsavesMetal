using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TsovinarAttackInitializer : MonoBehaviour
{
    public TsovinarAttackData AttackData;

    public List<TsovinarAttack> AttacksToInitialize = new List<TsovinarAttack>();

    private void Start()
    {
        foreach(var attack in AttacksToInitialize)
        {
            attack.Initialize(AttackData);
        }
    }
    private void OnDestroy()
    {
        foreach (var attack in AttacksToInitialize)
        {
            if(attack.instance.isValid())
            {
                FMOD.Studio.PLAYBACK_STATE state;
                attack.instance.getPlaybackState(out state);
                if(state != FMOD.Studio.PLAYBACK_STATE.STOPPED)
                {
                    attack.instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                }
            }
        }
    }
}

