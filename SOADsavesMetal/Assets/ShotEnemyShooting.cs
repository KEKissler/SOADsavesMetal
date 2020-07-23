using FMOD.Studio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEnemyShooting : MonoBehaviour
{
    [SerializeField]
    int numberOfShots = 1;
    [SerializeField]
    float longwaitTime = 2;
    [SerializeField]
    float shortWaitTime = .5f;
    [SerializeField]
    GameObject shot;
    [SerializeField]
    public ProjectileSpeed ProjectileSpeed;
    [SerializeField]
    public ProjectileType ProjectileType;

    [FMODUnity.EventRef]
    public string minionProjectile;

    public EventInstance minionInstance;

    // Start is called before the first frame update
    void Start()
    {
        minionInstance = FMODUnity.RuntimeManager.CreateInstance(minionProjectile);
        StartCoroutine(shootRoutine());
    }

    private IEnumerator shootRoutine()
    {
        while(true)
        {
            Debug.Log("Shoot");
            yield return new WaitForSeconds(longwaitTime);
            for(int i = 0; i < numberOfShots; ++i)
            {
                Shoot();
                yield return new WaitForSeconds(shortWaitTime);
            }
        }
    }

    private void Shoot()
    {
        GameObject fireballObject = Instantiate(shot, transform.position, Quaternion.identity);
        if (fireballObject.GetComponent<Projectile>() != null)
        {
            minionInstance.start();
            //fireballObject.GetComponent<Projectile>().Configure(gameObject, ProjectileType, ProjectileSpeed, 0f);
        }
    }
}
