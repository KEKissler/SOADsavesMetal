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
    float time;
    // Start is called before the first frame update
    void Start()
    {
        AttackObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (goUp)
        {
            AttackObject.SetActive(false);
            transform.position = transform.position + new Vector3(0, speed * Time.deltaTime, 0);
        }
        if(transform.position.y > attackPos && attack == true)
        {
            goUp = false;
            if (time == 0) {
                time = Time.time;
                //start playing attack animation

            }
            

            if(Time.time - time >= attackTime){
                attack = false; goUp = true;
                AttackObject.SetActive(true);
            }

        }
    }

    public void goUpNow(GameObject player)
    {
        gameObject.transform.position = new Vector3(player.transform.position.x, -2, 0);
        goUp = true;
        attack = true;
        time = 0;
    }
}
