using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    private bool paused = false;
    private float currentScale;
    public GameObject deathMenu;
    public Player player;

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
        deathMenu.SetActive(true);
        if (deathEffect.isValid())
        {
            deathEffect.setParameterByName("PauseMuteIntensity", 1);
        }
    }

    public void UnPause()
    {
        Time.timeScale = currentScale;
        paused = false;
        deathMenu.SetActive(false);
        
    }
}
