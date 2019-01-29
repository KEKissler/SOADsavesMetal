using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumStick : MonoBehaviour {

    public Rigidbody2D rb;
    public bool started;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        started = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (started)
        {
            rb.velocity = new Vector2(1.0f, 0.0f);
        }
	}

    public void Fire()
    {
        if (!started)
        {
            started = true;
            StartCoroutine("fireStick");
        }
    }

    public IEnumerator fireStick()
    {
        yield return new WaitForSeconds(1.0f);
        started = false;
    }


}
