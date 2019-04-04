using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBase : MonoBehaviour
{
    // TEMPORARY TEST CODE
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

    // Internal vars
    Rigidbody2D[] bodyRB;
    Rigidbody2D rb;
    GameObject[] anchor;
    bool anchorsEnabled = true;
    bool bodyRotationEnabled = true;
    private int bodyLength;

    // Start is called before the first frame update
    void Start()
    {
        bodyLength = body.Length;
        rb = GetComponent<Rigidbody2D>();
        buildSnake();
        statueHand.transform.position = (Vector2)transform.position + new Vector2(-5f, -3.4f);
        indicator.transform.position = (Vector2)transform.position + new Vector2(-5f, -1.5f);
        StartCoroutine(basicPattern());
    }

    // Update is called once per frame
    void Update()
    {
        if(false)
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                StartCoroutine(slam());
            }

            if(Input.GetKey(KeyCode.S))
            {
                StartCoroutine(spit());
            }

            if(Input.GetKeyDown(KeyCode.D))
            {
                StartCoroutine(hand());
            }

            if(Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(lunge());
            }
        }
    }

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
                body[0].GetComponent<SnakeRotationLock>().target = transform;
            }
            else
            {
                body[i].GetComponent<SpringJoint2D>().connectedBody = body[i-1].GetComponent<Rigidbody2D>();
                body[i].GetComponent<SnakeRotationLock>().target = body[i-1].transform;
            }
            bodyRB[i] = body[i].GetComponent<Rigidbody2D>();

            anchor[i] = Instantiate(bodyAnchor, body[i].transform.position, Quaternion.identity);
            anchor[i].GetComponent<SpringJoint2D>().connectedBody = bodyRB[i];
        }
        
        // body[bodyLength-1] = Instantiate(snakeHead, bodyPosition(bodyLength), Quaternion.identity);
        body[bodyLength-1].GetComponent<SpringJoint2D>().connectedBody = body[bodyLength-2].GetComponent<Rigidbody2D>();
        bodyRB[bodyLength-1] = body[bodyLength-1].GetComponent<Rigidbody2D>();
        body[bodyLength-1].GetComponent<SnakeRotationLock>().target = body[bodyLength-2].transform;

        anchor[bodyLength-1] = Instantiate(bodyAnchor, body[bodyLength-1].transform.position, Quaternion.identity);
        anchor[bodyLength-1].GetComponent<SpringJoint2D>().connectedBody = bodyRB[bodyLength-1];
    }

    Vector2 bodyPosition(int partIndex)
    {
        return new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.75f * partIndex);
    }

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
        attacking = true;
        toggleAnchors();
        // toggleRotation();
        float timer = 0f;
        while(timer < 0.7f)
        {
            for(int i=0; i<bodyLength-1; ++i) bodyRB[i].AddForce(new Vector2(5.3f-4.9f*timer+6.2f*(float)i, 0.9f-2.5f*timer-0.4f*(float)i));
            bodyRB[bodyLength-1].AddForce(new Vector2(-259.3f-485f*timer-3.9f*(float)(bodyLength-1), -20.3f-24f*timer-2.2f*(float)(bodyLength-1)));
            timer += Time.deltaTime;
            yield return null;
        }

        for(int i=0; i<bodyLength; ++i) bodyRB[i].velocity = new Vector2(-10f, 0);
        bodyRB[bodyLength-1].drag = 999999f;

        timer = 0f;
        while(timer < 0.18f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        bodyRB[bodyLength-1].drag = 2f;
        bodyRB[bodyLength-1].velocity = new Vector2(1f, 6f);
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

    IEnumerator spit()
    {
        attacking = true;

        GameObject temp = Instantiate(projectile, body[bodyLength-1].transform.position, Quaternion.identity);
        //temp.GetComponent<SnakeProjectile>().Configure(ProjectileType.Gravity, ProjectileSpeed.Med);

        attackTimer = 0.0f;
        while(attackTimer < 0.1f)
        {
            attackTimer += Time.deltaTime;
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

    IEnumerator hand()
    {
        attacking = true;
        indicator.SetActive(true);
        float timer = 0.0f;
        Rigidbody2D statueRB = statueHand.GetComponent<Rigidbody2D>();

        while(timer < 1.0f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        indicator.SetActive(false);
        timer = 0.0f;
        statueRB.velocity = new Vector2(0, 2.6f);

        while(timer < 0.76f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        statueRB.velocity = new Vector2(0, 0);
        timer = 0.0f;

        while(timer < 0.25f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        statueRB.velocity = new Vector2(0, -2.6f);
        timer = 0.0f;

        while(timer < 0.76f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        statueRB.velocity = new Vector2(0, 0);
        timer = 0.0f;

        while(timer < 0.5f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        attacking = false;

    }


    // Attack patterns

    IEnumerator basicPattern()
    {
        float timer = 0f;
        int attackPhase = 0;    // Replace with enum later
        float waitTime = 2.5f;

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
                        // 3 projectiles
                        StartCoroutine(repeatProjectile(6, 0.3f));
                        break;
                    case 1:
                        // lunge
                        StartCoroutine(lunge());
                        break;
                    case 2:
                        // fan shape projectiles
                        StartCoroutine(fanProjectile(5, 1.0f));
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

    IEnumerator repeatProjectile(int numRepeats, float waitTime)
    {
        //attacking = true;
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
        }
        attacking = false;
    }

    IEnumerator fanProjectile(int numProjectiles, float yVelocityModifier)
    {
        if(numProjectiles < 1)  numProjectiles = 1;
        float yv = -yVelocityModifier*0.5f*(numProjectiles-1);

        GameObject[] temp = new GameObject[numProjectiles];

        for(int i=0; i<numProjectiles; ++i)
        {
            temp[i] = Instantiate(projectile, body[bodyLength-1].transform.position, Quaternion.identity);
        }

        yield return null;

        // Add sinusoidal xVel modifier
        for(int i=0; i<numProjectiles; ++i)
        {
            temp[i].GetComponent<SnakeProjectile>().Configure(ProjectileType.Gravity, ProjectileSpeed.Fast, 0.0f, yv);
            yv += yVelocityModifier;
        }

        yield return null;
    }
}
