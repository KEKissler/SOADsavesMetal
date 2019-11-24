using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBeamAnimation : MonoBehaviour
{ 
    private static float X_LENGTH = 60;

    public float preYLength;
    public float yAttackSize;
    public float chargeDuration;
    public float attackDuration;
    public float flashTime;
    public float endDuration;
    public Color startingColor;
    public Color attackColor;
    public Transform player;

    private float postYLength = 0;
    private SpriteRenderer alpha;
    private bool tracking = true;

    // Start is called before the first frame update
    void Start()
    {
        alpha = GetComponent<SpriteRenderer>();
        transform.localScale = new Vector3(X_LENGTH, preYLength, 1);
        alpha.color = startingColor;
        StartCoroutine(Coalesce());
        
    }

    // Update is called once per frame
    void Update()
    {
        if(tracking)
        {
            float angle = Mathf.Rad2Deg * Mathf.Atan2(transform.position.y - player.position.y, transform.position.x - player.position.x);
            Quaternion newRotate = new Quaternion();
            newRotate.eulerAngles = new Vector3(0, 0, angle);
            transform.rotation = newRotate;
            Debug.Log(angle);
        }
        
    }

    private IEnumerator Coalesce()
    {
        transform.LeanScaleY(yAttackSize, chargeDuration);
        var a = StartCoroutine(LerpColor(alpha, startingColor, attackColor, chargeDuration));

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

    private IEnumerator Warning()
    {
        alpha.color = startingColor;
        yield return new WaitForSeconds(flashTime);
        alpha.color = attackColor;
        StartCoroutine(Firing());
    }

    private IEnumerator Firing()
    {
        yield return new WaitForSeconds(attackDuration);
        StartCoroutine(Shrink());
    }

    private IEnumerator Shrink()
    {
        transform.LeanScaleY(0, endDuration);
        yield return new WaitForSeconds(endDuration);
    }
}
