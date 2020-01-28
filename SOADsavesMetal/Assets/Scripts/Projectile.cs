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

public class Projectile : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;

    private const float SLOW_VEL = 5.0f;
    private const float MED_VEL = 6.7f;
    private const float FAST_VEL = 9.0f;

    // Start is called before the first frame update
    void Start()
    {   
        if(!player) player = GameObject.FindWithTag("Player");
        transform.LookAt(new Vector3(transform.position.x, transform.position.y, 1),
            new Vector3(transform.position.x-player.transform.position.x, transform.position.y-player.transform.position.y, 0));

        rb = GetComponent<Rigidbody2D>();
        
        //If the projectile has no rigidbody it can still do damage on tigger enter.
        if(rb != null)
            Configure(player, ProjectileType.Gravity, ProjectileSpeed.Fast, 0.0f);
        
        Destroy(gameObject, 3.6f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //why is every unity project does this show up.
    private float PosMod(float angle)
    {
        var newAngle = Mathf.Rad2Deg * angle % 360;
        newAngle = newAngle < 0 ? newAngle + 360 : newAngle;
        return newAngle * Mathf.Deg2Rad;
    }

    public void Configure(GameObject target, ProjectileType pt, ProjectileSpeed ps, float degreeModifier, float? presetAngle = null)
    {
        float a = target.transform.position.x-transform.position.x;
        float b = target.transform.position.y-transform.position.y;
        float angle = Mathf.Atan2(b, a) + degreeModifier * Mathf.Deg2Rad;
        if (presetAngle != null)
        {
            angle = presetAngle.Value * Mathf.Deg2Rad;
        }
        angle = PosMod(angle);

        Quaternion newRotation = new Quaternion();
        newRotation.eulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg + 90);
        transform.rotation = newRotation;

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

        rb.velocity = new Vector2(a, b);

        switch(pt)
        {
            case ProjectileType.Linear:
                rb.gravityScale = 0f;
                break;
            case ProjectileType.Gravity:
                rb.gravityScale = 0.24f;
                rb.velocity += new Vector2(0, 1.2f);
                break;
            case ProjectileType.Honing:
                // Temporary patch
                rb.gravityScale = 0f;
                break;
            default:
                rb.gravityScale = 0f;
                break;
        }
    }

    //This handles destroying boss projectiles when they hit a player
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().Health -= 1;
            Debug.Log(gameObject.name);
            Destroy(this.gameObject);
        }
    }
}
