using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionImageController : MonoBehaviour
{
    public GameObject Player;
    public GameObject Boss;

    public Sprite s_John;
    public Sprite s_Shavo;
    public Sprite s_Daron;
    public Sprite s_Serj;

    public Sprite boss_Agas;
    public Sprite boss_Tsovinar;
    public Sprite boss_Nhang;
    public Sprite boss_Sandaramet;

    public Button FightButton;
    private bool PlayerSelected;
    private bool BossSelected;

    void Awake()
    {
        PlayerSelected = BossSelected = false;
    }
    private void Update()
    {

        if (PlayerSelected && BossSelected) {
            FightButton.interactable = true;
        }
        else
            FightButton.interactable = false;

    }
    public void SwitchPlayerSprites(int player) {
        Sprite p;

        switch (player) {
            case 0:
                p = s_John;
                break;
            case 1:
                p = s_Shavo;
                break;
            case 2:
                p = s_Daron;
                break;
            case 3:
                p = s_Serj;
                break;
            default:
                p = s_John;
                break;

        }
        Player.GetComponent<Image>().sprite = p;
    }

    public void SwitchBossSprites(int boss)
    {
        Sprite p;
        switch(boss)
        {
            case 0:
                p = boss_Agas;
                break;
            case 1:
                p = boss_Tsovinar;
                break;
            case 2:
                p = boss_Nhang;
                break;
            case 3:
                p = boss_Sandaramet;
                break;
            default:
                p = boss_Agas;
                break;

        }
        Boss.GetComponent<Image>().sprite = p;

    }


    public void ButtonPlayerClicked(){
        PlayerSelected = true;
        Player.SetActive(true);
    }
    public void ButtonBossClicked() {
        BossSelected = true;
        Boss.SetActive(true);
    }
}
