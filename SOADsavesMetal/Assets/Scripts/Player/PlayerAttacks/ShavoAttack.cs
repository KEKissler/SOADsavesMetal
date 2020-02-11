using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShavoAttack : MonoBehaviour
{
    private Player ps;

	private bool attacking;
	private float attackTimer = 0.0f; // General purpose timer

	// Short range attack
    public GameObject shortRangeHitbox;
    private const float SHORT_ATTACK_WINDUP = 0.25f;
    private const float SHORT_ATTACK_HOLD_DURATION = 0.25f;
    private const float SHORT_ATTACK_COOLDOWN = 0.01f;
    
    // Long range attack
    public GameObject musicNote;
    private const float LONG_ATTACK_WINDUP = 0.1f;
    private const float LONG_ATTACK_COOLDOWN = 0.27f;
    private System.Random rand;

    // Super attack
    public GameObject rocks;
    
	// Use this for initialization
	void Start () {
        ps = GameObject.FindWithTag("Player").GetComponent<Player>();
        shortRangeHitbox.SetActive(false);
        attacking = false;
        rand = new System.Random();
	}

	// Update is called once per frame
	void Update ()
	{
		if(!attacking)
		{
			if(Input.GetKey(KeyCode.Z))
			{
				StartCoroutine(AttackShort());
			}
			else if(Input.GetKey(KeyCode.X))
			{
				StartCoroutine(AttackLong());
			}
            else if(Input.GetKey(KeyCode.C))
            {
                StartCoroutine(AttackSuper());
            }
		}
	}

    IEnumerator AttackShort()
    {
        attacking = true;

        // Windup period
        attackTimer = 0.0f;
        while(attackTimer < SHORT_ATTACK_WINDUP)
        {
            attackTimer += Time.deltaTime;
            yield return null;
        }

        // Attack
        shortRangeHitbox.SetActive(true);
        yield return null;
        attackTimer = 0.0f;
        while(attackTimer < SHORT_ATTACK_HOLD_DURATION)
        {
        	attackTimer += Time.deltaTime;
        	yield return null;
        }
        yield return null;
        shortRangeHitbox.SendMessage("refreshHit");
        shortRangeHitbox.SetActive(false);

        // Cooldown period
        attackTimer = 0.0f;
        while(attackTimer < SHORT_ATTACK_COOLDOWN)
        {
        	attackTimer += Time.deltaTime;
        	yield return null;
        }

        attacking = false;
    }

    IEnumerator AttackLong()
    {
    	attacking = true;

        attackTimer = 0.0f;
        while(attackTimer < LONG_ATTACK_WINDUP)
        {
            attackTimer += Time.deltaTime;
        	yield return null;     
        }

    	// Create projectile (musicNote)
    	GameObject mNote = Instantiate(musicNote, transform.parent.position, Quaternion.identity);
        MusicNote mnScript = mNote.GetComponent<MusicNote>();
        mnScript.playerRotationY = transform.parent.rotation.y;
        mnScript.startTime = (float)rand.NextDouble() * 2f;
        mnScript.oscillationPeriod = (float)rand.NextDouble() * 2f + 2f;
        mnScript.setSpeed((float)rand.NextDouble() * 2f + 5.8f);
        mNote.SetActive(true);

        // Cooldown period
        attackTimer = 0.0f;
        while(attackTimer < LONG_ATTACK_COOLDOWN)
        {
        	attackTimer += Time.deltaTime;
        	yield return null;
        }

    	attacking = false;
    }

    IEnumerator AttackSuper()
    {
        attacking = true;

        while (ps.blockAttackProgress) yield return null;

        GameObject superRocksParent = Instantiate(rocks, transform.position + new Vector3(0.35f, -1.5f, 0), Quaternion.identity);
        List<Rigidbody2D> rbRocks = new List<Rigidbody2D>();
        foreach (Transform child in superRocksParent.transform)
        {
            rbRocks.Add(child.gameObject.GetComponent<Rigidbody2D>());
        }

        foreach (Rigidbody2D rb in rbRocks)
        {
            rb.velocity = new Vector2(1.3f, 7.2f);
        }

        yield return new WaitForSeconds(0.05f);
        
        while (ps.blockAttackProgress) yield return null;

        foreach (Rigidbody2D rb in rbRocks)
        {
            rb.velocity = new Vector2(17f + 2.5f * (float)rand.NextDouble() + 2.5f * (float)rand.NextDouble(),
                3.5f + 3.5f * (float)rand.NextDouble() + 3f * (float)rand.NextDouble());
        }

        while (ps.attacking) yield return null;

        attacking = false;
    }
}
