using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Attack", menuName = "Attack")]
public class BossAttacks : ScriptableObject
{
    

    public string name;
    public string description;

    public Animation animation;

    public ProjectileSpeed projectileSpeed;
    //public int speed;
    public int damage;
    public int duration;


}
