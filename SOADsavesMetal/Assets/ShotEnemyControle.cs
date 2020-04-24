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
    //Time stuff
    float lastPerchSwitch;
    //Time stuff done

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag);
        perches = GameObject.FindGameObjectsWithTag(perchTag);
        perch = perches[0];
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
        Vector3.MoveTowards(gameObject.transform.position, perch.transform.position,speed);
    }

    private void SelectNewPerch()
    {
        GameObject newPerch = perch;
        int temp = 0;
        while(newPerch == perch && temp < 5)
        {
            newPerch = perches[UnityEngine.Random.Range(0, perches.Length)];
            temp++;
        }
            
        
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
    }
}
