using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialDummy : MonoBehaviour
{
    [Header("Attack Hit")]
    public bool shortHit = false;
    public bool longHit = false;
    public bool superHit = false;

    //used to recognize what attach hit
    [Header("Attack Names")]
    [SerializeField] private string shortName;
    [SerializeField] private string longName;
    [SerializeField] private string superName;

    [Header("Attack Checks")]
    public bool shortCheck = false;
    public bool longCheck = false;
    public bool superCheck = false;
    Vector3 startingPos;
    private bool doIShake = false;

    void Awake()
    {
        shortHit = false;
        longHit = false;
        superHit = false;
        shortCheck = false;
        longCheck = false;
        superCheck = false;
        startingPos = transform.position;
    }

    void Start()
    {

    }
    void OnTriggerEnter2D(Collider2D other) //"ShortRange", "Drumstick(Clone)" and Projectile, "Cymbal(Clone)"
    {
        Debug.Log(other + " and " + other.tag);
        if(other.name == shortName && shortCheck)
        {
            shortHit = true;
            gameObject.GetComponent<ParticleSystem>().Play();
            StartCoroutine(Shake(0.1f, 0.05f));
        }
        if(other.name == longName && longCheck)
        {
            longHit = true;
            gameObject.GetComponent<ParticleSystem>().Play();
            StartCoroutine(Shake(0.1f, 0.05f));
        }
        if(other.name == superName && superCheck)
        {
            superHit = true;
            gameObject.GetComponent<ParticleSystem>().Play();
            StartCoroutine(Shake(0.1f, 0.05f));
        }
    }

    void FixedUpdate()
    {
        
    }

    IEnumerator Shake(float duration, float strength)
    {
        float elapsed = 0f;
        while(elapsed < duration)
        {
            float xPos = startingPos.x + Random.Range(-1f,1f) * strength;
            float yPos = startingPos.y + Random.Range(-1f,1f) * strength;
            transform.position = new Vector3(xPos,yPos, startingPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = startingPos;
    }
}
