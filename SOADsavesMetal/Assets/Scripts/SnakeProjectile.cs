using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType
{
    Linear,
    Gravity,
    Honing
};

public enum ProjectileSpeed
{
    Stop,
    Slow,
    Med,
    Fast
};

public class SnakeProjectile : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;

    private const float SLOW_VEL = 7.5f;
    private const float MED_VEL = 10.5f;
    private const float FAST_VEL = 13.5f;

    // Start is called before the first frame update
    void Start()
    {   
        if(!player) player = GameObject.FindWithTag("Player");
        transform.LookAt(new Vector3(transform.position.x, transform.position.y, 1),
            new Vector3(transform.position.x-player.transform.position.x, transform.position.y-player.transform.position.y, 0));

        rb = GetComponent<Rigidbody2D>();
        Configure(ProjectileType.Gravity, ProjectileSpeed.Fast, 0.0f);
        
        Destroy(gameObject, 2.6f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Configure(ProjectileType pt, ProjectileSpeed ps, float degreeModifier)
    {
        float a = player.transform.position.x-transform.position.x;
        float b = player.transform.position.y-transform.position.y;
        float angle = Mathf.Atan2(b, a) + degreeModifier*Mathf.Deg2Rad;
        float v;

        switch(ps)
        {
            case ProjectileSpeed.Stop:
                v = 0f;
                break;
            case ProjectileSpeed.Slow:
                v = SLOW_VEL;
                break;
            case ProjectileSpeed.Med:
                v = MED_VEL;
                break;
            case ProjectileSpeed.Fast:
                v = FAST_VEL;
                break;
            default:
                v = MED_VEL;
                break;
        }
        
        a = Mathf.Cos(angle)*v;
        b = Mathf.Sin(angle)*v;
        // float m = (transform.position-player.transform.position).magnitude;
        rb.velocity = new Vector2(a, b);

        switch(pt)
        {
            case ProjectileType.Linear:
                rb.gravityScale = 0f;
                break;
            case ProjectileType.Gravity:
                rb.gravityScale = 0.5f;
                rb.velocity += new Vector2(0, 2f);
                break;
            case ProjectileType.Honing:
                break;
            default:
                rb.gravityScale = 0f;
                break;
        }
    }
}
