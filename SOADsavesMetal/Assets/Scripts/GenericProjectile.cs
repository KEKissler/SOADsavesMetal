using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericProjectile : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;

    private const float VEL = 7.5f;

    // Start is called before the first frame update
    void Start()
    {   
        if(!player) player = GameObject.Find("Player");
        transform.LookAt(new Vector3(transform.position.x, transform.position.y, 1),
            new Vector3(transform.position.x-player.transform.position.x, transform.position.y-player.transform.position.y, 0));
        
        rb = GetComponent<Rigidbody2D>();
        float a = player.transform.position.x-transform.position.x;
        float b = player.transform.position.y-transform.position.y;
        float m = (transform.position-player.transform.position).magnitude;
        rb.velocity = new Vector2(a/m*VEL, b/m*VEL);
        Destroy(gameObject, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
