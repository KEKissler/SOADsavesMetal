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

}
