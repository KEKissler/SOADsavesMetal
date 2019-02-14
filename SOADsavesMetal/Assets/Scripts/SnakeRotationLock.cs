using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeRotationLock : MonoBehaviour
{
    public Transform target;

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
        if(enabled)
        {
            timer += Time.deltaTime;
            if(timer > UPDATE_TIME)
            {
                timer %= UPDATE_TIME;
                transform.LookAt(new Vector3(transform.position.x, transform.position.y, 1),
                    new Vector3(transform.position.x-target.position.x, transform.position.y-target.position.y, 0));
            }
        }
    }
}
