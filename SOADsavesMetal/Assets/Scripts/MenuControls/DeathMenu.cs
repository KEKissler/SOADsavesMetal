using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    private bool paused = false;
    private float currentScale;
    public GameObject deathMenu;
    public Player player;
    public BossHealth bossHealth;
    public Text deathTipBox;

    public GameObject pauseFirstButton, controlsFirstButton, controlsClosedButton;

    FMOD.Studio.EventInstance deathEffect;
    [FMODUnity.EventRef]
    public string deathSong;
    [FMODUnity.EventRef]
    public string buttonClick;

    private void Start()
    {
        deathMenu.SetActive(false);
        if (deathSong != null)
        {
            deathEffect = FMODUnity.RuntimeManager.CreateInstance(deathSong);
            deathEffect.setParameterByName("DeathIntensity", 0);
            //deathEffect.start();
        }
    }

    private void OnEnable()
    {
        if (deathEffect.isValid())
        {
            deathEffect.setParameterByName("PauseMuteIntensity", 0);
            deathEffect.start();
        }
    }

    private void OnDisable()
    {
        if (deathEffect.isValid())
        {
            deathEffect.setParameterByName("PauseMuteIntensity", 0);
            deathEffect.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    public bool getPaused()
    {
        return paused;
    }

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    public void deathMenuPop()
    {
        Pause();
    }

    private void Pause()
    {
        currentScale = Time.timeScale;
        Time.timeScale = 0f;
        paused = true;

        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //Set a new selected event object
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);

        deathMenu.SetActive(true);
        if (deathEffect.isValid())
        {
            deathEffect.setParameterByName("PauseMuteIntensity", 1);
        }
        
        string deathTip = "Tip: ";
        switch(bossHealth.currentBoss)
        {
            case 1:
                deathTip += DeathTipGenerator.GenerateDeathTip(DeathTipGenerator.Boss.Agas, player.currentBandMember, bossHealth.agasAttackManager.phaseIndex);
                break;
            case 2:
                deathTip += DeathTipGenerator.GenerateDeathTip(DeathTipGenerator.Boss.Nhang, player.currentBandMember, bossHealth.nhangAttackManager.phaseIndex);
                break;
            case 3:
                deathTip += DeathTipGenerator.GenerateDeathTip(DeathTipGenerator.Boss.Tsovinar, player.currentBandMember, bossHealth.tsovinarAttackManager.phaseIndex);
                break;
            case 4:
                deathTip += DeathTipGenerator.GenerateDeathTip(DeathTipGenerator.Boss.Sandaramet, player.currentBandMember, bossHealth.sandarametAttackManager.phaseIndex);
                break;
        }
        deathTipBox.text = deathTip;
    }

    public void UnPause()
    {
        Time.timeScale = currentScale;
        paused = false;
        deathMenu.SetActive(false);
        
    }
}
