using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayPause : MonoBehaviour
{
    private bool paused = false;
    private float currentScale;
    public GameObject pauseMenu;
    public GameObject controlsScreen;
    public GameObject optionsScreen;
    public CountDown countDown;
    public Player player;

    FMOD.Studio.EventInstance pauseEffect;
    [FMODUnity.EventRef]
    public string pauseSound;
    [FMODUnity.EventRef]
    public string resumeSound;

    private void Start()
    {
        pauseMenu.SetActive(false);
        pauseEffect = FMODUnity.RuntimeManager.CreateInstance("snapshot:/PauseEffect");
        pauseEffect.setParameterByName("PauseMuteIntensity", 0);
        pauseEffect.start();
    }
    private void OnEnable()
    {
        pauseEffect.setParameterByName("PauseMuteIntensity", 0);
        pauseEffect.start();
    }
    private void OnDisable()
    {
        pauseEffect.setParameterByName("PauseMuteIntensity", 0);
        pauseEffect.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
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
        if(Input.GetKeyDown(player.GetPause()))
        {
            togglePause();
        }
    }
    public void togglePause()
    {
        if ((countDown == null || !countDown.getCountDown()) && !player.isDead && !countDown.isFading())
        {
            if (paused)
            {
                Play();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        currentScale = Time.timeScale;
        Time.timeScale = 0f;
        paused = true;
        pauseMenu.SetActive(true);
        pauseEffect.setParameterByName("PauseMuteIntensity", 1);
        FMOD.Studio.EventInstance pauseSoundInstance = FMODUnity.RuntimeManager.CreateInstance(pauseSound);
        pauseSoundInstance.start();
    }

    private void Play()
    {
        Time.timeScale = currentScale;
        paused = false;
        pauseMenu.SetActive(false);
        controlsScreen.SetActive(false);
        optionsScreen.SetActive(false);
        pauseEffect.setParameterByName("PauseMuteIntensity", 0);
        FMOD.Studio.EventInstance resumeSoundInstance = FMODUnity.RuntimeManager.CreateInstance(resumeSound);
        resumeSoundInstance.start();
    }
}
