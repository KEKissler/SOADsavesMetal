using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionKiller : MonoBehaviour
{
    [SerializeField]
    string PlayerProjectile = "Player Projectile";
 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == PlayerProjectile)
        {
            //Destroy(collision.gameObject);
            //Play Death animation here
            Destroy(this.gameObject);
        }
    }
}
