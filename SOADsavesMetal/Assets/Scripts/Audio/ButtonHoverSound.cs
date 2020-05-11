using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverSound : MonoBehaviour
{
    public LevelLoad_Audio audio;

    public void OnPointerEnter(PointerEventData eventData)
    {
        audio.PlayMoveSound();
    }
}
