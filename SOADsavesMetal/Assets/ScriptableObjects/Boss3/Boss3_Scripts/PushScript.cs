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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(rb.position.x >= leftPosition)
        {
            Left = false;
        }else
        {
            Left = true;
        }
        if (Left && rb.position.y < leftPosition)
        {
            rb.position = rb.position + new Vector2(.1f,0);
            
        }
        else if (!Left && rb.position.x > rightPosition)
        {
            rb.position = rb.position - new Vector2( .1f,0);
        }
    }
}
