using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveMic : MonoBehaviour {
 
    protected Rigidbody2D rb;
    public int damage = 30;
    protected float speed;
    public float playerRotationY;

	// Use this for initialization
	void Start () {
        speed = 11.5f;
		rb = GetComponent<Rigidbody2D>();

        float angle = Mathf.Deg2Rad * playerRotationY * -180f;
        rb.velocity = new Vector2(speed * Mathf.Cos(angle), 6f);

		Destroy(gameObject, 25f / speed);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "BossHittable")
        {
            // Disable collider
            
            col.gameObject.SendMessage("hit", damage);

            Animator anim = GetComponent<Animator>();
            anim.SetBool("explode", true);

            Destroy(gameObject, 0.03f);
        }
    }


}
