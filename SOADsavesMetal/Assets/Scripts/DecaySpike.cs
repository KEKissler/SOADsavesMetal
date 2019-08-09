using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecaySpike : MonoBehaviour
{
    public GameObject indicator;

    private Rigidbody2D rb;
    private float travel_distance = 2.8f;
    
    // Maybe have it move more slowly near start or end of motion?
    public float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        indicator.SetActive(false);
        // if(speed <= 0.01f) speed = 1.5f;
        // StartCoroutine(attack());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator attack()
    {
        indicator.SetActive(true);
        float timer = 0f;
        while(timer < 1.8f)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        indicator.SetActive(false);

        timer = 0f;
        rb.velocity = new Vector2(0, speed);
        while(timer < travel_distance/speed)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0f;
        rb.velocity = new Vector2(0, 0);
        while(timer < 0.7f)
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
        // Destroy(gameObject, 1f);
    }
}
