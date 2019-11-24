using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Platform : MonoBehaviour
{
    public Transform Player;
    public float ActivateThreshold;
    public float DeactivateThreshold;

    private CapsuleCollider2D platformCollider;

    // Start is called before the first frame update
    void Start()
    {
        platformCollider = GetComponent<CapsuleCollider2D>();
        Player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        var abovePlatform = Player.position.y - platformCollider.bounds.max.y > ActivateThreshold;
        var belowPlatform = Player.position.y - platformCollider.bounds.max.y < DeactivateThreshold;
        if(abovePlatform && !platformCollider.enabled)
        {
            platformCollider.enabled = true;
        }
        else if (belowPlatform && platformCollider.enabled)
        {
            platformCollider.enabled = false;
        }
    }
}
