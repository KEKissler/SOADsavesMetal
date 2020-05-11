using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject Spawnee;
    [SerializeField]
    float speed = 5;

    private void FixedUpdate()
    {
        transform.position = transform.position + new Vector3(speed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Instantiate(Spawnee, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
