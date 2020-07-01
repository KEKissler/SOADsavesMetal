using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandHandAttack : MonoBehaviour
{
    private bool Damage = false;
    private Sidescroll sidescroll;

    void Start()
    {
        sidescroll = FindObjectOfType<Sidescroll>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            sidescroll.movePlayer = false;
            if(collision.gameObject.GetComponent<Player>() != null && Damage)
            {
                collision.gameObject.GetComponent<Player>().DamagePlayer();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            sidescroll.movePlayer = true;
        }
    }

    public void setDamage(bool input)
    {
        Damage = input;
    }
}
