using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/Tsovinar/DDRBeamMulti")]
public class DDRBeamMulti : TsovinarAttackSequence
{
    private GameObject tsovinar;
    private GameObject bigScreen;
    private Quaternion originalRotation;
    private GameObject[] screenArrows;

    public override void Initialize(TsovinarAttackData data)
    {
        tsovinar = data.tsovinar;
        bigScreen = data.screen1;
        originalRotation = tsovinar.transform.rotation;
        screenArrows = new GameObject[] { data.rightArrow, data.upArrow, data.leftArrow, data.downArrow };
    }

    protected override void OnEnd()
    {
        base.OnEnd();

        tsovinar.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
        tsovinar.transform.localRotation = originalRotation;
        tsovinar.transform.localPosition = bigScreen.transform.position;

        foreach (GameObject arrow in screenArrows)
        {
            arrow.SetActive(false);
        }
    }
}
