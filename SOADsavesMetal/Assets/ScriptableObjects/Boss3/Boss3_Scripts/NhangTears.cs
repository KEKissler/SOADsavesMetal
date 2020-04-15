using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NhangTears : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(other.gameObject.GetComponent<Player>() != null)
            {
                other.gameObject.GetComponent<Player>().DamagePlayer();
                Destroy(this.gameObject);
            }

        }
        else if(other.gameObject.tag == "Floor"){
            Destroy(this.gameObject);
        }
    }
}
