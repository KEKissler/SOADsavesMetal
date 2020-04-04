using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearShow : MonoBehaviour
{
    [SerializeField]
    GameObject Tear;
    [SerializeField]
    float timeTillDrop = 4;
    [SerializeField]
    float flashRate = .2f;
    float time;
    float flashTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        flashTime = Time.time;
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - flashTime >= flashRate)
        {
            flashTime = Time.time;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = !this.gameObject.GetComponent<SpriteRenderer>().enabled;
        }
        if(Time.time - time >= timeTillDrop)
        {
            if(Tear != null)
                Instantiate(Tear, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(this.gameObject);
        }
    }

    public void SetDrop(GameObject drop)
    {
        Tear = drop;
    }

    public void SetTimeTillDrop(float t)
    {
        timeTillDrop = t;
    }

    public void SetFlashRate(float fr)
    {
        flashRate = fr;
    }
}
 