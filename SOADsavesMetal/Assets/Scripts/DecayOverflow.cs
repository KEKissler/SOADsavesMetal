using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecayOverflow : MonoBehaviour
{
    private Vector2 raisedPos;
    private Rigidbody2D rb;

    public float speed = 1.0f;

    private float travel_distance = 2f;

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

    public IEnumerator flood()
    {
        float timer = 0f;
        rb.velocity = new Vector2(0, speed);
        while(timer < travel_distance/speed)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0f;
        rb.velocity = new Vector2(0, 0);
        while(timer < 2f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0f;
        rb.velocity = new Vector2(0, -speed);
        while(timer < travel_distance/speed)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        rb.velocity = new Vector2(0, 0);
    }
}
