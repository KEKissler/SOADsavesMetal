using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/White Noise Fixed")]
public class WhiteNoiseFixedSide : WhiteNoiseAttack
{
    public bool isOnLeftSide;

    protected override void OnStart()
    {
        isLeft = isOnLeftSide;
        base.OnStart();
        antennaWatcherScript.whiteNoise = this;
        antennaCount = 2;
    }
    protected override void OnEnd()
    {

    }
}
