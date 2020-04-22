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
        deathEffect = FMODUnity.RuntimeManager.CreateInstance("snapshot:/DeathEffect");
        deathEffect.setParameterByName("DeathIntensity", 0);
        deathEffect.start();
    }
    private void OnEnable()
    {
        deathEffect.setParameterByName("PauseMuteIntensity", 0);
        deathEffect.start();
    }
    private void OnDisable()
    {
        deathEffect.setParameterByName("PauseMuteIntensity", 0);
        deathEffect.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public bool getPaused()
    {
        return paused;
    }

    private void Awake()
    {
        Time.timeScale = 1f;
    }
    private void Update()
    {
        if (player.Health == 0)
        {
            deathMenuPop();
        }
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
        deathEffect.setParameterByName("PauseMuteIntensity", 1);
        FMOD.Studio.EventInstance pauseSoundInstance = FMODUnity.RuntimeManager.CreateInstance(pauseSound);
        pauseSoundInstance.start();
    }
}
