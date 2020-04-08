using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushScript : MonoBehaviour
{
    bool Left;
    Rigidbody2D rb;
    [SerializeField]
    float leftPosition;
    [SerializeField]
    float rightPosition;
    [SerializeField]
    float speed = .05f;
    [SerializeField]
    float upPosition;
    [SerializeField]
    float bottomPosition;
    bool isPushing;
    bool goUp;
    bool Up;
    // Start is called before the first frame update
    void Start()
    {
        Left = true;
        isPushing = false;
    }

    public void Push()
    {
        
        isPushing = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isPushing)
        {
            if (transform.position.x <= leftPosition)
            {
                Left = true;
            }
            if (transform.position.x >= rightPosition)
            {
                Left = false;
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

        
        
        if (!goUp && transform.position.y > bottomPosition)
        {
            transform.position = transform.position - new Vector3(0, speed, 0);
        }
        else if (goUp && transform.position.y < upPosition)
        {
            transform.position = transform.position + new Vector3(0, speed, 0);
        }
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
