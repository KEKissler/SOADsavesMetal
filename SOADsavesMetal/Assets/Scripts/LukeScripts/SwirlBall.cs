using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwirlBall : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float speed = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.GetComponent<Rigidbody2D>() != null)
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, -6 * speed * Time.deltaTime);
    }
}
