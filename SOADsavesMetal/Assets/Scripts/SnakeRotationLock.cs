using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeRotationLock : MonoBehaviour
{
    public Transform targetBase;
    public Transform targetHead;
    public bool isHead = false;

    float timer = 0f;
    const float UPDATE_TIME = 0.02f;
    bool enabled = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float baseAngle = Mathf.Atan2(transform.position.y-targetBase.position.y, transform.position.x-targetBase.position.x);
        float angle;
        if(isHead)  angle = baseAngle - Mathf.PI/2;
        else
        {
            float headAngle = Mathf.Atan2(targetHead.position.y-transform.position.y, targetHead.position.x-transform.position.x);
            angle = (headAngle + baseAngle) / 2f;
        }
        if(enabled)
        {
            timer += Time.deltaTime;
            if(timer > UPDATE_TIME)
            {
                timer %= UPDATE_TIME;
                transform.LookAt(new Vector3(transform.position.x, transform.position.y, 1),
                    new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0));
            }
        }
    }
}
