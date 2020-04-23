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
    public GameObject bodyAnchor;
    public GameObject projectile;
    
    // Public vars
    public GameObject[] body; // = new GameObject[bodyLength];
    public GameObject[] groundTargets; // 0 is closer to snake, large numbers are further
    public GameObject[] airTargets; // 0 is lower, large numbers are higher
    public GameObject[] groundAnchors; // 0 is closer, large numbers are further
    public BoxCollider2D snakeHeadHitbox;

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
        Debug.Log("The stuff below can make the boss do attacks");
        //StartCoroutine(basicPattern());
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

    float snapDrag = 35f;   // snapDrag on
    float endDrag = 3.2f;
    bool snakeMoving = false;

    public IEnumerator fallToGround()
    {
        snakeMoving = true;
        canHitPlayer(true);
        float timer = 0f;

        toggleAnchors();
        for(int i=0; i<bodyLength-1; ++i) bodyRB[i].velocity = new Vector2(-2.2f, 0.2f);
        bodyRB[bodyLength-1].velocity = new Vector2(-2.4f, 0.4f);
        // toggleRotation();
        while(timer < 0.55f)
        {
            for(int i=0; i<bodyLength; ++i) bodyRB[i].AddForce(new Vector2(-85f+85f*timer, -10f-105f*timer));
            bodyRB[bodyLength-1].AddForce(new Vector2(-350f+130f*timer, -10f-440f*timer));
            timer += Time.deltaTime;
            yield return null;
        }

        // Lock to ground
        timer = 0f;
        for(int i=0; i<groundAnchors.Length; ++i)
        {
            groundAnchors[i].GetComponent<SpringJoint2D>().enabled = true;
            while(timer < 0.1f)
            {
                timer += Time.deltaTime;
                yield return null;
            }
        }

        timer = 0f;
        while(timer < 0.2f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        // Snap position
        timer = 0f;
        for(int i=0; i<bodyLength; ++i)
        {
            bodyRB[i].drag = snapDrag * 1.2f;
            while(timer < 0.1f)
            {
                timer += Time.deltaTime;
                yield return null;
            }
        }

        snakeMoving = false;
        canHitPlayer(false);
    }

    IEnumerator rise()
    {
        snakeMoving = true;
        float timer = 0f;

        for(int i=0; i<groundAnchors.Length; ++i)
        {
            groundAnchors[i].GetComponent<SpringJoint2D>().enabled = false;
        }
        for(int i=0; i<bodyLength; ++i)
        {
            bodyRB[i].drag = endDrag;
        }

        bodyRB[bodyLength-1].velocity = new Vector2(0.5f, 1f);
        toggleAnchors();

        while(timer < 0.45f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        snakeMoving = false;
    }

    public IEnumerator slam()
    {
        attacking = true;
        float timer = 0f;

        // Indicator before slam
        while(timer < 1.15f)
        {
            for(int i=0; i<bodyLength; ++i)
                bodyRB[i].AddForce(new Vector2(0f, 20f));
            timer += Time.deltaTime;
            yield return null;
        }
        
        snakeMoving = true;
        StartCoroutine(fallToGround());
        while(snakeMoving)  yield return null;

        timer = 0f;
        while(timer < 0.55f)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        
        snakeMoving = true;
        StartCoroutine(rise());
        while(snakeMoving)  yield return null;

        while(timer < 0.1f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        attacking = false;
    }

    public IEnumerator lunge(GameObject target)
    {

        Debug.Log("LUNGE");
        attacking = true;
        snakeHeadHitbox.enabled = false;
        float timer = 0f;

        FMOD.Studio.EventInstance instance = FMODUnity.RuntimeManager.CreateInstance("event:/Enemy/E_Nhang/E_Nhang_SnakeEyes");
        instance.start();
        // Indicator
        while(timer < 1.35f)
        {
            for(int i=0; i<bodyLength; ++i)
                bodyRB[i].AddForce(new Vector2(17f+5f*timer * 5, 5f+27f*timer));
            timer += Time.deltaTime;
            yield return null;
        }

        while(timer < 0.1f)
        {
            for(int i=0; i<bodyLength; ++i)
                bodyRB[i].AddForce(new Vector2(10f, 13f));
            timer += Time.deltaTime;
            yield return null;
        }

        // Extend
        toggleAnchors();
        canHitPlayer(true);
        timer = 0f;
        FMOD.Studio.EventInstance attackInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Enemy/E_Nhang/E_Nhang_LungeAttack");
        attackInstance.start();
        bodyRB[bodyLength-1].velocity += new Vector2(-7.7f, -0.7f);
        float startLungeMagnitude = 777f;
        float veryWellNamedAngle = Mathf.Atan2(target.transform.position.y - body[bodyLength-1].transform.position.y + 0.77f,
                                                target.transform.position.x - body[bodyLength-1].transform.position.x);
        // Debug.Log(veryWellNamedAngle);
        while(timer < 0.777f)
        {
            for(int i=0; i<bodyLength-1; ++i)
            {
                // bodyRB[i].AddForce(new Vector2(5.3f-4.9f*timer+6.2f*(float)i, 0.9f-2.5f*timer-0.4f*(float)i));
            }
            // bodyRB[bodyLength-1].AddForce(new Vector2(-259.3f-485f*timer-3.9f*(float)(bodyLength-1), -20.3f-24f*timer-2.2f*(float)(bodyLength-1)));
            float currLungeMagnitude = startLungeMagnitude * (1 + timer / 7f);
            bodyRB[bodyLength-1].AddForce(new Vector2(
                currLungeMagnitude * Mathf.Cos(veryWellNamedAngle),
                currLungeMagnitude * Mathf.Sin(veryWellNamedAngle)
                ));
            bodyRB[bodyLength-1].velocity += new Vector2(-0.05f, 0);
            timer += Time.deltaTime;
            yield return null;
        }

        // Snap
        for(int i=0; i<bodyLength; ++i)
        {
            bodyRB[i].velocity += new Vector2(-0.07f, 0.2f);
            bodyRB[i].drag = snapDrag;
        }
        bodyRB[bodyLength-1].angularDrag = 99f;
        bodyRB[bodyLength-1].velocity += new Vector2(-0.07f, 0.1f);

        // Hold
        timer = 0f;
        while(timer < 0.07f)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        canHitPlayer(false);

        // Retract
        for(int i=0; i<bodyLength-1; ++i)   bodyRB[i].velocity += new Vector2(3.65f, 0.3f);
        toggleAnchors();

        timer = 0f;
        float timeToLerpAnchor = 1.3f;
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

        // Bonus wait time
        timer = 0f;
        while(timer < 0.66f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        // Prevent snake jello effect
        bodyRB[bodyLength-1].angularDrag = 8f;
        for(int i=0; i<bodyLength; ++i)
            bodyRB[i].drag *= 3f;

        // Bonus wait time
        timer = 0f;
        while(timer < 0.67f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        for(int i=0; i<bodyLength; ++i)
            bodyRB[i].drag = endDrag;

        snakeHeadHitbox.enabled = true;
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
        FMOD.Studio.EventInstance instance = FMODUnity.RuntimeManager.CreateInstance("event:/Enemy/E_Nhang/E_Nhang_AcidAttack");
        instance.start();
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
        float yv = -angularSpacing*0.5f*(numProjectiles-1);

        for(int i=0; i<numProjectiles; ++i)
        {
            StartCoroutine(spit(target, pt, ps, yv + degreeModifier));
            yv += angularSpacing;
        }

        // yield return null;

        // // Add sinusoidal xVel modifier
        // for(int i=0; i<numProjectiles; ++i)
        // {
        //     temp[i].GetComponent<Projectile>().Configure(target, pt, ps, angularSpacing);
        //     yv += angularSpacing;
        // }

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

    IEnumerator fanSpray(int numRepeats, float waitTime, int numProjectiles, float angularSpacing, GameObject target,
        ProjectileType pt = ProjectileType.Linear, ProjectileSpeed ps = ProjectileSpeed.Med, float degreeModifier = 0.0f)
    {
        attacking = true;
        float timer = 0.0f;
        float halfway = (numProjectiles - 1) / 2f;

        for(int repeat = 0; repeat < numRepeats; ++repeat)
        {
            for(int p = 0; p < numProjectiles; ++p)
            {
                StartCoroutine(spit(target, pt, ps, (p-halfway) * angularSpacing + degreeModifier));
                while(timer < waitTime)
                {
                    timer += Time.deltaTime;
                    yield return null;
                }
                timer = 0f;
            }
        }
        
        attacking = false;
    }

    #endregion SnakeRanged
    #region StatueHand

    const float HAND_VELOCITY = 11.5f;
    const float HAND_MOVE_TIME = 0.52f;
    private bool handMoving = false;
    
    IEnumerator handUp()
    {
        float highVelocity = 2 * HAND_VELOCITY;
        float minVelocity = 6f;
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
        FMOD.Studio.EventInstance instance = FMODUnity.RuntimeManager.CreateInstance("event:/Enemy/E_Nhang/E_Nhang_HandAttack");
        instance.start();
        while (timer < HAND_MOVE_TIME)
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
        statueHand.transform.localPosition = new Vector2(-1.676f, -3.78f);

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

    #region HitPlayer

    public void canHitPlayer(bool hitPlayer)
    {
        for(int i=0; i<bodyLength; ++i)
        {
            body[i].GetComponent<SnakeHitPlayer>().snakeAttacking = hitPlayer;
        }
    }

    #endregion HitPlayer

    bool isPlayerClose()
    {
        return player.transform.position.x > -1f;
    }


    #region AttackPatterns

    #region BasicPatterns

    IEnumerator basicPattern()
    {
        float defaultWait = 2.6f;
        
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
                        StartCoroutine(repeatProjectile(3, 0.45f, player));
                        ++attackPhase;
                        break;
                    case 1:
                        // lunge
                        StartCoroutine(lunge(player));
                        if(isPlayerClose())
                        {
                            waitTime = 1.7f;
                            attackPhase = 10;
                        }
                        else
                        {
                            ++attackPhase;
                        }
                        break;
                    case 2:
                        // 4 shots away
                        StartCoroutine(fanSpray(1, 0.1f, 4, -19f, groundTargets[2],
                            ProjectileType.Linear, ProjectileSpeed.Fast, 0.0f));
                        if(isPlayerClose())
                        {
                            waitTime = 1.7f;
                            attackPhase = 10;
                        }
                        else
                        {
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
                            waitTime = defaultWait * 0.9f;
                            ++attackPhase;
                        }
                        break;
                    case 4:
                        // perform some kind of attack
                        for(int i=0; i<3; ++i)
                        {
                            timer = 0f;
                            attacking = true;
                            switch(i)
                            {
                                case 0:
                                    StartCoroutine(fanSpray(1, 0.1f, 4, 19f, groundTargets[2],
                                    ProjectileType.Linear, ProjectileSpeed.Fast, 0.0f));
                                    break;
                                case 1:
                                    StartCoroutine(slam());
                                    break;
                                case 2:
                                    StartCoroutine(fanSpray(1, 0.1f, 5, -21f, groundTargets[2],
                                    ProjectileType.Linear, ProjectileSpeed.Med, 0.0f));
                                    break;
                                case 3:
                                    StartCoroutine(repeatFanProjectile(3, 0.3f, 3, 30f, player,
                                    ProjectileType.Linear, ProjectileSpeed.Slow, 3.0f));
                                    break;
                            }

                            while(attacking)    yield return null;
                            while(timer < waitTime)
                            {
                                timer += Time.deltaTime;
                                yield return null;
                            }
                        }
                        ++attackPhase;
                        break;
                    case 5:
                        // hand barrier down
                        waitTime = defaultWait;
                        StartCoroutine(handDown());
                        attackPhase = 0;
                        break;
                    case 10:
                        // statue hand
                        waitTime = defaultWait * 0.8f;
                        StartCoroutine(hand());
                        ++attackPhase;
                        break;
                    case 11:
                        // 3 slow fan
                        StartCoroutine(repeatFanProjectile(3, 0.5f, 5, 20f, player,
                            ProjectileType.Linear, ProjectileSpeed.Slow, -5f));
                        waitTime = defaultWait * 0.95f;
                        ++attackPhase;
                        break;
                    case 12:
                        // 6 shots away, 2x
                        StartCoroutine(fanSpray(2, 0.09f, 6, -19f, groundTargets[2],
                            ProjectileType.Linear, ProjectileSpeed.Fast, 0.0f));
                        waitTime = 0.7f;
                        ++attackPhase;
                        break;
                    case 13:
                        // slam, weak
                        StartCoroutine(fallToGround());
                        transform.parent.gameObject.SendMessage("changeMultiplier", 2f);
                        waitTime = 6f;
                        ++attackPhase;
                        break;
                    case 14:
                        // back to normal
                        StartCoroutine(rise());
                        transform.parent.gameObject.SendMessage("changeMultiplier", 1f);
                        waitTime = defaultWait;
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
                        StartCoroutine(lunge(player));
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
                attackPhase %= 4;   // Medium pattern to be reworked
            }

            yield return null;
        }
    }

    #endregion MediumPatterns

    #endregion AttackPatterns
}
