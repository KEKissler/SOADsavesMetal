using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameSphereManager : MonoBehaviour
{
    public Collider2D top;
    public Collider2D bottom;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == top && rb.velocity.y > 0 || collision == bottom && rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
        }
    }
}
