using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fading : MonoBehaviour
{

    public Image fadeImage;
    public float fadeStrength;
    public Animation fadeInAnim;
    public Animation fadeOutAnim;
    IEnumerator fadeIn()
    {
        float alpha = 0f;
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            Color fadeColor = fadeImage.color;
            fadeColor.a = alpha;

            while (alpha >= 0f)
            {
                fadeColor.a = alpha;
                fadeImage.color = fadeColor;
                alpha -= fadeStrength;
                yield return new WaitForSecondsRealtime(0.01f);
            }
        }
    }

    IEnumerator fadeOut()
    {
        float alpha = 0f;
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            Color fadeColor = fadeImage.color;
            fadeColor.a = alpha;

            while (alpha <= 1f)
            {
                fadeColor.a = alpha;
                fadeImage.color = fadeColor;
                alpha += fadeStrength;
                yield return new WaitForSecondsRealtime(0.01f);
            }
        }
    }
}
