// Note: try to move the attacks and attack pattern code out to another file
// The code to maintain the snake itself should stay here though, interfacing with other code
// Move the attack code onto the statue itself, have it send messages / call functions
// on the statue hand and the snake base

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nhang : MonoBehaviour
{
    private bool attacking;
	private float attackTimer = 0.0f;
    public GameObject statueHand;
    public GameObject indicator;

    // Prefabs
    public GameObject snakeBody;
    public GameObject snakeHead;
    public GameObject bodyAnchor;
    public GameObject projectile;
    
    // Public vars
    public GameObject[] body; // = new GameObject[bodyLength];
    public GameObject[] groundTargets; // 0 is closer to snake, large numbers are further
    public GameObject[] airTargets; // 0 is lower, large numbers are higher

    // Internal vars
    Rigidbody2D[] bodyRB;
    Rigidbody2D rb;
    GameObject[] anchor;
    bool anchorsEnabled = true;
    bool bodyRotationEnabled = true;
    private int bodyLength;
    private float anchorOscillationFreq;
    private float anchorHeadOscillationMult = 1.15f;

    private float medHPthreshold = 13000;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if(!player) player = GameObject.FindWithTag("Player");
        bodyLength = body.Length;
        rb = GetComponent<Rigidbody2D>();
        buildSnake();
        //statueHand.transform.position = (Vector2)transform.position + new Vector2(-5f, -3.4f);
        // indicator.transform.position = (Vector2)transform.position + new Vector2(-5f, -1.5f);
        StartCoroutine(basicPattern());
    }

    // Update is called once per frame
    void Update()
    {
//        if(false)
//        {
//            if(Input.GetKeyDown(KeyCode.A))
//            {
//                StartCoroutine(slam());
//            }
//
//            if(Input.GetKey(KeyCode.S))
//            {
//                StartCoroutine(spit());
//            }
//
//            if(Input.GetKeyDown(KeyCode.D))
//            {
//                StartCoroutine(hand());
//            }
//
//            if(Input.GetKeyDown(KeyCode.Q))
//            {
//                StartCoroutine(lunge());
//            }
//        }
    }

    #region SnakeCode

    public void buildSnake()
    {
        bodyRB = new Rigidbody2D[bodyLength];
        anchor = new GameObject[bodyLength];
        for(int i=0; i<bodyLength-1; ++i)
        {
            // body[i] = Instantiate(snakeBody, bodyPosition(i+1), Quaternion.identity);
            if(i == 0)
            {
                body[0].GetComponent<SpringJoint2D>().connectedBody = rb;
                body[i].GetComponent<SpringJoint2D>().distance = (transform.position - body[i].transform.position).magnitude;
                body[0].GetComponent<SnakeRotationLock>().targetBase = transform;
            }
            else
            {
                body[i].GetComponent<SpringJoint2D>().connectedBody = body[i-1].GetComponent<Rigidbody2D>();
                body[i].GetComponent<SpringJoint2D>().distance = (body[i-1].transform.position - body[i].transform.position).magnitude;
                body[i].GetComponent<SnakeRotationLock>().targetBase = body[i-1].transform;
            }
            if(i == bodyLength-2)   body[bodyLength-2].GetComponent<SnakeRotationLock>().targetHead = body[bodyLength-1].transform;
            else    body[i].GetComponent<SnakeRotationLock>().targetHead = body[i+1].transform;
            bodyRB[i] = body[i].GetComponent<Rigidbody2D>();

            anchor[i] = Instantiate(bodyAnchor, body[i].transform.position, Quaternion.identity);
            anchor[i].GetComponent<SpringJoint2D>().connectedBody = bodyRB[i];
        }
        
        // body[bodyLength-1] = Instantiate(snakeHead, bodyPosition(bodyLength), Quaternion.identity);
        body[bodyLength-1].GetComponent<SpringJoint2D>().connectedBody = body[bodyLength-2].GetComponent<Rigidbody2D>();
        body[bodyLength-1].GetComponent<SpringJoint2D>().distance = (body[bodyLength-2].transform.position - body[bodyLength-1].transform.position).magnitude;
        bodyRB[bodyLength-1] = body[bodyLength-1].GetComponent<Rigidbody2D>();
        body[bodyLength-1].GetComponent<SnakeRotationLock>().targetBase = body[bodyLength-2].transform;
        body[bodyLength-1].GetComponent<SnakeRotationLock>().isHead = true;

        anchor[bodyLength-1] = Instantiate(bodyAnchor, body[bodyLength-1].transform.position, Quaternion.identity);
        anchor[bodyLength-1].GetComponent<SpringJoint2D>().connectedBody = bodyRB[bodyLength-1];

        anchorOscillationFreq = anchor[bodyLength-1].GetComponent<SpringJoint2D>().frequency;
        anchor[bodyLength-1].GetComponent<SpringJoint2D>().frequency *= anchorHeadOscillationMult;
    }

    Vector2 bodyPosition(int partIndex)
    {
        return new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.75f * partIndex);
    }

    #endregion SnakeCode

    #region Attacks

    #region SnakeMelee

    IEnumerator slam()
    {
        attacking = true;
        toggleAnchors();
        // toggleRotation();
        float timer = 0f;
        while(timer < 0.75f)
        {
            for(int i=0; i<bodyLength; ++i) bodyRB[i].AddForce(new Vector2(-28.9f+11.1f*timer-9.9f*(float)i, -15.3f-54f*timer-3.9f*(float)i));
            bodyRB[bodyLength-1].AddForce(new Vector2(-40f+48f*timer, -48f*timer));
            timer += Time.deltaTime;
            yield return null;
        }

        bodyRB[bodyLength-1].velocity = new Vector2(1f, 3.6f);
        toggleAnchors();
        // toggleRotation();

        attackTimer = 0.0f;
        while(attackTimer < 1.5f)
        {
            attackTimer += Time.deltaTime;
            yield return null;
        }
        
        attacking = false;
    }

    IEnumerator lunge()
    {
        float snapDrag = 35f;   // snapDrag on
        float endDrag = 3.2f;

        attacking = true;
        toggleAnchors();
        // toggleRotation();
        float timer = 0f;
        bodyRB[bodyLength-1].velocity += new Vector2(-7.0f, -0.5f);

        // Extend
        float snakeLungeMagnitude = 670f;
        float veryWellNamedAngle = Mathf.Atan2(player.transform.position.y - body[bodyLength-1].transform.position.y + 0.8f,
                                                player.transform.position.x - body[bodyLength-1].transform.position.x);
        Debug.Log(veryWellNamedAngle);
        while(timer < 0.9f)
        {
            for(int i=0; i<bodyLength-1; ++i)
            {
                // bodyRB[i].AddForce(new Vector2(5.3f-4.9f*timer+6.2f*(float)i, 0.9f-2.5f*timer-0.4f*(float)i));
            }
            // bodyRB[bodyLength-1].AddForce(new Vector2(-259.3f-485f*timer-3.9f*(float)(bodyLength-1), -20.3f-24f*timer-2.2f*(float)(bodyLength-1)));
            bodyRB[bodyLength-1].AddForce(new Vector2(
                snakeLungeMagnitude * Mathf.Cos(veryWellNamedAngle),
                snakeLungeMagnitude * Mathf.Sin(veryWellNamedAngle)
                ));
            timer += Time.deltaTime;
            yield return null;
        }

        // Snap
        for(int i=0; i<bodyLength; ++i)
        {
            bodyRB[i].velocity = new Vector2(-1.5f, 0.2f);
            bodyRB[i].drag = snapDrag;
        }
        bodyRB[bodyLength-1].angularDrag = 99f;
        bodyRB[bodyLength-1].velocity += new Vector2(-2.0f, -0.2f);

        // Hold
        timer = 0f;
        while(timer < 0.05f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        // Retract

        // for(int i=0; i<bodyLength; ++i) bodyRB[i].drag = 3.2f;

        for(int i=0; i<bodyLength-1; ++i)   bodyRB[i].velocity += new Vector2(3.45f, 0f);
        toggleAnchors();

        timer = 0f;
        float timeToLerpAnchor = 1.1f;
        while(timer < timeToLerpAnchor)
        {
            for(int i=0; i<bodyLength-2; ++i) bodyRB[i].AddForce(new Vector2(0f, 18f));
            bodyRB[bodyLength-1].AddForce(new Vector2(0f, -24f));
            timer += Time.deltaTime;
            for(int i=0; i<bodyLength; ++i)
            {
                float lerpValue = timer / timeToLerpAnchor;
                anchor[i].GetComponent<SpringJoint2D>().frequency = anchorOscillationFreq * lerpValue;
                bodyRB[i].drag = snapDrag * (1-lerpValue) + endDrag * lerpValue;
            }
            anchor[bodyLength-1].GetComponent<SpringJoint2D>().frequency *= anchorHeadOscillationMult;
            yield return null;
        }

        bodyRB[bodyLength-1].angularDrag = 8f;
        
        // toggleRotation();

        // Bonus wait time
        timer = 0f;
        while(timer < 1.5f)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        
        attacking = false;
    }

    void toggleAnchors()
    {
        anchorsEnabled = !anchorsEnabled;
        for(int i=0; i<bodyLength; ++i)
        {
            anchor[i].GetComponent<SpringJoint2D>().enabled = anchorsEnabled;
        }
    }

    void toggleRotation()
    {
        bodyRotationEnabled = !bodyRotationEnabled;
        for(int i=0; i<bodyLength; ++i)
        {
            body[i].GetComponent<SnakeRotationLock>().enabled = bodyRotationEnabled;
        }
    }

    #endregion SnakeMelee
    #region SnakeRanged

    IEnumerator spit(GameObject target, ProjectileType pt = ProjectileType.Gravity,
        ProjectileSpeed ps = ProjectileSpeed.Fast, float degreeModifier = 0.0f)
    {
        attacking = true;

        GameObject temp = Instantiate(projectile, body[bodyLength-1].transform.position, Quaternion.identity);
        yield return null;
        temp.GetComponent<Projectile>().Configure(target, pt, ps, degreeModifier);
        
        attacking = false;
    }

    
    IEnumerator repeatProjectile(int numRepeats, float waitTime, GameObject target, ProjectileType pt = ProjectileType.Gravity,
        ProjectileSpeed ps = ProjectileSpeed.Fast, float degreeModifier = 0.0f)
    {
        attacking = true;
        float timer = waitTime;
        for(int i=0; i<numRepeats; ++i)
        {
            while(timer < waitTime)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            timer = 0f;
            // Fire projectile
            GameObject temp = Instantiate(projectile, body[bodyLength-1].transform.position, Quaternion.identity);
            yield return null;
            yield return null;
            temp.GetComponent<Projectile>().Configure(player, ProjectileType.Gravity, ProjectileSpeed.Fast, 0);
        }
        attacking = false;
    }

    IEnumerator fanProjectile(int numProjectiles, float angularSpacing, GameObject target, ProjectileType pt = ProjectileType.Gravity,
        ProjectileSpeed ps = ProjectileSpeed.Fast, float degreeModifier = 0.0f)
    {
        if(numProjectiles < 1)  numProjectiles = 1;
        float yv = -angularSpacing*0.5f*(numProjectiles-1) + degreeModifier;

        GameObject[] temp = new GameObject[numProjectiles];

        for(int i=0; i<numProjectiles; ++i)
        {
            temp[i] = Instantiate(projectile, body[bodyLength-1].transform.position, Quaternion.identity);
        }

        yield return null;

        // Add sinusoidal xVel modifier
        for(int i=0; i<numProjectiles; ++i)
        {
            temp[i].GetComponent<Projectile>().Configure(target, pt, ps, angularSpacing);
            yv += angularSpacing;
        }

        yield return null;
    }

    IEnumerator repeatFanProjectile(int numRepeats, float waitTime, int numProjectiles, float angularSpacing, GameObject target,
        ProjectileType pt = ProjectileType.Linear, ProjectileSpeed ps = ProjectileSpeed.Med, float degreeModifier = 0.0f)
    {
        if(numProjectiles < 1)  numProjectiles = 1;
        attacking = true;
        float timer = waitTime;
        for(int r=0; r<numRepeats; ++r)
        {
            while(timer < waitTime)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            timer = 0f;

            // Fire projectile
            float yv = -angularSpacing*0.5f*(numProjectiles-1);

            GameObject[] temp = new GameObject[numProjectiles];

            for(int i=0; i<numProjectiles; ++i)
            {
                temp[i] = Instantiate(projectile, body[bodyLength-1].transform.position, Quaternion.identity);
            }

            yield return null;

            // Add sinusoidal xVel modifier
            for(int i=0; i<numProjectiles; ++i)
            {
                temp[i].GetComponent<Projectile>().Configure(target, pt, ps, yv + degreeModifier);
                yv += angularSpacing;
            }
        }
        attacking = false;

        yield return null;
    }

    #endregion SnakeRanged
    #region StatueHand

    const float HAND_VELOCITY = 12f;
    const float HAND_MOVE_TIME = 0.52f;
    private bool handMoving = false;
    
    IEnumerator handUp()
    {
        float highVelocity = 2 * HAND_VELOCITY;
        float minVelocity = 1.5f;
        handMoving = true;
        indicator.SetActive(true);
        float timer = 0.0f;
        Rigidbody2D statueRB = statueHand.GetComponent<Rigidbody2D>();

        // Time to react to indicator
        while(timer < 1.0f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0.0f;
        while(timer < HAND_MOVE_TIME)
        {
            timer += Time.deltaTime;
            statueRB.velocity = new Vector2(0, (highVelocity-minVelocity) * (1 - timer / HAND_MOVE_TIME) + minVelocity * timer / HAND_MOVE_TIME);
            yield return null;
        }

        statueRB.velocity = new Vector2(0, 0);
        handMoving = false;
    }

    IEnumerator handDown()
    {
        handMoving = true;
        float timer = 0.0f;
        Rigidbody2D statueRB = statueHand.GetComponent<Rigidbody2D>();

        statueRB.velocity = new Vector2(0, -HAND_VELOCITY);

        while(timer < HAND_MOVE_TIME)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        statueRB.velocity = new Vector2(0, 0);
        statueHand.transform.localPosition = new Vector2(-1.676f, -4.58f);

        indicator.SetActive(false);
        handMoving = false;
    }

    IEnumerator hand()
    {
        attacking = true;
        float timer = 0.0f;
        Rigidbody2D statueRB = statueHand.GetComponent<Rigidbody2D>();

        handMoving = true;
        StartCoroutine(handUp());
        while(handMoving)    yield return null;

        while(timer < 0.8f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        handMoving = true;
        StartCoroutine(handDown());
        while(handMoving)   yield return null;
        attacking = false;
    }

    #endregion StatueHand

    #endregion Attacks

    bool isPlayerClose()
    {
        return player.transform.position.x > -1f;
    }


    #region AttackPatterns

    #region BasicPatterns

    IEnumerator basicPattern()
    {
        float defaultWait = 3f;
        
        float timer = 0f;
        int attackPhase = 0;    // Keep track of attack phase
        float waitTime = defaultWait;

        while(true)
        {
            if(attacking)   yield return null;

            // Check if below HP threshold

            timer += Time.deltaTime;
            if(timer > waitTime)
            {
                // Debug.Log(attackPhase);
                timer = 0f;
                switch(attackPhase)
                {
                    case 0:
                        waitTime = defaultWait;
                        // 3 projectiles
                        StartCoroutine(repeatProjectile(3, 0.55f, player));
                        ++attackPhase;
                        break;
                    case 1:
                        // lunge
                        if(isPlayerClose())
                        {
                            waitTime = 0.04f;
                            attackPhase = 10;
                        }
                        else
                        {
                            StartCoroutine(lunge());
                            ++attackPhase;
                        }
                        break;
                    case 2:
                        // 4 shots away
                        if(isPlayerClose())
                        {
                            waitTime = 0.04f;
                            attackPhase = 10;
                        }
                        else
                        {
                            for(int t = 0; t<4; ++t)
                            {
                                StartCoroutine(spit(groundTargets[2], ProjectileType.Linear, ProjectileSpeed.Fast, (t-1.5f) * -19f));
                                while(timer < 0.1f)
                                {
                                    timer += Time.deltaTime;
                                    yield return null;
                                }
                                timer = 0f;
                            }

                            // StartCoroutine(repeatFanProjectile(2, 0.5f, 3, 30f, player));
                            ++attackPhase;
                        }
                        break;
                    case 3:
                        // hand barrier
                        if(isPlayerClose())
                        {
                            waitTime = 0.04f;
                            attackPhase = 10;
                        }
                        else
                        {
                            StartCoroutine(handUp());
                            ++attackPhase;
                        }
                        break;
                    case 4:
                        // hand barrier down
                        waitTime = defaultWait;
                        StartCoroutine(handDown());
                        attackPhase = 0;
                        break;
                    case 10:
                        // statue hand
                        waitTime = defaultWait;
                        StartCoroutine(hand());
                        attackPhase = 0;
                        break;
                }
            }

            yield return null;
        }
    }

    #endregion BasicPatterns
    #region MediumPatterns

    IEnumerator mediumPattern()
    {
        float timer = 0f;
        int attackPhase = 0;    // Replace with enum later
        float shortWait = 1.0f;
        float longWait = 2.5f;

        bool isLongWait = true;

        while(true)
        {
            if(attacking)   yield return null;

            timer += Time.deltaTime;
            if(timer > (isLongWait ? longWait : shortWait))
            {
                timer = 0f;
                switch(attackPhase)
                {
                    case 0:
                        // 5 projectiles
                        //StartCoroutine(repeatProjectile(5, 0.22f));
                        isLongWait = true;
                        break;
                    case 1:
                        // lunge
                        StartCoroutine(lunge());
                        isLongWait = true;
                        break;
                    case 2:
                        // fan shape projectiles
                        //StartCoroutine(repeatFanProjectile(3, 0.25f, 5, 5.5f));
                        break;
                    case 3:
                        // statue hand
                        StartCoroutine(hand());
                        break;
                }
                ++attackPhase;
                attackPhase %= 4;   // Hard-coded number of phases
            }

            yield return null;
        }
    }

    #endregion MediumPatterns

    #endregion AttackPatterns
}
