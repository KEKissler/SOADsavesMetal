using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearShow : MonoBehaviour
{
    [SerializeField]
    GameObject Tear;
    [SerializeField]
    float timeTillDrop = 4;
    [SerializeField]
    float flashRate = .2f;
    float time;
    float flashTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        flashTime = 0;
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
