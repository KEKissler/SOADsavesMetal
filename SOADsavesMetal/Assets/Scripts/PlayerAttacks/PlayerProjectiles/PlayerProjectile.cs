using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {Default, Right, Left};

public class PlayerProjectile : MonoBehaviour
{
    public int damage;

    protected float velocity;
    protected Direction direction;

    public void setDamage(int damage)
    {
        this.damage = damage;
    }

    public void setVelocity(float velocity)
    {
        this.velocity = velocity;
    }

    public void setDirection(Direction direction)
    {
        this.direction = direction;
    }

    // Lets the projectile travel around 25 units before destroying
    protected void DestroyWhenOffScreen()
    {
        Destroy(this, 25f / velocity);
    }
}
