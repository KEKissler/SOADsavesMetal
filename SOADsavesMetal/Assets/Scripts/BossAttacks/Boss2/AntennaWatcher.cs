using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntennaWatcher : MonoBehaviour
{
    private const string ANTENNA_FOLD = "antenna_fold";

    public AnimationClip foldClip;
    public Transform spawnPosition;
    public int maxHits;
    public float hitTime;

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
    public SpriteRenderer faceVisable;
    [System.NonSerialized]
    public CapsuleCollider2D tsovinarHitBox;
    [System.NonSerialized]
    public CapsuleCollider2D antennaHitBox;

    private Animator telescopingAntenna;

    private int hitCount = 0;
    private float hitDelay = 0;

    private void Start()
    {
        telescopingAntenna = GetComponent<Animator>();
    }

    private void Update()
    {
        hitDelay += Time.deltaTime;
    }

    public void hit()
    {
        Debug.Log(hitCount);
        if (hitDelay <= hitTime)
        {
            return;
        }

        ++hitCount;
        hitDelay = 0;
        OnHit();

        if (hitCount == maxHits)
        {
            StartCoroutine(FoldAntenna());
            ScreenOn();
            hitCount = 0;
        }


    }

    IEnumerator FoldAntenna()
    {
        yield return new WaitForSeconds(foldClip.length);
        telescopingAntenna.Play(ANTENNA_FOLD);
    }

    private void ScreenOn()
    {
        screen1.GetComponent<SpriteRenderer>().sharedMaterial = screen1DefaultMat;
        screen2.GetComponent<SpriteRenderer>().sharedMaterial = screen2DefaultMat;
        screen3.GetComponent<SpriteRenderer>().sharedMaterial = screen3DefaultMat;
        screen4.GetComponent<SpriteRenderer>().sharedMaterial = screen4DefaultMat;
        screen5.GetComponent<SpriteRenderer>().sharedMaterial = screen5DefaultMat;
        faceVisable.enabled = true;
        tsovinarHitBox.enabled = true;
        antennaHitBox.enabled = false;
    }

    private void OnHit()
    {
        hit();
    }
}
