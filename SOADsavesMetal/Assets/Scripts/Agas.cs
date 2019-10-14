using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agas : MonoBehaviour
{
    private const float SPIKE_HEIGHT = -4.81f;
    private const float GROUND_SPACING = 20.95f;
    private const float ARC_SPACING = 16.25f;
    private const float GROUND_Y = -4.75f;
    private const float ARC_Y = 5.15f;
    private float defaultDamageMultiplier = 1f;
    private float defaultWaitTime = 5f;
    private GameObject[] groundSpikes;
    private float spikeStart = 4f;

    // Prefabs
    public GameObject candle;
    public GameObject spike;
    public GameObject ghostball;

    // Scene objects
    public GameObject overflowLiquid;
    private GameObject player;
    public GameObject[] candles;
    public GameObject agas;
    public BossHealth healthScript;
    public GameObject goblet;
    public int numberOfSpikes;
    public float spikeSpacing;

    private bool attacking;
    System.Random randomGen = new System.Random();

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        attacking = false;
        groundSpikes = new GameObject[numberOfSpikes];
        //for(int i=0; i<numberOfSpikes; ++i) {
        //    groundSpikes[i] = Instantiate(spike, new Vector2(spikeStart-spikeSpacing*i, SPIKE_HEIGHT), Quaternion.identity);
		//}
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
        Instantiate(spike, new Vector3(player.transform.position.x, SPIKE_HEIGHT), Quaternion.identity);
        yield return null;
    }

    IEnumerator scroll()
    {
        float scrollSpeed = 2.5f;
        float scrollTime = ARC_SPACING / scrollSpeed;
        float timer = 0f;

        // Create new terrain in scroll direction if it does not already exist
        // Set velocity
        while(timer < scrollTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
    }

    void turnOnInactiveCandle()
    {
        int c;
        bool foundCandle = false;
        do
        {
            c = randomGen.Next(6);
            if(!candles[c].GetComponent<CandleEmitter>().getCandleActive())
            {
                candles[c].GetComponent<CandleEmitter>().enableFire();
                foundCandle = true;
            }
        }
        while(!foundCandle);

        candles[c].GetComponent<CandleEmitter>().setMaxShots(randomGen.Next(3) + 4);
        candles[c].GetComponent<CandleEmitter>().setFirePeriod((randomGen.Next(3) + 14.0f)/4f);
        candles[c].GetComponent<CandleEmitter>().setSpeed(randomGen.Next(2));
    }

    IEnumerator agasFireball(GameObject target, ProjectileType pt = ProjectileType.Linear,
        ProjectileSpeed ps = ProjectileSpeed.Fast, float degreeModifier = 3f)
    {
        attacking = true;

        GameObject temp = Instantiate(ghostball, agas.transform.position, Quaternion.identity);
        yield return null;
        temp.GetComponent<Projectile>().Configure(target, pt, ps, degreeModifier);
        
        attacking = false;
    }

    // Submerge, not fade
    IEnumerator submerge(float submergeTime)
    {
        float timer = 0f;
        float moveDuration = 1.2f;
        float moveVelocity = 5.0f;
        Vector2 startPosition = agas.transform.position;
        healthScript.changeMultiplier(0.5f * defaultDamageMultiplier);

        while(timer < moveDuration)
        {
            agas.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -moveVelocity * timer);
            timer += Time.deltaTime;
            yield return null;
        }
        timer = 0f;
        agas.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        healthScript.changeMultiplier(0f);

        while(timer < submergeTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        timer = 0f;
        healthScript.changeMultiplier(0.5f * defaultDamageMultiplier);

        while(timer < moveDuration)
        {
            agas.GetComponent<Rigidbody2D>().velocity = new Vector2(0, moveVelocity * (moveDuration-timer) - 0.031f);
            timer += Time.deltaTime;
            yield return null;
        }
        timer = 0f;
        agas.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        agas.transform.position = startPosition;
        healthScript.changeMultiplier(defaultDamageMultiplier);
    }

    IEnumerator basicPattern()
    {
        float timer = 0f;
        int attackPhase = 0;
        float waitTime = defaultWaitTime;

        while(true)
        {
            if(healthScript.getHPPercentage() <= 75f)
            {
                StartCoroutine(submerge(4.5f));
                turnOnInactiveCandle();
                break;
            }
            if(attacking)   yield return null;

            timer += Time.deltaTime;
            if(timer > waitTime)
            {
                timer = 0f;
                switch(attackPhase)
                {
                    case 0:
                        // Enable a random candle
                        turnOnInactiveCandle();
                        break;
                    case 1:
                        // Idk
                        break;
                    case 2:
                        // Flood
                        StartCoroutine(overflowLiquid.GetComponent<DecayOverflow>().flood(5f));
                        waitTime = 9.5f;
                        break;
                    case 3:
                        // Another candle
                        turnOnInactiveCandle();
                        waitTime = defaultWaitTime;
                        break;
                    case 4:
                        // Spike
                        StartCoroutine(createSpike());
                        break;
                }
                ++attackPhase;
                attackPhase %= 4;   // Hard-coded number of phases
            }

            yield return null;
        }

        StartCoroutine(mediumPattern());
    }

    // 75% HP
    IEnumerator mediumPattern()
    {
        float timer = 0f;
        int attackPhase = 0;
        defaultWaitTime = 4.5f;
        float waitTime = defaultWaitTime;

        while(true)
        {
            // if(healthScript.getHPPercentage() <= 50f)
            // {
            //     turnOnInactiveCandle();
            //     StartCoroutine(submerge(5f));
            //     yield return null;
            //     break;
            // }
            if(attacking)   yield return null;

            timer += Time.deltaTime;
            if(timer > waitTime)
            {
                timer = 0f;
                switch(attackPhase)
                {
                    case 0:
                        // Enable a random candle
                        turnOnInactiveCandle();
                        break;
                    case 1:
                        // Spike
                        StartCoroutine(createSpike());
                        break;
                    case 2:
                        // Flood
                        StartCoroutine(overflowLiquid.GetComponent<DecayOverflow>().flood(5f));
                        waitTime = 5f;
                        break;
                    case 3:
                        // Ghostball
                        StartCoroutine(agasFireball(player));
                        waitTime = defaultWaitTime;
                        break;
                }
                ++attackPhase;
                attackPhase %= 4;   // Hard-coded number of phases
            }

            yield return null;
        }

        // StartCoroutine(mediumPattern());
    }
}