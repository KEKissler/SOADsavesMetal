using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandHandAttack : MonoBehaviour
{
    private bool Damage = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && Damage)
        {
            if(collision.gameObject.GetComponent<Player>() != null)
            {
                collision.gameObject.GetComponent<Player>().DamagePlayer();
            }
        }
    }

    public void setDamage(bool input)
    {
        Damage = input;
    }
}
