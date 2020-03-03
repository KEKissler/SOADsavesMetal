using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAttack : MonoBehaviour
{
    public Player ps;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract IEnumerator AttackShort();
    public abstract IEnumerator AttackLong();
    public abstract IEnumerator AttackSuper();

    protected float getAngle(Transform tr)
    {
        return Mathf.Deg2Rad * tr.rotation.y * -180f;
    }

    // Returns 1 if facing right, -1 if facing left
    protected float getDirection(Transform tr)
    {
        return (Convert.ToInt32(getAngle(tr) == 0f) - 0.5f) * 2f;
    }
}
