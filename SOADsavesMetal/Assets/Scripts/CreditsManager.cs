using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    public GameObject creditsArea;
    public TextAsset creditsfile;
    public float Timer = 3.0f;
    public int MaxTextSection = 8;
    public GameObject GameLogo;
    
    private TMPro.TextMeshProUGUI creditsText;
    private int currline; //0-7 credit sections
    private bool isStarted;
    private string[] txt;
    
    // Start is called before the first frame update
    void Awake()
    {
        creditsText = creditsArea.GetComponent<TMPro.TextMeshProUGUI>();
        txt = creditsfile.text.Split('|'); //include | between credit sections as main separator!
        isStarted = false;
        currline = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStarted) {
            isStarted = true;
            StartCoroutine("waitTime");
        }
    }

    IEnumerator waitTime() {
        StartCoroutine(FadeTo(0.1f, 1.0f, 1.0f));
        StartCoroutine(FadeTo(4.0f, 0.0f, 1.0f));
        yield return new WaitForSeconds(Timer);
        StartCoroutine(FadeTo(0.0f, 1.0f, 1.0f));
        while (currline < MaxTextSection)
        {
            
            StartCoroutine(FadeTo(4.0f, 0.0f, 1.0f));
            creditsText.text = txt[currline++];
            yield return new WaitForSeconds(Timer);
            StartCoroutine(FadeTo(0.0f, 1.0f, 1.0f));

        }
        StartCoroutine(FadeTo(6.0f, 0.0f, 1.0f));
        creditsText.text = txt[currline++];
        yield return new WaitForSeconds(Timer+2.0f);
        //creditsText.text = "";
        StartCoroutine(FadeLogo(1.0f, 1.0f, 3.0f));
        StartCoroutine(FadeLogo(7.0f, 0.0f, 1.0f));
        yield return new WaitForSeconds(Timer+3.0f);
        SceneManager.LoadScene("Main Menu");
    }

    IEnumerator FadeTo(float starttime, float val, float durTime) {
        yield return new WaitForSeconds(starttime);
        float alpha = creditsText.color.a;
        for (float i = 0.0f; i < 1.0f; i += Time.deltaTime / durTime) {
            Color newCol = new Color(creditsText.color.r, creditsText.color.g, creditsText.color.b, 
                Mathf.Lerp(alpha, val, i));
            creditsText.color = newCol;
            yield return null;
        }
    }

    IEnumerator FadeLogo(float starttime, float val, float durTime)
    {
        yield return new WaitForSeconds(starttime);
        Color orig = GameLogo.GetComponent<Image>().color;
        float alpha = orig.a;
        for (float i = 0.0f; i < 1.0f; i += Time.deltaTime / durTime)
        {
            Color newCol = new Color(orig.r, orig.g, orig.b,
                Mathf.Lerp(alpha, val, i));
            GameLogo.GetComponent<Image>().color = newCol;
            yield return null;
        }
    }
}
