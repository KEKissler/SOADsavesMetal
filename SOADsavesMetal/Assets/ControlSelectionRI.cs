using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class ControlSelectionRI : MonoBehaviour
{

    public Texture arrowControls; 
    public Texture wasdControls;
    public Texture controllerControls;
    public RawImage currentControlImg;

    private int currentControls;

    // Start is called before the first frame update
    void Start()
    {
        currentControls = StaticData.controlScheme;
        currentControlImg = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentControls)
        {
            case 0:
                currentControlImg.texture = arrowControls;
                break;
            case 1:
                currentControlImg.texture = wasdControls;
                break;
            case 2:
                currentControlImg.texture = controllerControls;
                break;
            default:
                currentControlImg.texture = arrowControls;
                break;

        }
    }
}
