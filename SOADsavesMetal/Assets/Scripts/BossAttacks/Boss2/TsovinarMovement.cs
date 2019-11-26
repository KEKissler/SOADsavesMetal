using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/Tsovinar Movement")]
public class TsovinarMovement : TsovinarAttack
{ 
    public Vector3 bossSizeScreen1;
    public Vector3 bossSizeScreen2;
    public Vector3 bossSizeScreen3;
    public Vector3 bossSizeScreen4;
    public Vector3 bossSizeScreen5;
    public Quaternion bossRotationScreen1;
    public Quaternion bossRotationAllOtherScreens;
    public Color visibleColor;
    public Color fadeColor;
    public float fadeDuration;

    private GameObject tsovinar;
    private SpriteRenderer alpha;
    private Transform screen1;
    private Transform screen2;
    private Transform screen3;
    private Transform screen4;
    private Transform screen5;

    public override void Initialize(TsovinarAttackData data)
    {
        screen1 = data.screen1.transform;
        screen2 = data.screen2.transform;
        screen3 = data.screen3.transform;
        screen4 = data.screen4.transform;
        screen5 = data.screen5.transform;
        tsovinar = data.tsovinar;
      

    }

    protected override IEnumerator Execute(float duration)
    {
        //Fade out
        CoroutineRunner.instance.StartCoroutine(LerpFade(alpha, visibleColor, fadeColor, fadeDuration));
        yield return new WaitForSeconds(fadeDuration);
        ChangeScreen();

        //Fade in
        CoroutineRunner.instance.StartCoroutine(LerpFade(alpha, fadeColor, visibleColor, fadeDuration));
        yield return new WaitForSeconds(fadeDuration);

    }

    protected override void OnEnd()
    {
        //Do nothing
    }

    protected override void OnStart()
    {
        alpha = tsovinar.GetComponent<SpriteRenderer>();
        //tsovinar.transform.position = screen1.position;
        Debug.Log(tsovinar.name);
    }

    private void ChangeScreen()
    {
        int randNum;
        randNum = Random.Range(0, 5);

        switch (randNum)
        {
            case 0:
                tsovinar.transform.localScale = bossSizeScreen1;
                tsovinar.transform.rotation = bossRotationScreen1;
                tsovinar.transform.position = screen1.position;
                break;

            case 1:
                tsovinar.transform.localScale = bossSizeScreen2;
                tsovinar.transform.rotation = bossRotationAllOtherScreens;
                tsovinar.transform.position = screen2.position;
                break;

            case 2:
                tsovinar.transform.localScale = bossSizeScreen3;
                tsovinar.transform.rotation = bossRotationAllOtherScreens;
                tsovinar.transform.position = screen3.position;
                break;

            case 3:
                tsovinar.transform.localScale = bossSizeScreen4;
                tsovinar.transform.rotation = bossRotationAllOtherScreens;
                tsovinar.transform.position = screen4.position;
                break;

            case 4:
                tsovinar.transform.localScale = bossSizeScreen5;
                tsovinar.transform.rotation = bossRotationAllOtherScreens;
                tsovinar.transform.position = screen5.position;
                break;

            default:
                break;
        }
    }

    //Handles both fade in and out lerp currently
    private IEnumerator LerpFade(SpriteRenderer alpha, Color from, Color to, float time)
    {
        float duration = 0;
        while (duration < time)
        {
            alpha.color = Color.Lerp(from, to, duration / time);
            duration += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        alpha.color = to;
        
    }
}
