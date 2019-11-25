using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BossAttackHitBox : MonoBehaviour
{
    private Collider2D hitbox;

    void Start()
    {
        hitbox = GetComponent<Collider2D>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().Health -= 1;
            Debug.Log(gameObject.name);
        }
    }
}
