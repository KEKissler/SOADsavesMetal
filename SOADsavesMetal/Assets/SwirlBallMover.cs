using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwirlBallMover : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float time_to_destroy;
    [SerializeField]
    private bool Left; 
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if((Time.time - time) >= time_to_destroy)
        {
            Destroy(this.gameObject);
        }
        if(this.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            if(Left)
                this.gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * (-speed);
            else
                this.gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * (speed);
        }
    }
}
