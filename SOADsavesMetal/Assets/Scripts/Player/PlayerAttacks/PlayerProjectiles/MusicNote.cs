using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicNote : PlayerProjectile
{
    public float playerRotationY;
    public float oscillationPeriod = 2f;
    public float startTime = 0f;
    private float accumulatedTime;
    private float startY;

    // Start is called before the first frame update
    void Start()
    {
        speed = 13.37f;
        rb = GetComponent<Rigidbody2D>();
        startY = transform.position.y;
        accumulatedTime = startTime;

        float angle = Mathf.Deg2Rad * playerRotationY * -180f;
        rb.velocity = new Vector2(speed * Mathf.Cos(angle), speed * Mathf.Sin(angle));

        DestroyAfter(26f / speed);
    }

    void Update()
    {
        transform.position = new Vector2(transform.position.x, startY +
            0.7f * Mathf.Sin((8f * accumulatedTime - 0.5f) / oscillationPeriod));
        accumulatedTime += Time.deltaTime;
    }
}
