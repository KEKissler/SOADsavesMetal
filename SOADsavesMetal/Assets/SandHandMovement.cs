using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandHandMovement : MonoBehaviour
{
    [SerializeField]
    GameObject AttackObject;
    [SerializeField]
    float speed = .1f;
    [SerializeField]
    float attackTime = .5f;
    [SerializeField]
    float attackPos = 5f;
    [SerializeField]
    bool goUp = false;
    [SerializeField]
    bool attack = false;
    float time = 0;
    float DeathTime = 10;
    // Start is called before the first frame update
    void Start()
    {
        AttackObject.SetActive(false);
        time = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (goUp && Time.time - time > attackTime)
        {
            AttackObject.SetActive(false);
            transform.position = transform.position + new Vector3(0, speed * Time.deltaTime, 0);
        }
        if(transform.position.y > attackPos && attack == true)
        {
            if (goUp == true)
            {
                time = Time.time;
                //start playing attack animation

            }
            goUp = false;
            
            

            if(Time.time - time >= attackTime){
                attack = false; goUp = true;
                AttackObject.SetActive(true);
                time = Time.time;
            }

        }
        if(Time.time - time > DeathTime)
        {
            Destroy(this.gameObject);
        }
    }

    public void goUpNow(Transform player, float Y_offset)
    {
        gameObject.transform.position = new Vector3(player.position.x, Y_offset, 0);
        goUp = true;
        attack = true;
        time = 0;
    }
}
