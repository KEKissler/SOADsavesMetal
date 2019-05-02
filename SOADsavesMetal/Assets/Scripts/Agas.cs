using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agas : MonoBehaviour
{
    // Prefabs
    public GameObject candle;
    public GameObject spike;

    // Scene objects
    public GameObject overflowLiquid;
    private GameObject player;

    private bool attacking;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        attacking = false;
        StartCoroutine(basicPattern());
    }

    void Update()
    {

    }

    // Warning indicator for spikes should be red X

    // When the goblet overflows, you can do special interactions between the attacks and the liquid
    
    IEnumerator createCandle()
    {
        Instantiate(candle, player.transform.position + new Vector3(2f, 2f, 0), Quaternion.identity);
        yield return null;
    }

    IEnumerator createSpike()
    {
        Instantiate(spike, new Vector3(player.transform.position.x, -4.82f), Quaternion.identity);
        yield return null;
    }

    IEnumerator basicPattern()
    {
        float timer = 0f;
        int attackPhase = 0;
        float waitTime = 2.9f;

        while(true)
        {
            if(attacking)   yield return null;

            timer += Time.deltaTime;
            if(timer > waitTime)
            {
                timer = 0f;
                switch(attackPhase)
                {
                    case 0:
                        // 
                        StartCoroutine(overflowLiquid.GetComponent<DecayOverflow>().flood());
                        break;
                    case 1:
                        // 
                        StartCoroutine(createCandle());
                        break;
                    case 2:
                        // 
                        
                        break;
                    case 3:
                        // 
                        StartCoroutine(createSpike());
                        break;
                }
                ++attackPhase;
                attackPhase %= 4;   // Hard-coded number of phases
            }

            yield return null;
        }
    }
}