using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerjTable : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool isBigTable;
    private float speed, accel;
    private ScreenShake shaker;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 0f;
        StartCoroutine(ChangeTableSpeed());
        shaker = GameObject.FindWithTag("MainCamera").GetComponent<ScreenShake>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ChangeTableSpeed()
    {
        while (true)
        {
            if (transform.position.y < 0.1f)
            {
                speed = 0f;
                shaker.shake();
                Destroy(gameObject, 0.75f);
                rb.velocity = new Vector2(0f, 0f);
                if (isBigTable)
                {
                    yield return new WaitForSeconds(0.2f);
                    shaker.shake();
                    //yield return new WaitForSeconds(0.2f);
                    //shaker.shake();
                }

                break;
            }
            else
            {
                accel += 0.3f;
                speed += Mathf.Pow(accel, 1.5f) + 0.15f;
            }
            rb.velocity = transform.right * speed;
            yield return new WaitForSeconds(0.07f);
        }
    }
}
