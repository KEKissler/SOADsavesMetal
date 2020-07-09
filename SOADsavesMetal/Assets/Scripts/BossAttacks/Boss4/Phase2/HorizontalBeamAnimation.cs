using FMOD.Studio;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalBeamAnimation : MonoBehaviour
{ 
    private static float X_LENGTH = 60;

    public float preYLength;
    public float yAttackSize;
    public float chargeDuration;
    public float attackDuration;
    public float flashDuration;
    public float endDuration;
    public float trackingSpeed;
    public Color startingColor;
    public Color attackColor;
    public Color flashColor;
    public Transform player;
    public GameplayPause gameplayPause;

    private BoxCollider2D beamHitBox;
    private float postYLength = 0;
    private SpriteRenderer alpha;
    private bool tracking = true;

    [FMODUnity.EventRef]
    public string chargeEvent;
    [FMODUnity.EventRef]
    public string fireEvent;

    // Start is called before the first frame update
    void Start()
    {
        alpha = GetComponentInChildren<SpriteRenderer>();
        alpha.transform.localScale = new Vector3(X_LENGTH, preYLength, 1);
        alpha.transform.position += new Vector3(-X_LENGTH / 2, 0, 0);
        alpha.color = startingColor;
        beamHitBox = GetComponentInChildren<BoxCollider2D>();
        beamHitBox.enabled = false;
        StartCoroutine(Coalesce());
        gameplayPause = FindObjectOfType<GameplayPause>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameplayPause.getPaused())
        {
            if (tracking)
            {
                transform.position += new Vector3(0, Mathf.Sign(player.position.y - transform.position.y), 0) * trackingSpeed;
            }
        }
        
    }

    private IEnumerator Coalesce()
    {
        EventInstance chargeInstance = FMODUnity.RuntimeManager.CreateInstance(chargeEvent);
        chargeInstance.start();
        alpha.transform.LeanScaleY(yAttackSize, chargeDuration);
        var a = StartCoroutine(LerpColor(alpha, startingColor, flashColor, chargeDuration));

        yield return new WaitForSeconds(chargeDuration);
        tracking = false;
        if (a != null)
        {
            StopCoroutine(a);
        }
        StartCoroutine(Warning());
    }

    private IEnumerator LerpColor(SpriteRenderer sprite, Color from, Color to, float time)
    {
        float duration = 0;
        while (duration < time)
        {
            sprite.color = Color.Lerp(from, to, duration/time);
            duration += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }     
        sprite.color = to;
    }

    private IEnumerator LerpColorWarn(SpriteRenderer sprite, Color from, Color to, float time)
    {
        float duration = 0;
        while (duration < time)
        {
            sprite.color = Color.Lerp(from, to, duration / time);
            duration += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        sprite.color = to;
    }

    private IEnumerator Warning()
    {
        StartCoroutine(LerpColorWarn(alpha, flashColor, attackColor, flashDuration));
        //alpha.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        //alpha.color = attackColor;
        StartCoroutine(Firing());
    }

    private IEnumerator Firing()
    {
        EventInstance fireInstance = FMODUnity.RuntimeManager.CreateInstance(fireEvent);
        fireInstance.start();
        beamHitBox.enabled = true;
        yield return new WaitForSeconds(attackDuration);
        beamHitBox.enabled = false;
        StartCoroutine(Shrink());
    }

    private IEnumerator Shrink()
    {
        alpha.transform.LeanScaleY(0, endDuration);
        yield return new WaitForSeconds(endDuration);
        Destroy(transform.parent.gameObject);
    }
}
