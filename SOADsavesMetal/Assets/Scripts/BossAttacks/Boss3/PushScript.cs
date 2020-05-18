using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushScript : MonoBehaviour
{
    bool Left;
    Rigidbody2D rb;
    [SerializeField] float leftPosition;
    [SerializeField] float rightPosition;
    [SerializeField] float speed = .05f;
    [SerializeField] float upPosition;
    [SerializeField] float bottomPosition;

    public bool isPushing;
    public bool isAboveGround;

    bool goUp;
    bool roaming;
    float startXScale;
    private Vector3 startPos;

    [FMODUnity.EventRef]
    public string pushHandEvent;
    // Start is called before the first frame update
    void Start()
    {
        Left = true;
        isPushing = false;
        roaming = false;
        startXScale = transform.localScale.x;
        startPos = transform.position;
    }

    public void Push(bool push, bool pushLeft = true)
    {        
        isPushing = push;
        Left = pushLeft;
        transform.localScale = new Vector3(startXScale * (Left ? -1 : 1), transform.localScale.y, 1);
    }

    public void Roam()
    {
        transform.position = startPos;
        isPushing = false;
        roaming = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (roaming || isPushing)
        {
            if (transform.position.x <= leftPosition)
            {
                Left = true;
                transform.localScale = new Vector3(-startXScale, transform.localScale.y, 1);
            }
            if (transform.position.x >= rightPosition)
            {
                Left = false;
                transform.localScale = new Vector3(startXScale, transform.localScale.y, 1);
            }
            if (!Left && transform.position.x > leftPosition)
            {
                transform.position = transform.position - new Vector3(speed, 0, 0);
            }
            else if (Left && transform.position.x < rightPosition)
            {
                transform.position = transform.position + new Vector3(speed, 0, 0);
            }
        }

        isAboveGround = false;

        if (((roaming && (!goUp || ShouldGoDown())) || !(roaming || isPushing)) && transform.position.y > bottomPosition)
        {
            transform.position = transform.position - new Vector3(0, speed, 0);
            isAboveGround = true;
        }
        else if (((roaming && goUp) || isPushing) && transform.position.y < upPosition)
        {
            transform.position = transform.position + new Vector3(0, speed, 0);
            isAboveGround = true;
        }
    }

    private bool ShouldGoDown()
    {
        return transform.position.x < -7 || transform.position.x > 4;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            goUp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            goUp = false;
        }
    }

    public void setSpeed(float input)
    {
        speed = input;
    }
}
