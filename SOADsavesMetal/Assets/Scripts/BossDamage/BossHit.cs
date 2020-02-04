using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHit : MonoBehaviour
{
    private const string AGAS_DAMAGE = "agas_damage";
    private const string AGAS_DEATH = "agas_death";
    
    private const string TSOVINAR_DAMAGE = "tsovinar_damage";
    private const string TSOVINAR_DEATH = "tsovinar_death";

    private const string NHANG_DAMAGE = "nhang_damage";
    private const string NHANG_DEATH = "nhang_death";

    private const string SANDARAMET_DAMAGE1 = "sandaramet_phase1_damage";
    private const string SANDARAMET_DAMAGE2 = "sandaramet_phase2_damage";
    private const string SANDARAMET_DEATH = "sandaramet_death";


    public BossHealth healthScript;
    public float damageMultiplier = 1f;
    public AnimationClip damageAnim;
    public AnimationClip damageAnim2;
    public AnimationClip deathAnim;

    private Animator bossAnimations;


    // Start is called before the first frame update
    void Start()
    {
        bossAnimations = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hit(int damage)
    {
        float HP;
        float startHP = healthScript.startingHP;
        float cutoff = 0.5F;
        healthScript.hit(damage * damageMultiplier);
        HP = healthScript.getHP();
        if(name == "TsovinarFace")
        {
            if(HP > 0)
            {
                bossAnimations.Play(TSOVINAR_DAMAGE);
            }
            else
            {
                bossAnimations.Play(TSOVINAR_DEATH);
                Destroy(gameObject, deathAnim.length-cutoff);
                //gameObject.
            }
        }
        else if(name == "Agas")
        {
            if (HP > 0)
            {
                bossAnimations.Play(AGAS_DAMAGE);
            }
            else
            {
                bossAnimations.Play(AGAS_DEATH);
            }
        }
        else if(name == "Boss_Nhang")
        {
            if(HP > 0)
            {
                bossAnimations.Play(NHANG_DAMAGE);
            }
            else
            {
                bossAnimations.Play(NHANG_DEATH);
            }
        }
        else if(name == "sandaramet")
        {
            if(HP > 0)
            {
                if (HP > startHP/2)
                {
                    bossAnimations.Play(SANDARAMET_DAMAGE1);
                }
                else
                {
                    bossAnimations.Play(SANDARAMET_DAMAGE2);
                }
            }
            else
            {
                bossAnimations.Play(SANDARAMET_DEATH);
            }
        }

    }
}
