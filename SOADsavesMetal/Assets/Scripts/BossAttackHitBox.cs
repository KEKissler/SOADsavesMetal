using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BossAttackHitBox : MonoBehaviour
{
    private Collider2D hitbox;
    private GameObject dummy;

    void Start()
    {
        hitbox = GetComponent<Collider2D>();
        dummy = GameObject.Find("Dummy");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!dummy)
        {
            if (other.gameObject.tag == "Player")
            {
                Player p = other.gameObject.GetComponent<Player>();
                if (p == null)
                {
                    p = other.gameObject.GetComponentInParent<Player>();
                }
                p.DamagePlayer();
            }
        }
    }
}
