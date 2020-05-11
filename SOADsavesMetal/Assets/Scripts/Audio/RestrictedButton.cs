using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestrictedButton : MonoBehaviour
{
    public Button button;
    public LevelLoad_Audio audio;
    public void PlayErrorWithContext()
    {
        if (!button.interactable)
        {
            audio.PlayErrorSound();
        }
    }
}
