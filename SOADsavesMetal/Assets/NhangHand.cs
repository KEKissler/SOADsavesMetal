using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NhangHand : MonoBehaviour
{
    Rigidbody2D rb;
    bool Up;

    [SerializeField]
    float TopPosition;
    [SerializeField]
    bool UseCurrentPosForBottom;
    [SerializeField]
    float BottomPosition;
    [SerializeField]
    float Duration;
    float time;
    // Start is called before the first frame update
    [FMODUnity.EventRef]
    public string groundShake;

    FMOD.Studio.EventInstance instance;
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(groundShake);
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        if (UseCurrentPosForBottom)
            BottomPosition = rb.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Up && rb.position.y < TopPosition)
        {
            rb.position = rb.position + new Vector2(0,.1f);
        }else if(!Up && rb.position.y > BottomPosition)
        {
            rb.position = rb.position - new Vector2(0, .1f);
        }
    }

    private void Update()
    {
        
        if (Up && (Time.time - time) >= Duration)
        {
            Up = false;
        }
    }
    public bool isUp()
    {
        return Up;
    }

    public void moveUp()
    {
        if (!Up)
        {
            instance.start();
        }
        Up = true;
        time = Time.time;
    }
}
