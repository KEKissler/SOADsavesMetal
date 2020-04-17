using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class ButtonHoverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public GameObject left_eye;
    public GameObject right_eye;

    public void OnPointerEnter(PointerEventData eventData)
    {
        left_eye.SetActive(true);
        right_eye.SetActive(true);
        left_eye.transform.position = new Vector3(left_eye.transform.position.x, transform.position.y+0.5f, 0);
        right_eye.transform.position = new Vector3(right_eye.transform.position.x, transform.position.y+0.5f, 0);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //if (eventData.pointerCurrentRaycast.gameObject.name == "Button") { return;}
        left_eye.SetActive(false);
        right_eye.SetActive(false);
    }

}
