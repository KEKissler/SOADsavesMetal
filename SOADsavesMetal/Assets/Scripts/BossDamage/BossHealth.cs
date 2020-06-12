using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Instructions to use the BossHealth script:
 * 
 * Attach this script to the root object of the boss.
 * For each hittable segment of the boss, tag that segment with "BossHittable" and give it the script "BossHit".
 * Change the damage multiplier on BossHit if desired.
 * 
 * The damage multiplier in this script is a global multiplier that affects all parts.
 * DamageMultiplier in BossHit only affects that particular component.
 */

public struct Phase
{
    int startHealth;
    string name;
}

public class BossHealth : MonoBehaviour
{
    // Public
    public AnimationClip deathAnim;
    public AnimationClip fadeOut;
    public Animator fader;
    public float startingHP = 200f;
    public float damageMultiplier = 1f;
    public BossAttackManager<AgasPhase> agasAttackManager;
    public BossAttackManager<TsovinarPhase> tsovinarAttackManager;
    public BossAttackManager<NhangPhase> nhangAttackManager;
    public BossAttackManager<SandarametPhase> sandarametAttackManager;
    public Player player;

    public SpriteRenderer enemyGameObject;
    

    AudioSource cheering;
    
    [FMODUnity.EventRef]
    public string bossHit;

    public int currentBoss;
    [SerializeField] bool Dead;

    // Should be accessible but not modifiable directly
    [SerializeField] float HP;

    public MusicSetter music;
    

    // Start is called before the first frame update
    void Start()
    {
        Dead = false;
        BossAttackManager<AgasPhase> temp1 = gameObject.GetComponentInChildren<AgasAttackManager>();
        if(temp1)
        {
            agasAttackManager = temp1;
            currentBoss = 1;   
        }
        BossAttackManager<NhangPhase> temp2 = gameObject.GetComponentInChildren<NhangAttackManager>();
        if (temp2)
        {
            nhangAttackManager = temp2;
            currentBoss = 2;
        }
        BossAttackManager<TsovinarPhase> temp3 = gameObject.GetComponentInChildren<TsovinarAttackManager>();
        if (temp3)
        {
            tsovinarAttackManager = temp3;
            currentBoss = 3;
        }
        BossAttackManager<SandarametPhase> temp4 = gameObject.GetComponentInChildren<SandarametAttackManager>();
        if (temp4)
        {
            sandarametAttackManager = temp4;
            currentBoss = 4;
        }
        HP = startingHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            this.Dead = true;
            player.curInvulnerableTime = 30f;
            switch(currentBoss)
            {
                case 1:
                    StaticData.shavoUnlock = true;
                    StaticData.firstLoad = false;
                    break;
                case 2:
                    StaticData.daronUnlock = true;
                    break;
                case 3:
                    StaticData.serjUnlock = true;
                    break;
                default:
                    break;
            }

            music.StopMusic();
            
            StartCoroutine(LoadLevelSelector());
            
        }
    }


    IEnumerator LoadLevelSelector()
    {
        
        yield return new WaitForSecondsRealtime(deathAnim.length-2);
        fader.SetBool("Fade", true);
        
        yield return new WaitForSecondsRealtime(1.0f);

        if (currentBoss == 4 && StaticData.firstPlay.Equals(true))
        {
            SceneManager.LoadScene("EndingSequence");
            StaticData.firstPlay = false;
        }
        else
            SceneManager.LoadScene("Level_Load_Test");
        SaveSystem.SaveGame();

    }
    public bool isDead()
    {
        return Dead;
    }

    public void hitF(float damage)
    {
        HP -= damage * damageMultiplier;            
        if(bossHit != null)
        {
            FMOD.Studio.EventInstance instance = FMODUnity.RuntimeManager.CreateInstance(bossHit);
            instance.start();
        }
        if(agasAttackManager)
        {
            agasAttackManager.bossHit();
        }
        else if (tsovinarAttackManager)
        {
            tsovinarAttackManager.bossHit();
        }
        else if (nhangAttackManager)
        {
            nhangAttackManager.bossHit();
        }
        else if (sandarametAttackManager)
        {
            sandarametAttackManager.bossHit();
        }
        Debug.Log(HP);
    }

    public void changeMultiplier(float multiplier)
    {
        damageMultiplier = multiplier;
    }

    public int getHP()
    {
        return (int)(HP + 0.5f);
    }

    public float getHPPercentage()
    {
        return (float)HP/(float)startingHP * 100f;
    }

    private void makeAllTrue(bool[] characterArray)
    {
        for (int i = 0; i < characterArray.Length; ++i)
        {
            characterArray[i] = true;
        }
    }
}
