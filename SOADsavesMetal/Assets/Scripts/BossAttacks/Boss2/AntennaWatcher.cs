using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntennaWatcher : MonoBehaviour
{
    private const string ANTENNA_FOLD = "antenna_fold";

    public AnimationClip foldClip;
    public Transform spawnPosition;
    public float hitTime;
    public WhiteNoiseAttack whiteNoise;

    [System.NonSerialized]
    public GameObject screen1;
    [System.NonSerialized]
    public GameObject screen2;
    [System.NonSerialized]
    public GameObject screen3;
    [System.NonSerialized]
    public GameObject screen4;
    [System.NonSerialized]
    public GameObject screen5;

    [System.NonSerialized]
    public Material screen1DefaultMat;
    [System.NonSerialized]
    public Material screen2DefaultMat;
    [System.NonSerialized]
    public Material screen3DefaultMat;
    [System.NonSerialized]
    public Material screen4DefaultMat;
    [System.NonSerialized]
    public Material screen5DefaultMat;

    [System.NonSerialized]
    public GameObject tsovinar;
    [System.NonSerialized]
    public CapsuleCollider2D antennaHitBox;

    private Animator telescopingAntenna;

    private void Start()
    {
        telescopingAntenna = GetComponent<Animator>();
    }

    public void hit()
    {
        whiteNoise.End();
        //OnEnd();
    }

    public void OnEnd()
    {
        //StartCoroutine(FoldAntenna());
        telescopingAntenna.Play(ANTENNA_FOLD);
    }

    IEnumerator FoldAntenna()
    {
        yield return new WaitForSeconds(foldClip.length);
        //telescopingAntenna.Play(ANTENNA_FOLD);
    }

    private void OnHit()
    {
        hit();
    }
}
