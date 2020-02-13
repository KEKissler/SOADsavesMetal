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
    public AntennaBolt antennaBolt;
    public float projectileFrequency = 1f;
    public int projectileCount = 4;
    public bool isLeft;

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

    private GameObject tsovinar;
    private SpriteRenderer faceVisable;
    private CapsuleCollider2D tsovinarHitBox;
    private CapsuleCollider2D antennaHitBox;

    private Transform spawnTransform;



    public override void Initialize(TsovinarAttackData data)
    {
        screen1 = data.screen1;
        screen2 = data.screen2;
        screen3 = data.screen3;
        screen4 = data.screen4;
        screen5 = data.screen5;
        if(isLeft)
        {
            telescopingAntenna = data.antennaAnimatorLeft;
        }
        else
        {
            telescopingAntenna = data.antennaAnimatorRight;
        }
        screen1DefaultMat = screen1.GetComponent<SpriteRenderer>().sharedMaterial;
        screen2DefaultMat = screen2.GetComponent<SpriteRenderer>().sharedMaterial;
        screen3DefaultMat = screen3.GetComponent<SpriteRenderer>().sharedMaterial;
        screen4DefaultMat = screen4.GetComponent<SpriteRenderer>().sharedMaterial;
        screen5DefaultMat = screen5.GetComponent<SpriteRenderer>().sharedMaterial;
        tsovinar = data.tsovinar;
        tsovinarHitBox = tsovinar.GetComponent<CapsuleCollider2D>();
        antennaHitBox = telescopingAntenna.GetComponent<CapsuleCollider2D>();
    }

    protected override IEnumerator Execute(float duration)
    {
        if (telescopingAntenna.GetCurrentAnimatorStateInfo(0).IsName(ANTENNA_BLINK))
        {
            yield break;
        }
        yield return new WaitForEndOfFrame();

        ScreenOff();
        telescopingAntenna.Play(ANTENNA_UNFOLD);

        float clipLength = unfoldClip.length;
        yield return new WaitForSeconds(clipLength);

        for (int i = 0; i < projectileCount; ++i)
        {
            antennaBolt.spawnPosition = spawnTransform;
            antennaBolt.ExecuteAttack();
            antennaBolt.angleOffset += 60;
            yield return new WaitForSeconds((duration - clipLength)/projectileCount);
        }

    }

    protected override void OnEnd()
    {
        telescopingAntenna.gameObject.GetComponent<AntennaWatcher>().OnEnd();
    }

    protected override void OnStart()
    {
        faceVisable = tsovinar.GetComponent<SpriteRenderer>();
        antennaBolt.angleOffset = 30;
        spawnTransform = telescopingAntenna.GetComponent<AntennaWatcher>().spawnPosition;
    }

    

    private void ScreenOff()
    {
        screen1.GetComponent<SpriteRenderer>().sharedMaterial = whiteNoise;
        screen2.GetComponent<SpriteRenderer>().sharedMaterial = whiteNoise;
        screen3.GetComponent<SpriteRenderer>().sharedMaterial = whiteNoise;
        screen4.GetComponent<SpriteRenderer>().sharedMaterial = whiteNoise;
        screen5.GetComponent<SpriteRenderer>().sharedMaterial = whiteNoise;
        Debug.Log(faceVisable);
        faceVisable.enabled = false;
        tsovinarHitBox.enabled = false;
        antennaHitBox.enabled = true;

        var script = telescopingAntenna.GetComponent<AntennaWatcher>();

        script.screen1DefaultMat = screen1DefaultMat;
        script.screen2DefaultMat = screen2DefaultMat;
        script.screen3DefaultMat = screen3DefaultMat;
        script.screen4DefaultMat = screen4DefaultMat;
        script.screen5DefaultMat = screen5DefaultMat;
        script.screen1 = screen1;
        script.screen2 = screen2;
        script.screen3 = screen3;
        script.screen4 = screen4;
        script.screen5 = screen5;
        script.faceVisable = faceVisable;
        script.tsovinarHitBox = tsovinarHitBox;
        script.antennaHitBox = antennaHitBox;

    }
}
