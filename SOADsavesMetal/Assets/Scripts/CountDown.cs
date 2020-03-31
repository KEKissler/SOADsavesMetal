using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public Text text;
    private bool countDownRunning = false;
    void Start()
    {
        StartCoroutine("countDown");
    }

    public bool getCountDown()
    {
        return countDownRunning;
    }

    public IEnumerator countDown()
    {
        countDownRunning = true;
        yield return new WaitForSecondsRealtime(.1f);
        Time.timeScale = 0f;
        text.text = "3";
        yield return new WaitForSecondsRealtime(.75f);
        text.text = "2";
        yield return new WaitForSecondsRealtime(.75f);
        text.text = "1";
        yield return new WaitForSecondsRealtime(.75f);
        text.text = "Fight!";
        yield return new WaitForSecondsRealtime(.75f);
        text.text = "";
        countDownRunning = false;
        Time.timeScale = 1f;
        text.transform.parent.gameObject.SetActive(false);
        
    }
}
