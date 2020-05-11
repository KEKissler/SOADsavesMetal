using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public bool isSuper;
    protected Rigidbody2D rb;
    public int damage = 30;
    protected float speed;

    // Lets the projectile travel a bit over 25 units before destroying it
    protected void DestroyAfter(float ttl)
    {
        Destroy(gameObject, ttl);
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

    public float getSpeed()
    {
        return speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "BossHittable")
        {
            col.gameObject.SendMessage("hit", damage);
            Destroy(gameObject, 0.03f);
        }
    }
}
