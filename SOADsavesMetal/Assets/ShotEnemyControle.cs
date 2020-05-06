using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEnemyControle : MonoBehaviour
{
    [SerializeField]
    string playerTag = "Player";
    [SerializeField]
    string perchTag = "Shot Minnion Perch";
    [SerializeField]
    float perchSwitchTime = 5;
    [SerializeField]
    float speed = 3;
    GameObject player;
    GameObject[] perches;
    GameObject perch;
    int perchCount = 0;
    //Time stuff
    float lastPerchSwitch;
    //Time stuff done

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag);
        perches = GameObject.FindGameObjectsWithTag(perchTag);
        Debug.Log(perches.Length);
        Debug.Log(perches);
        perch = perches[perchCount];
        lastPerchSwitch = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        FacePlayer();
        if(Time.time - lastPerchSwitch >= 5)
        {
            SelectNewPerch();
            lastPerchSwitch = Time.time;
        }

        MoveToPerch();
    }

    private void MoveToPerch()
    {
        //Vector3.MoveTowards(gameObject.transform.position, perch.transform.position,speed);
        if((perch.transform.position - transform.position).magnitude >= .05f )
            transform.position += (perch.transform.position - transform.position).normalized * speed * Time.deltaTime;
    }

    private void SelectNewPerch()
    {
        
            perchCount++;
            if (perchCount == perches.Length)
            {
                perchCount = 0;
            }
            perch = perches[perchCount];
        
    }

    void FacePlayer()
    {
        Vector3 targ = player.transform.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));
        
        if(targ.x < objectPos.x)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipY = false;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipY = true;
        }
        
    }
}
