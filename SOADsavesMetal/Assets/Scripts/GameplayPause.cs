﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayPause : MonoBehaviour
{
    private bool paused = false;
    private float currentScale;
    public GameObject pauseMenu;
    public GameObject controlsScreen;
    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    private void Awake()
    {
        Time.timeScale = 1f;
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
        currentScale = Time.timeScale;
        Time.timeScale = 0f;
        paused = true;
        pauseMenu.SetActive(true);
    }

    private void Play()
    {
        Time.timeScale = currentScale;
        paused = false;
        pauseMenu.SetActive(false);
        controlsScreen.SetActive(false);
    }
}
