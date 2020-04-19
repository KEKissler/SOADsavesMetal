using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionImageController : MonoBehaviour
{
    public GameObject Player;
    public GameObject Boss;

    public Sprite [] players;
    public GameObject [] bosses;

    public Button FightButton;
    private bool PlayerSelected;
    private bool BossSelected;

    void Awake()
    {
        PlayerSelected = BossSelected = false;
        Time.timeScale = 1f;
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
                p = players[0];
                break;
            case 1:
                p = players[1];
                break;
            case 2:
                p = players[2];
                break;
            case 3:
                p = players[2];
                break;
            default:
                p = players[0];
                break;

        }
        Player.GetComponent<Image>().sprite = p;
    }

    public void SwitchBossSprites(int boss)
    {
        switch(boss)
        {
            case 0:
                bosses[0].SetActive(true);
                bosses[1].SetActive(false);
                bosses[2].SetActive(false);
                bosses[3].SetActive(false);
                break;
            case 1:
                bosses[0].SetActive(false);
                bosses[1].SetActive(true);
                bosses[2].SetActive(false);
                bosses[3].SetActive(false);
                break;
            case 2:
                bosses[0].SetActive(false);
                bosses[1].SetActive(false);
                bosses[2].SetActive(true);
                bosses[3].SetActive(false);
                break;
            case 3:
                bosses[0].SetActive(false);
                bosses[1].SetActive(false);
                bosses[2].SetActive(false);
                bosses[3].SetActive(true);
                break;
            default:
                bosses[0].SetActive(true);
                bosses[1].SetActive(false);
                bosses[2].SetActive(false);
                bosses[3].SetActive(false);
                break;

        }

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
