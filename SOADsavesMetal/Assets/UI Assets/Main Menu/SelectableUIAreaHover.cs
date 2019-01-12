using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectableUIAreaHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public GameObject left_eye;
    public GameObject right_eye;

    public void OnPointerEnter(PointerEventData eventData)
    {
        left_eye.SetActive(true);
        right_eye.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //if (eventData.pointerCurrentRaycast.gameObject.name == "Button") { return;}
        left_eye.SetActive(false);
        right_eye.SetActive(false);
    }
}
