using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TurnDialOnInteract : MonoBehaviour
{
    private static int SELECTIONOCUNT = 4;

    private Dictionary<int, Button> playerButtonMap, bossButtonMap, trackButtonMap;
    private Dictionary<int, string> playerMap, bossMap, trackMap;
    private int currentPlayer, currentBoss, currentTrack;
    private StaticData staticData;
    private Animator playerAnimator, bossAnimator; 
    private Animation playerKnob, bossKnob, trackKnob;
    private MusicSetter music;

    StageSelectData SSD;
    // Start is called before the first frame update
    void Start()
    {
        playerMap = new Dictionary<int, string>();
        bossMap = new Dictionary<int, string>();
        trackMap = new Dictionary<int, string>();

        playerButtonMap = new Dictionary<int, Button>();
        bossButtonMap = new Dictionary<int, Button>();
        trackButtonMap = new Dictionary<int, Button>();



        #region Selection Mapping
        playerMap.Add(0, "John");
        playerMap.Add(1, "Shavo");
        playerMap.Add(2, "Daron");
        playerMap.Add(3, "Serj");

        bossMap.Add(0, "Level1_Test");
        bossMap.Add(1, "Level3_Test");
        bossMap.Add(2, "Level2_Test");
        bossMap.Add(3, "Level4_Test");

        trackMap.Add(0, "Song 1");
        trackMap.Add(1, "Song 2");
        trackMap.Add(2, "Song 3");
        trackMap.Add(3, "Song 4");
        #endregion

        #region Button Mapping
        playerButtonMap.Add(0, SSD.john);
        playerButtonMap.Add(1, SSD.shavo);
        playerButtonMap.Add(2, SSD.daron);
        playerButtonMap.Add(3, SSD.serj);

        bossButtonMap.Add(0, SSD.agas);
        bossButtonMap.Add(1,SSD.nhang);
        bossButtonMap.Add(2,SSD.tsovi);
        bossButtonMap.Add(3,SSD.sandy);

        trackButtonMap.Add(0,SSD.byob);
        trackButtonMap.Add(1,SSD.hMts);
        trackButtonMap.Add(2,SSD.vPorno);
        trackButtonMap.Add(3,SSD.aerials);
        #endregion
    }

    // Update is called once per frame
    public void OnClickEvents()
    {
        SSD.SSPlayer.onClick.AddListener(SelectNextPlayer);
        SSD.SSBoss.onClick.AddListener(SelectNextBoss);
        SSD.SSTrack.onClick.AddListener(SelectNextTrack);       
    }

    public void SelectNextPlayer()
    {
        do
        {
            Debug.Log("Here");
            if (currentPlayer < 4) {
                ++currentPlayer;
                playerKnob.Play("KnobAnimation" + currentPlayer);

            }
            else
            {
                currentPlayer = 0;
                playerKnob.Play("KnobAnimation");
            }
        }
        while (false);
        staticData.setPlayer(playerMap[currentPlayer]);
        
    }

    void SelectNextBoss()
    {
        if (currentBoss < 4)
        {
            ++currentBoss;
            

        }
        else
        {
            currentBoss = 0;

        }
        staticData.setLevel(bossMap[currentPlayer]);
    }

    void SelectNextTrack()
    {
        if (currentTrack < 4)
        {
            ++currentTrack;
            
        }
        else
        {
            currentTrack = 0;

        }
        music.QueueBattleMusic(currentTrack);
    }
}
