using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandarametAttackInitializer : MonoBehaviour
{
    public SandarametAttackData AttackData;
    public List<SandarametAttack> AttacksToInitialize = new List<SandarametAttack>();

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

