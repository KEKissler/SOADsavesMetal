using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected int damage;
    protected float speed;

    // Lets the projectile travel a bit over 25 units before destroying it
    protected void DestroyWhenOffScreen()
    {
        Destroy(this, 27f / speed);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "BossHittable") col.gameObject.SendMessage("hit", damage);
    }
}
