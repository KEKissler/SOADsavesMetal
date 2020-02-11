using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHitPlayer : MonoBehaviour
{
    public bool snakeAttacking;

    // Start is called before the first frame update
    void Start()
    {
        snakeAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
		if(col.gameObject.name == "Player" && snakeAttacking) {
			col.GetComponent<Rigidbody2D>().velocity = new Vector2(-18f, 13.37f);
            col.gameObject.GetComponent<Player>().blockMovement(0.37f);
        }
    }
}
