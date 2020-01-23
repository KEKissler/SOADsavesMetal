using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayPause : MonoBehaviour
{
    public bool paused = false;
    public GameObject pauseMenu;
    private float timeScale; 

    private void Start()
    {
        timeScale = Time.timeScale;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            togglePause();
        }
    }
    public void togglePause()
    {
        if(paused)
        {
            Play();
        }
        else
        {
            Pause();
        }
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        paused = true;
        pauseMenu.SetActive(true);
    }

    private void Play()
    {
        Time.timeScale = timeScale;
        paused = false;
        pauseMenu.SetActive(false);
    }
}
