using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/White Noise Attack")]
public class WhiteNoiseAttack : TsovinarAttackSequence
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

    private Animator leftAntenna;
    private Animator rightAntenna;
    private Animator telescopingAntenna;

    private GameObject tsovinar;
    private CapsuleCollider2D antennaHitBox;

    private Transform spawnTransform;
    private bool isLeft;



    public override void Initialize(TsovinarAttackData data)
    {
        isLeft = false;
        screen1 = data.screen1;
        screen2 = data.screen2;
        screen3 = data.screen3;
        screen4 = data.screen4;
        screen5 = data.screen5;
        leftAntenna = data.antennaAnimatorLeft;
        rightAntenna = data.antennaAnimatorRight;
        telescopingAntenna = rightAntenna;
        screen1DefaultMat = screen1.GetComponent<SpriteRenderer>().sharedMaterial;
        screen2DefaultMat = screen2.GetComponent<SpriteRenderer>().sharedMaterial;
        screen3DefaultMat = screen3.GetComponent<SpriteRenderer>().sharedMaterial;
        screen4DefaultMat = screen4.GetComponent<SpriteRenderer>().sharedMaterial;
        screen5DefaultMat = screen5.GetComponent<SpriteRenderer>().sharedMaterial;
        tsovinar = data.tsovinar;
        antennaHitBox = telescopingAntenna.GetComponent<CapsuleCollider2D>();
        
        base.Initialize(data);
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

        while(true)
        {
            yield return base.Execute(duration);
        }
    }

    protected override void OnEnd()
    {
        telescopingAntenna.gameObject.GetComponent<AntennaWatcher>().OnEnd();
        base.OnEnd();
        tsovinar.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
        tsovinar.transform.rotation = new Quaternion(-23f, -16f, -7f, 90f);
        tsovinar.transform.position = screen1.transform.position;
    }

    protected override void OnStart()
    {
        if(isLeft)
        {
            telescopingAntenna = leftAntenna;
        }
        else
        {
            telescopingAntenna = rightAntenna;
        }
        isLeft = !isLeft;
        
        spawnTransform = telescopingAntenna.GetComponent<AntennaWatcher>().spawnPosition;
        foreach (var subAttack in Attacks)
        {
            ((AntennaBolt)subAttack.Attack).spawnPosition = spawnTransform;
        }

        base.OnStart();
    }

    

    private void ScreenOff()
    {
        screen1.GetComponent<SpriteRenderer>().sharedMaterial = whiteNoise;
        screen2.GetComponent<SpriteRenderer>().sharedMaterial = whiteNoise;
        screen3.GetComponent<SpriteRenderer>().sharedMaterial = whiteNoise;
        screen4.GetComponent<SpriteRenderer>().sharedMaterial = whiteNoise;
        screen5.GetComponent<SpriteRenderer>().sharedMaterial = whiteNoise;
        tsovinar.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        tsovinar.SetActive(false);
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
        script.tsovinar = tsovinar;
        script.antennaHitBox = antennaHitBox;

    }
}
