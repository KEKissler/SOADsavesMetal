using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/White Noise Attack")]
public class WhiteNoiseAttack : TsovinarAttack
{
    private const string ANTENNA_UNFOLD = "antenna_unfold";
    private const string ANTENNA_BLINK = "antenna_blinkrapid";
    private const string ANTENNA_FOLD = "antenna_fold";

    public Material whiteNoise;
    public AnimationClip foldClip;
    public AnimationClip unfoldClip;

    private GameObject screen1;
    private GameObject screen2;
    private GameObject screen3;
    private GameObject screen4;
    private GameObject screen5;

    private Material screen1DefaultMat;
    private Material screen2DefaultMat;
    private Material screen3DefaultMat;
    private Material screen4DefaultMat;
    private Material screen5DefaultMat;

    private Animator telescopingAntenna;

    public override void Initialize(TsovinarAttackData data)
    {
        screen1 = data.screen1;
        screen2 = data.screen2;
        screen3 = data.screen3;
        screen4 = data.screen4;
        screen5 = data.screen5;
        telescopingAntenna = data.antennaAnimator;
        screen1DefaultMat = screen1.GetComponent<SpriteRenderer>().sharedMaterial;
        screen2DefaultMat = screen2.GetComponent<SpriteRenderer>().sharedMaterial;
        screen3DefaultMat = screen3.GetComponent<SpriteRenderer>().sharedMaterial;
        screen4DefaultMat = screen4.GetComponent<SpriteRenderer>().sharedMaterial;
        screen5DefaultMat = screen5.GetComponent<SpriteRenderer>().sharedMaterial;
        
    }

    protected override IEnumerator Execute(float duration)
    {
        yield return new WaitForEndOfFrame();
        telescopingAntenna.Play(ANTENNA_UNFOLD);
        yield return new WaitForSeconds(unfoldClip.length);
        ScreenOff();
        yield return new WaitForSeconds(duration - foldClip.length - unfoldClip.length);
        telescopingAntenna.Play(ANTENNA_FOLD);
        ScreenOn();
    }

    protected override void OnEnd()
    {
        
    }

    protected override void OnStart()
    {
        Debug.Log("Antenna Up");
    }

    private void ScreenOn()
    {
        screen1.GetComponent<SpriteRenderer>().sharedMaterial = screen1DefaultMat;
        screen2.GetComponent<SpriteRenderer>().sharedMaterial = screen2DefaultMat;
        screen3.GetComponent<SpriteRenderer>().sharedMaterial = screen3DefaultMat;
        screen4.GetComponent<SpriteRenderer>().sharedMaterial = screen4DefaultMat;
        screen5.GetComponent<SpriteRenderer>().sharedMaterial = screen5DefaultMat;
    }

    private void ScreenOff()
    {
        screen1.GetComponent<SpriteRenderer>().sharedMaterial = whiteNoise;
        screen2.GetComponent<SpriteRenderer>().sharedMaterial = whiteNoise;
        screen3.GetComponent<SpriteRenderer>().sharedMaterial = whiteNoise;
        screen4.GetComponent<SpriteRenderer>().sharedMaterial = whiteNoise;
        screen5.GetComponent<SpriteRenderer>().sharedMaterial = whiteNoise;
    }
}
