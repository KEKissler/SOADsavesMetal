using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Platform : MonoBehaviour
{
    public Transform Player;
    public float heightThreshold;

    private BoxCollider2D platformCollider;

    // Start is called before the first frame update
    void Start()
    {
        platformCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var abovePlatform = Player.position.y - platformCollider.bounds.max.y > heightThreshold;
        platformCollider.enabled = abovePlatform;
    }
}
