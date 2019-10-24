using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CandleEmitter : MonoBehaviour {

	public GameObject projectile;
	public float minFireScale, maxFireScale;
	private float fireScaleRange;
	
	private GameObject player;
	private int maxShots, currShots;
	private float timer, disableTimer;
	private float firePeriod = 2.0f;
	private Transform candleGlow;
	private GameObject fire1, fire2;
	private Vector2 fire1Start, fire2Start;
	private bool active;
	private ProjectileSpeed speed;

	void Start()
	{
		speed = ProjectileSpeed.Slow;
		player = GameObject.Find("Player");
		candleGlow = transform.GetChild(0);
		fire1 = transform.GetChild(1).GetChild(0).gameObject;
		fire2 = transform.GetChild(1).GetChild(1).gameObject;
		fire1Start = fire1.transform.position;
		fire2Start = fire2.transform.position;
		timer = 0f;
		currShots = 0;
		maxShots = 6;
		disableFire();
		fireScaleRange = maxFireScale - minFireScale;
	}

	void Update()
	{
		if(active)
		{
			timer += Time.deltaTime;
			disableTimer += Time.deltaTime;
			if(timer > firePeriod)
			{
				timer %= firePeriod;
				StartCoroutine(createProjectile());
				++currShots;
			}
			if(currShots >= maxShots) disableFire();
			float scaleVelocity = 3 * timer * fireScaleRange / firePeriod * firePeriod;
			float scale = minFireScale + scaleVelocity;
			fire1.transform.localScale = new Vector2(scale, scale);
			fire2.transform.localScale = new Vector2(scale, scale);
			fire1.transform.position = fire1Start + new Vector2(0, scaleVelocity/2f);
			fire2.transform.position = fire2Start + new Vector2(0, scaleVelocity/2f);
		}
		else
		{
			timer = 0f;
			disableTimer = 0f;
			currShots = 0;
			fire1.transform.position = fire1Start;
			fire2.transform.position = fire2Start;
		}
	}

	IEnumerator createProjectile()
	{
		if(!player)	player = GameObject.Find("Player");
		GameObject projectileClone = Instantiate(projectile, candleGlow.position, transform.rotation);
		yield return null;
        if(projectileClone == null)
        {
            //the projectile was removed by the player on its first frame after Instatiate
            yield break;
        }
		projectileClone.GetComponent<Projectile>().Configure(player, ProjectileType.Honing, speed, 0.0f);
		projectileClone.transform.localScale = new Vector2(3.6f, 3.6f);
	}

	public void enableFire()
	{
		active = true;
		fire1.SetActive(true);
		fire2.SetActive(true);
		candleGlow.gameObject.SetActive(true);
	}

	public void disableFire()
	{
		active = false;
		fire1.SetActive(false);
		fire2.SetActive(false);
		candleGlow.gameObject.SetActive(false);
		fire1.transform.localScale = new Vector2(minFireScale, minFireScale);
		fire2.transform.localScale = new Vector2(minFireScale, minFireScale);
		fire1.transform.position = fire1Start;
		fire2.transform.position = fire2Start;
	}

	public void setFirePeriod(float firePeriod)
	{
		firePeriod = firePeriod;
	}

	public void setMaxShots(int maxShots)
	{
		this.maxShots = maxShots;
	}

	public bool getCandleActive()
	{
		return active;
	}

	public void setSpeed(int speed)
	{
		if(speed == 0) this.speed = ProjectileSpeed.Slow;
		else if(speed == 1) this.speed = ProjectileSpeed.Med;
		else this.speed = ProjectileSpeed.Med;
	}
	
	public void hit()
	{
		disableFire();
	}
}