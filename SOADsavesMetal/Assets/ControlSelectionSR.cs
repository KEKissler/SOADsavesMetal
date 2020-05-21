using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class ControlSelectionSR : MonoBehaviour
{

    public Sprite arrowControls; 
    public Sprite wasdControls;
    public Sprite controllerControls;
    public SpriteRenderer currentControlImg;

    private int currentControls;

    // Start is called before the first frame update
    void Start()
    {
        currentControls = StaticData.controlScheme;
        currentControlImg = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentControls)
        {
            case 0:
                currentControlImg.sprite = arrowControls;
                break;
            case 1:
                currentControlImg.sprite = wasdControls;
                break;
            case 2:
                currentControlImg.sprite = controllerControls;
                break;
            default:
                currentControlImg.sprite = arrowControls;
                break;

        }
    }
}
