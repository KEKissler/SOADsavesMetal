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

    private const string SANDARAMET_DAMAGE = "sandaramet_damage";
    private const string SANDARAMET_DEATH = "sandaramet_death";


    public BossHealth healthScript;
    public float damageMultiplier = 1f;
    public AnimationClip damageAnim;
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
        int HP = healthScript.getHP();
        healthScript.hit(damage * damageMultiplier);
        if(name == "TsovinarFace")
        {
            if(HP > 0)
            {
                bossAnimations.Play(TSOVINAR_DAMAGE);
            }
            else
            {
                bossAnimations.Play(TSOVINAR_DEATH);
            }
        }

    }
}
