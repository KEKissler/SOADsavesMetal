using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/Nhang/HandRaise")]
public class HandRaise : NhangAttack
{
    GameObject[] HandObjects;
    GameObject HandObject;
    int numberOfHands;

    public override void Initialize(NhangAttackData data)
    {
        numberOfHands = data.NhangHand.Length;
        HandObjects = data.NhangHand;
        HandObject = HandObjects[Random.Range(0, numberOfHands)];
    }

    protected override IEnumerator Execute(float duration)
    {
        bool isUp = false;
        for(int i = 0; i<4 && !isUp; i++)
        {
            HandObject = HandObjects[Random.Range(0, numberOfHands)];
        }
        yield return null;
        if(HandObject.GetComponent<NhangHand>() != null)
        {
            HandObject.GetComponent<NhangHand>().moveUp();
        }
    }

    protected override void OnEnd()
    {
    }

    protected override void OnStart()
    {
    }

    
}
