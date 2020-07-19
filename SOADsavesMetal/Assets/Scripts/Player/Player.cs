using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Default Player Variables
    [Header("Player General Properties")]
    public string currentBandMember;
    public bool Dead { get { return health < 1; } }
    public int Health { get { return health; } set { health = value; } }
    public Rigidbody2D rb;
    public float jumpHeight;
    public int startingHealth;
    private int health;
    public float curInvulnerableTime;
    public float invulnerabilityDuration = 3f;
    public float maxGroundSpeed, groundAccel, groundDecel, groundFrictionDecel;
    public float maxAirSpeed, airAccel, airDecel, airFrictionDecel;

    private SpriteRenderer sr, srLegs;

    //Player Hitbox Variables
    [Header("Player Hitbox Variables")]
    public GameObject upperBodyHitbox;
    public GameObject shortRangeHitbox;
    public GameObject stick;
    private BoxCollider2D lowerBodyHitbox;
    private Vector2 lowOriginalOffset, lowOriginalSize;

    //Player State
    [Header("Player State")]
    public int remainingJumps;
    public bool crouched;
    public bool inAir;
    // private bool landing;
    public bool attacking;
    public float superMeterCharge; // Ranges between 0 to maxSuperCharge
    public float maxSuperCharge = 100f;
    private float crouchDistance = 0.24f;
    public bool isSuperActive;
    public bool blockHorizontalMovement;
    public bool blockNormalJumpAnims;
    public bool moving;
    public bool movedWhileAttacking;
    public bool isDead = false;
    public bool deathStarted;
    public bool listeningForDoubleDownTap;
    public bool daronListeningForParry;
    public bool serjFlightActive;

    //Attack state blocker
    public bool blockAttackProgress;

    //Player Control Components
    public ControlSchemes controlSchemes;
    private KeyCode up;
    private KeyCode down;
    private KeyCode CAttack;
    private KeyCode RAttack;
    private KeyCode SAttack;
    private KeyCode pause;
    private string hori;
    private string vert;


    //Scripts
    private PlayerHorizontalMovement phm;
    private PlayerJump pj;
    private PlayerAttackManager pam;

    //Scripts to identify if in countdown or paused		
    public GameplayPause gameplayPause;
    public CountDown countDown;
    public DeathMenu deathMenu;

    //Player Animation Variables
    [Header("Player Animation Variables")]
    public GameObject playerUpperBody;
    public GameObject playerLowerBody;
    public Animator playerUpperAnim;
    public Animator playerLowerAnim;
    public Animator shortRange;
    private PlayerAttackAnims paa;
    private Slider superBar;
    private Image superBarGlow;
    public GameObject serjWings;

    public Platform[] platforms;

    [HideInInspector]
    public float desiredVelocity;
    private bool performFriction;

    #region FMODEvents
    [Header("General Events")]
    [FMODUnity.EventRef]
    public string playerJump;
    [FMODUnity.EventRef]
    public string playerHit;
    [FMODUnity.EventRef]
    public string playerDeath;
    [FMODUnity.EventRef]
    public string playerSuperReady;


    [Header("John Events")]
    [FMODUnity.EventRef]
    public string johnJump;
    [FMODUnity.EventRef]
    public string johnShortRange;
    [FMODUnity.EventRef]
    public string johnLongRange;
    [FMODUnity.EventRef]
    public string johnSuper;

    [Header("Shavo Events")]
    [FMODUnity.EventRef]
    public string shavoDash;
    [FMODUnity.EventRef]
    public string shavoShortRange;
    [FMODUnity.EventRef]
    public string shavoLongRange;
    [FMODUnity.EventRef]
    public string shavoSuper;

    [Header("Daron Events")]
    [FMODUnity.EventRef]
    public string daronTeleport;
    [FMODUnity.EventRef]
    public string daronShortRange;
    [FMODUnity.EventRef]
    public string daronLongRange;
    [FMODUnity.EventRef]
    public string daronSuper;

    [Header("Serj Events")]
    [FMODUnity.EventRef]
    public string serjShortRange;
    [FMODUnity.EventRef]
    public string serjLongRange;
    [FMODUnity.EventRef]
    public string serjFlyStart;
    [FMODUnity.EventRef]
    public string serjFlyMid;
    [FMODUnity.EventRef]
    public string serjFlyEnd;
    [FMODUnity.EventRef]
    public string serjSuper;

    #endregion

    public string GetAnimName(string animSuffix)
    {
        return currentBandMember + animSuffix;
    }

    public string GetHori()
    {
        return hori;
    }

    public KeyCode GetUp()
    {
        return up;
    }

    public KeyCode GetPause()
    {
        return pause;
    }

    public void SetCurrentBandMember(string newBandMember)
    {
        pam = playerUpperBody.GetComponentInChildren<PlayerAttackManager>();
        currentBandMember = newBandMember;
        StartCoroutine(pam.SetPlayerName());
    }

    public void PlayAudioEvent(string fmodEvent)
    {
        EventInstance instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();
    }

    // Handles a common use case regarding playing body and leg animations.
    public void PlayAnims(string animSuffix)
    {
        if (!attacking)
        {
            playerUpperAnim.Play(GetAnimName(animSuffix));
        }
        playerLowerAnim.Play(GetAnimName(animSuffix + "Legs"));
    }

    private void Awake()
    {
        desiredVelocity = 0;
    }

    void Start()
    {
        // Initialize player state
        health = startingHealth;
        //health = 2;
        moving = false;
        attacking = false;
        deathStarted = false;
        movedWhileAttacking = false;
        isSuperActive = false;
        inAir = false;
        crouched = false;
        listeningForDoubleDownTap = false;
        remainingJumps = 1;
        superMeterCharge = 0f;
        superBar = GameObject.Find("Super Bar").GetComponent<Slider>();
        superBarGlow = superBar.transform.GetChild(0).GetComponent<Image>();
        superBar.maxValue = maxSuperCharge;
        superBarGlow.color = new Color(0.8784314f, 1f, 0f, 0f);
        blockNormalJumpAnims = false;
        daronListeningForParry = false;
        serjFlightActive = false;
        performFriction = false;

        // Player-specific attack
        blockAttackProgress = true;

        // Get components from gameobjects
        sr = transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
        srLegs = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();

        rb = gameObject.GetComponent<Rigidbody2D>();
        shortRange = shortRangeHitbox.GetComponent<Animator>();
        lowerBodyHitbox = gameObject.GetComponent<BoxCollider2D>();
        lowOriginalOffset = lowerBodyHitbox.offset;
        lowOriginalSize = lowerBodyHitbox.size;

        // Configure other player scripts
        paa = playerUpperBody.GetComponentInChildren<PlayerAttackAnims>();
        phm = GetComponent<PlayerHorizontalMovement>();
        pj = GetComponent<PlayerJump>();
        paa.ps = this;
        phm.ps = this;
        pj.ps = this;

        // Empty check on bandmember name
        if (currentBandMember == "")
        {
            currentBandMember = "John";
        }

        //Get the starting control scheme
        updateControlScheme();

    }

    public AudioClip GetRandomSoundEffect(AudioClip[] array)
    {
        return array[Random.Range(0, array.Length)];
    }

    public bool FacingLeft()
    {
        return transform.rotation.y != 0f;
    }

    void Update() {
        //Debug.Log("crouched " + crouched);
        //Debug.Log("inair " + inAir);
        //stops player from being able to move if in pause or countdown

        if ((countDown == null || !countDown.getCountDown()) && (gameplayPause == null || !gameplayPause.getPaused()) /*&& (!countDown.isFading())*/)
        {
            if (!Dead)
            {
                performFriction = true;

                #region Super meter charge
                // Uncomment the following line for instant meter recharge
                //superMeterCharge += maxSuperCharge;
                // Passive meter charge, maybe vary by character
                superMeterCharge += maxSuperCharge / 100f * Time.deltaTime;
                if (superMeterCharge > maxSuperCharge) superMeterCharge = maxSuperCharge;
                // Debug.Log("meter charge " + superMeterCharge);
                superBar.value = superMeterCharge;
                if (superBar.value >= 100.0f){ //Superbar UI slider glow fades in (quickly) when super meter is full
                    if (superBarGlow.color.a < 1.0f) //until the glow is fully faded in
                        superBarGlow.color = new Color(0.8784314f, 1f, 0f, superBarGlow.color.a+0.05f); 
                    //modify last value ^^^ for the fade in rate
                }
                else //otherwise the glow is not present unless super meter is full
                    superBarGlow.color = new Color(0.8784314f, 1f, 0f, 0f);

                #endregion Super meter charge

                #region Falling and jumping animations
                if (!blockHorizontalMovement && !blockNormalJumpAnims)  // Or any other special condition is in effect
                {
                    if (rb.velocity.y < -0.5f)
                    {
                        // landing = true;
                        if (currentBandMember == "John" || !isSuperActive)
                            PlayAnims("Fall");
                        lowerBodyHitbox.offset = lowOriginalOffset;
                        lowerBodyHitbox.size = lowOriginalSize;
                    }
                    if (rb.velocity.y > 0.5)
                    {
                        if (currentBandMember == "John" || !isSuperActive)
                            PlayAnims("Jump");
                        lowerBodyHitbox.offset = new Vector2(lowOriginalOffset.x, -0.05f);
                        lowerBodyHitbox.size = new Vector2(lowOriginalSize.x, 0.34f);
                    }
                }
                #endregion Falling and jumping animations

                #region Crouching
                if ((Input.GetKey(down) || (!Input.GetKey(up) && Input.GetAxisRaw(vert) > 0.5)) && !inAir
                    && (currentBandMember == "John" || !isSuperActive)) // This is a hacky fix
                { // This line used to have !attacking
                    if(!crouched) playerUpperBody.transform.localPosition = new Vector3(0, -crouchDistance);
                    crouched = true;
                    PlayAnims("Crouch");
                    upperBodyHitbox.SetActive(false);
                    if (!listeningForDoubleDownTap)
                    {
                        listeningForDoubleDownTap = true;
                        StartCoroutine(listenForDoubleDownTap());
                    }
                }
                else
                {
                    if (crouched)
                    {
                        playerUpperBody.transform.localPosition = new Vector3(0, 0);
                        playerLowerAnim.Play(GetAnimName("IdleLegs"));
                    }
                    crouched = false;
                    playerUpperBody.transform.localPosition = new Vector3(0, 0);
                    upperBodyHitbox.SetActive(true);
                }
                #endregion Crouching

                pj.HandleJump();

                #region Attacks
                //Z: Short Range Attack    X: Long Range Attack    C: Super Attack
                if (Input.GetKeyDown(CAttack) && !attacking)
                {
                    StartCoroutine(paa.shortRangeAttackAnims());
                    StartCoroutine(pam.pa.AttackShort());
                }
                else if (Input.GetKey(RAttack) && !attacking)
                {
                    StartCoroutine(paa.longRangeAttackAnims());
                    StartCoroutine(pam.pa.AttackLong());
                }
                else if (Input.GetKeyDown(SAttack) && !attacking &&
                        !isSuperActive && superMeterCharge >= maxSuperCharge)
                {
                    superMeterCharge = 0f;
                    StartCoroutine(paa.superAttackAnims());
                    StartCoroutine(pam.pa.AttackSuper());
                }
                #endregion Attacks

                phm.HandleHorizontalMovement();

                #region Serj flight vertical movement
                if (serjFlightActive)
                {
                    if (Input.GetKey(up))
                    {
                        rb.velocity = new Vector2(rb.velocity.x, maxAirSpeed * 0.85f);
                    }
                    else if (Input.GetKey(down))
                    {
                        rb.velocity = new Vector2(rb.velocity.x, -maxAirSpeed * 0.85f);
                    }
                }
                #endregion

                #region Serj wing animations
                if (serjFlightActive)
                {
                    serjWings.GetComponent<SpriteRenderer>().enabled = true;
                }
                else
                {
                    serjWings.GetComponent<SpriteRenderer>().enabled = false;
                }
                #endregion

                #region Invulnerability Timer Tick
                if (curInvulnerableTime > 0f)
                {
                    curInvulnerableTime -= Time.deltaTime;
                    if (curInvulnerableTime < 0f) curInvulnerableTime = 0f;
                }
                #endregion
            }
            else
            {
                // Death animation
                if (!deathStarted)
                {
                    isDead = true;
                    StartCoroutine("Kill");
                    deathStarted = true;

                    // Serj edge case
                    if (serjFlightActive)
                    {
                        rb.gravityScale = 5f; // If gravityScale on prefab changes, this is no longer correct
                        rb.drag = 0f;
                        serjFlightActive = false;
                        PlayAudioEvent(serjFlyEnd);
                        serjWings.GetComponent<SpriteRenderer>().enabled = false;
                    }
                }
            }
        }
        else if(!Dead)
        {
            PlayAnims("Idle");
        }
        else
        {
            PlayAnims("Death");
        }
    }

    #region Friction
    private void FixedUpdate()
    {
        if(performFriction)
        {
            float speedReductionThisFrame;
            float frictionMultiplier = 1f;
            if (isSuperActive) frictionMultiplier = 0.55f;
            else if (crouched) frictionMultiplier = 0.33f;
            if (inAir)
                speedReductionThisFrame = Time.deltaTime * airFrictionDecel * frictionMultiplier;
            else
                speedReductionThisFrame = Time.deltaTime * groundFrictionDecel * frictionMultiplier;
            if (Mathf.Abs(rb.velocity.x - desiredVelocity) > speedReductionThisFrame)
            {
                rb.velocity += new Vector2(-1 * Mathf.Sign(rb.velocity.x - desiredVelocity) * speedReductionThisFrame, 0);
            }
            else
            {
                rb.velocity = new Vector3(desiredVelocity, rb.velocity.y, 0);
            }
            
            performFriction = false;
        }
    }
    #endregion Friction

    #region Collision Detection
    public void OnCollisionEnter2D(Collision2D coll)
    {
        // Debug.Log("ground touched");
        if (coll.collider.tag == "Floor" && !Dead)
        {
            playerLowerAnim.Play(GetAnimName("LandLegs"));
            inAir = false;
            remainingJumps = 1;
        }
    }

    public void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.collider.tag == "Floor")
        {
            inAir = false;
            // landing = false;
            remainingJumps = 1;
        }
    }

    public void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.collider.tag == "Floor")
        {
            inAir = true;
            playerLowerAnim.SetTrigger("inAir");
        }
    }
    #endregion

    // Pass the call down to the movement script
    public void blockMovement(float duration)
    {
        StartCoroutine(phm.blockMovement(duration));
    }

    //Sets current control schemes
    public void updateControlScheme()
    {        
        StartCoroutine(ControllerUpdatePause());
    }

    public IEnumerator ControllerUpdatePause()
    {
        yield return new WaitForEndOfFrame();
        up = controlSchemes.up;
        yield return new WaitForEndOfFrame();
        down = controlSchemes.down;
        yield return new WaitForEndOfFrame();
        hori = controlSchemes.hori;
        yield return new WaitForEndOfFrame();
        pause = controlSchemes.pause;
        yield return new WaitForEndOfFrame();
        vert = controlSchemes.vert;
        yield return new WaitForEndOfFrame();
        CAttack = controlSchemes.CAttack;
        yield return new WaitForEndOfFrame();
        RAttack = controlSchemes.RAttack;
        yield return new WaitForEndOfFrame();
        SAttack = controlSchemes.SAttack;
        yield return new WaitForEndOfFrame();
        SaveSystem.SaveGame();
    }

    public IEnumerator Kill()
    {
        playerUpperAnim.Play(GetAnimName("Death"));
        playerLowerAnim.Play("ShavoDashLegs");
        PlayAudioEvent(playerDeath);
        yield return new WaitForSeconds(1.0f);
        SaveSystem.SaveGame();
        yield return new WaitForSeconds(2.0f);
        deathMenu.deathMenuPop();
    }

    public bool isCrouching()
    {
        return crouched;
    }

    private IEnumerator listenForDoubleDownTap()
    {
        bool usingController = false;
        if (Input.GetAxis(vert) > 0.5) usingController = true;
        bool returnedToNeutral = false;

        yield return null;
        float timer = 0f;

        while (timer < 0.30f)
        {
            timer += Time.deltaTime;
            if (Input.GetKeyDown(down) || (usingController && returnedToNeutral
                && Input.GetAxis(vert) > 0.5))
            {
                // Toggle platform colliders off
                for (int i = 0; i < platforms.Length; ++i)
                {
                    if (platforms[i] != null)
                        platforms[i].setColliderEnabled(false);
                }

                float timer2 = 0f;
                while (timer2 < 0.18f)
                {
                    timer2 += Time.deltaTime;
                    yield return null;
                }

                // Toggle platform colliders on
                for (int i = 0; i < platforms.Length; ++i)
                {
                    if (platforms[i] != null)
                        platforms[i].setColliderEnabled(true);
                }
                listeningForDoubleDownTap = false;
                yield break;
            }
            else if (usingController && Input.GetAxis(vert) < 0.42)
            {
                returnedToNeutral = true;
            }
            yield return null;
        }
        listeningForDoubleDownTap = false;
    }

    public void DamagePlayer()
    {
        sr.size = new Vector2(10f, 10f);
        SetCurrentBandMember(currentBandMember);
        if (daronListeningForParry)
        {
            daronListeningForParry = false;
            remainingJumps += 1;
        }

        if (curInvulnerableTime <= 0f)
        {
            curInvulnerableTime = invulnerabilityDuration;
            if (!Dead) StartCoroutine(PlayerFlashOnDamage(invulnerabilityDuration));
        }
    }

    IEnumerator PlayerFlashOnDamage(float duration)
    {
        if (Health != 0 && currentBandMember == "Daron")
        {
            float forgiveTime = 0.14f, t2 = 0f;
            while (t2 < forgiveTime)
            {
                t2 += Time.deltaTime;
                if (daronListeningForParry)
                {
                    curInvulnerableTime += forgiveTime;
                    daronListeningForParry = false;
                    yield break;
                }
                yield return null;
            }

        }
        Health -= 1;
        if (Health != 0)
        {
            PlayAudioEvent(playerHit);

            float initialPeriod = 0.25f, finalPeriod = 0.05f;
            float curPeriod;
            float timer = 0f, tick = 0f;
            sr.enabled = false;
            if (currentBandMember == "John" || !isSuperActive) srLegs.enabled = false;
            while (timer < duration)
            {
                if (currentBandMember != "John" && isSuperActive)
                {
                    sr.enabled = true;
                    srLegs.enabled = true;
                    yield break;
                }
                curPeriod = Mathf.Lerp(initialPeriod, finalPeriod, timer / duration);
                timer += Time.deltaTime;
                tick += Time.deltaTime;
                if (tick > curPeriod / 2)
                {
                    sr.enabled = !sr.enabled;
                    if (currentBandMember == "John" || !isSuperActive) srLegs.enabled = sr.enabled;
                    tick = 0f;
                }
                yield return null;
            }
            sr.enabled = true;
            if (currentBandMember == "John" || !isSuperActive) srLegs.enabled = true;
        }
    }
}