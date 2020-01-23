using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecayOverflow : MonoBehaviour
{
    private Vector2 raisedPos;
    private Rigidbody2D rb;

    public float speed = 0.6f;

    private float travel_distance = 2.3f;

    // Start is called before the first frame update
    void Start()
    {
        if(speed <= 0.01f) speed = 1.5f;
        rb = GetComponent<Rigidbody2D>();
        raisedPos = transform.position;
        transform.position -= new Vector3(0, 2f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator flood(Animator FlowAnimator, string defaultFlowStateName)
    {
        float timer = 0f;
        rb.velocity = new Vector2(0, speed);
        while(timer < travel_distance/speed)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        //liquid fully up, stop the flow animation
        FlowAnimator.Play(defaultFlowStateName);

        timer = 0f;
        rb.velocity = new Vector2(0, 0);
    }
}
