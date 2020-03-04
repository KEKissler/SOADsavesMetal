using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaronString : PlayerProjectile
{
    public float LIFESPAN;
    private Animator animator;
    private float timer;
    float angle;

    public float initialSpeed;

    private SpriteRenderer sr;
    private Vector4 initialColor;

    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        initialColor = sr.color;

        animator = GetComponent<Animator>();
        timer = 0f;
        angle = Mathf.Deg2Rad * transform.rotation.y * -180f;

        Destroy(gameObject, LIFESPAN);
    }

    // Update is called once per frame
    void Update()
    {
        float curLife = timer / LIFESPAN;
        animator.speed = Mathf.Lerp(1.35f, 0, curLife);
        speed = Mathf.Lerp(initialSpeed, initialSpeed / 4, curLife);
        sr.color = setAlpha(Mathf.Sqrt(Mathf.Lerp(initialColor.w, 0, curLife)));
        rb.velocity = new Vector2(speed * Mathf.Cos(angle), speed * Mathf.Sin(angle));

        timer += Time.deltaTime;
    }

    private Vector4 setAlpha(float alpha)
    {
        return new Vector4(initialColor.x, initialColor.y, initialColor.z, alpha);
    }
}
