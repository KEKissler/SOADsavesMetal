using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoSave : MonoBehaviour
{
    [SerializeField] private int bossNumber; //Agas = 1, Tsovinar = 2, Nhang = 3, Sandaramet = 4
    private string playerName;
    private BossHealth bossHealth;
    private bool saveOnce = false;
    public Image fadeImage;
    public float fadeOutStrength = 0.01f;
    void Start()
    {
        playerName = FindObjectOfType<Player>().currentBandMember;
        bossHealth = FindObjectOfType<BossHealth>();
    }
    void Update()
    {
        if(bossHealth != null)
        {
            if(bossHealth.isDead())
            {
                if(!saveOnce)
                {
                    saveOnce = true;
                    switch(playerName)
                    {
                        case "John":
                        {
                            switch(bossNumber)
                            {
                                case 1:
                                {
                                    //Shavo 0
                                    StaticData.shavoUnlock[0] = true;
                                    break;
                                }
                                case 2:
                                {
                                    //Serj 0
                                    StaticData.serjUnlock[0] = true;
                                    break;
                                }
                                case 3:
                                {
                                    //Shavo 1
                                    StaticData.shavoUnlock[1] = true;
                                    break;
                                }
                                case 4:
                                {
                                    break;
                                }
                            }
                            break;
                        }
                        case "Shavo":
                        {
                            switch(bossNumber)
                            {
                                case 1:
                                {
                                    //Daron 0
                                    StaticData.daronUnlock[0] = true;
                                    break;
                                }
                                case 2:
                                {
                                    //Daron 1
                                    StaticData.daronUnlock[1] = true;
                                    break;
                                }
                                case 3:
                                {
                                    //Serj 1
                                    StaticData.serjUnlock[1] = true;
                                    break;
                                }
                                case 4:
                                {
                                    break;
                                }
                            }
                            break;
                        }
                        case "Daron":
                        {
                            switch(bossNumber)
                            {
                                case 1:
                                {
                                    //Serj 2
                                    StaticData.serjUnlock[2] = true;
                                    break;
                                }
                                case 2:
                                {
                                    break;
                                }
                                case 3:
                                {
                                    break;
                                }
                                case 4:
                                {
                                    break;
                                }
                            }
                            break;
                        }
                        case "Serj":
                        {
                            switch(bossNumber)
                            {
                                case 1:
                                {
                                    break;
                                }
                                case 2:
                                {
                                    break;
                                }
                                case 3:
                                {
                                    break;
                                }
                                case 4:
                                {
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    //StartCoroutine("loadNextScene");
                }
            }
        }
    }
    private IEnumerator loadNextScene(){
        float alpha = 0f;
        if(fadeImage != null){
            fadeImage.gameObject.SetActive(true);
            Color fadeColor = fadeImage.color;
            fadeColor.a = alpha;

            while(alpha <= 1f){
                fadeColor.a = alpha;
                fadeImage.color = fadeColor;
                alpha += fadeOutStrength;
                yield return new WaitForSecondsRealtime(0.01f);
            }
        }
        //load next scene
    }
}

