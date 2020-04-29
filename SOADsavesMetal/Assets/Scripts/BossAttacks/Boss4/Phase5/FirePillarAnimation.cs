using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePillarAnimation : MonoBehaviour
{ 
    private static float Y_LENGTH = 20;

    public float preXLength;
    public float xAttackSize;
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
    public ParticleSystem smoke;
    public ParticleSystem fire;

    private BoxCollider2D beamHitBox;
    private float postYLength = 0;
    private SpriteRenderer alpha;
    private bool tracking = true;

    // Start is called before the first frame update
    void Start()
    {
        alpha = GetComponentInChildren<SpriteRenderer>();
        alpha.transform.parent.localScale = new Vector3(preXLength, Y_LENGTH, 1);
        alpha.color = startingColor;
        beamHitBox = GetComponentInChildren<BoxCollider2D>();
        beamHitBox.enabled = false;
        StartCoroutine(Coalesce());
        gameplayPause = FindObjectOfType<GameplayPause>();
        fire.gameObject.SetActive(false);
    }

    private IEnumerator Coalesce()
    {
        alpha.transform.parent.LeanScaleX(xAttackSize, chargeDuration);
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
        beamHitBox.enabled = true;
        fire.gameObject.SetActive(true);
        var particles = new ParticleSystem.Particle[100];
        smoke.GetParticles(particles);
        for(int i = 0; i < particles.Length; ++i)
        {
            if (particles[i].Equals(null))
                break;
            particles[i].startColor = fire.main.startColor.colorMax;
            particles[i].rotation = 0;
            particles[i].velocity *= 1.5f;
            particles[i].startSize *= 2;
        }

        fire.SetParticles(particles);
        smoke.gameObject.SetActive(false);
        yield return new WaitForSeconds(attackDuration);
        beamHitBox.enabled = false;
        StartCoroutine(Shrink());
    }

    private IEnumerator Shrink()
    {
        alpha.transform.parent.LeanScaleX(0, endDuration);

        fire.transform.parent = null;
        var m = fire.main;
        Destroy(fire, m.startLifetime.constant + m.duration);
        m.loop = false;
        m.startLifetime = 0.25f;
        var particles = new ParticleSystem.Particle[100];
        for (int i = 0; i < particles.Length; ++i)
        {
            if (particles[i].Equals(null))
                break;
            particles[i].remainingLifetime /= 4f;
        }

        yield return new WaitForSeconds(endDuration);
        Destroy(transform.parent.gameObject);
    }
}
