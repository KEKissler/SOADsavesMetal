using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBase : MonoBehaviour
{
    // TEMPORARY TEST CODE
    private bool attacking;
	private float attackTimer = 0.0f;

    // Prefabs
    public GameObject snakeBody;
    public GameObject snakeHead;
    public GameObject bodyAnchor;
    public GameObject projectile;
    
    // Public vars
    public int bodyLength = 7;

    // Internal vars
    GameObject[] body;
    Rigidbody2D[] bodyRB;
    Rigidbody2D rb;
    GameObject[] anchor;
    bool anchorsEnabled = true;
    bool bodyRotationEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        buildSnake();
    }

    // Update is called once per frame
    void Update()
    {
        if(!attacking)
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                StartCoroutine(slam());
            }

            if(Input.GetKey(KeyCode.S))
            {
                StartCoroutine(spit());
            }
        }
    }

    public void buildSnake()
    {
        body = new GameObject[bodyLength];
        bodyRB = new Rigidbody2D[bodyLength];
        anchor = new GameObject[bodyLength];
        for(int i=0; i<bodyLength-1; ++i)
        {
            body[i] = Instantiate(snakeBody, bodyPosition(i+1), Quaternion.identity);
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

            anchor[i] = Instantiate(bodyAnchor, bodyPosition(i+1), Quaternion.identity);
            anchor[i].GetComponent<SpringJoint2D>().connectedBody = bodyRB[i];
        }
        
        body[bodyLength-1] = Instantiate(snakeHead, bodyPosition(bodyLength), Quaternion.identity);
        body[bodyLength-1].GetComponent<SpringJoint2D>().connectedBody = body[bodyLength-2].GetComponent<Rigidbody2D>();
        bodyRB[bodyLength-1] = body[bodyLength-1].GetComponent<Rigidbody2D>();
        body[bodyLength-1].GetComponent<SnakeRotationLock>().target = body[bodyLength-2].transform;

        anchor[bodyLength-1] = Instantiate(bodyAnchor, bodyPosition(bodyLength), Quaternion.identity);
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
        while(timer < 0.7f)
        {
            for(int i=0; i<bodyLength; ++i) bodyRB[i].AddForce(new Vector2(-29.3f+13f*timer-9.9f*(float)i, -11.3f-19f*timer-3.1f*(float)i));
            timer += Time.deltaTime;
            yield return null;
        }

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

        Instantiate(projectile, transform.position, Quaternion.identity);

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
}
