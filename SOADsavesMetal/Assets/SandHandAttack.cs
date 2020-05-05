using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandHandAttack : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(collision.gameObject.GetComponent<Player>() != null)
            {
                collision.gameObject.GetComponent<Player>().DamagePlayer();
            }
        }
    }
}
