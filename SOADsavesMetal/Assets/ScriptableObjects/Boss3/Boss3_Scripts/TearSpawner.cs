using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject Tear;
    [SerializeField]
    float[] times;
    int tear;
    float time;
    bool isCrying = false;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        tear = 0;
    }

    public void Cry()
    {
        isCrying = true;
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCrying)
        {
            if(Time.time - time >= times[tear])
            {
                time = Time.time;
                //Spawn bullet
                if(tear < times.Length)
                {
                    tear++;
                }
                else
                {
                    tear = 0;
                }
            }
        }
    }
}
