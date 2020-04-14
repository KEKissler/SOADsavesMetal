﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/White Noise Attack")]
public class WhiteNoiseAttack : TsovinarAttackSequence
{
    private const string ANTENNA_UNFOLD = "antenna_unfold";
    private const string ANTENNA_BLINK = "antenna_blinkrapid";
    private const string ANTENNA_FOLD = "antenna_fold";
    
    protected bool isLeft;

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

    private FMOD.Studio.EventInstance instance;
    
    protected AntennaWatcher antennaWatcherScript;
    protected static int antennaCount;


    public override void Initialize(TsovinarAttackData data)
    {
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Enemy/E_Tsovinar/E_Tsov_StaticLoop");
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
        instance.start();
        if (telescopingAntenna.GetCurrentAnimatorStateInfo(0).IsName(ANTENNA_BLINK))
        {
            yield break;
        }
        yield return new WaitForEndOfFrame();

        ScreenOff();
        telescopingAntenna.Play(ANTENNA_UNFOLD);
        
        yield return new WaitForSeconds(unfoldClip.length);

        while(!endEarly)
        {
            yield return base.Execute(duration);
        }
    }

    protected override void OnEnd()
    {
        End();
    }

    public void End()
    {
        if(!endEarly)
        {
            antennaWatcherScript.OnEnd();
            base.OnEnd();
            tsovinar.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
            tsovinar.transform.rotation = new Quaternion(-23f, -16f, -7f, 90f);
            tsovinar.transform.position = screen1.transform.position;
            instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            endEarly = true;
            --antennaCount;
        }

        if(antennaCount == 0)
            ScreenOn();
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

        antennaWatcherScript = telescopingAntenna.GetComponent<AntennaWatcher>();
        antennaWatcherScript.whiteNoise = this;
        endEarly = false;
        antennaCount = 1;

        spawnTransform = antennaWatcherScript.spawnPosition;
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

        antennaWatcherScript.screen1DefaultMat = screen1DefaultMat;
        antennaWatcherScript.screen2DefaultMat = screen2DefaultMat;
        antennaWatcherScript.screen3DefaultMat = screen3DefaultMat;
        antennaWatcherScript.screen4DefaultMat = screen4DefaultMat;
        antennaWatcherScript.screen5DefaultMat = screen5DefaultMat;
        antennaWatcherScript.screen1 = screen1;
        antennaWatcherScript.screen2 = screen2;
        antennaWatcherScript.screen3 = screen3;
        antennaWatcherScript.screen4 = screen4;
        antennaWatcherScript.screen5 = screen5;
        antennaWatcherScript.tsovinar = tsovinar;
        antennaWatcherScript.antennaHitBox = antennaHitBox;

    }

    public void ScreenOn()
    {
        screen1.GetComponent<SpriteRenderer>().sharedMaterial = screen1DefaultMat;
        screen2.GetComponent<SpriteRenderer>().sharedMaterial = screen2DefaultMat;
        screen3.GetComponent<SpriteRenderer>().sharedMaterial = screen3DefaultMat;
        screen4.GetComponent<SpriteRenderer>().sharedMaterial = screen4DefaultMat;
        screen5.GetComponent<SpriteRenderer>().sharedMaterial = screen5DefaultMat;
        tsovinar.SetActive(true);
        antennaHitBox.enabled = false;
    }
}
