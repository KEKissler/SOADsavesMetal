using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaronParryBubble : MonoBehaviour
{
    public Player ps;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Projectile")
        {
            Destroy(col.gameObject);
            ps.daronListeningForParry = false;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Projectile")
        {
            Destroy(col.gameObject);
            ps.daronListeningForParry = false;
        }
    }
}
