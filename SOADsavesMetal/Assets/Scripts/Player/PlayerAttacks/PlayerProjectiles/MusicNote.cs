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
        damage = 40;
        speed = 6.5f;
        rb = GetComponent<Rigidbody2D>();
        startY = transform.position.y;
        accumulatedTime = startTime;

        float angle = Mathf.Deg2Rad * playerRotationY * -180f;
        rb.velocity = new Vector2(speed * Mathf.Cos(angle), speed * Mathf.Sin(angle));

        DestroyWhenOffScreen();
    }

    void Update()
    {
        transform.position = new Vector2(transform.position.x, startY +
            Mathf.Sin(8f * accumulatedTime / oscillationPeriod) - 0.5f);
        accumulatedTime += Time.deltaTime;
    }
}
