using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CandleEmitter : MonoBehaviour {

	public GameObject projectile;
	private GameObject player;
	private float timer, disableTime, disableTimer;
	private float fireTime = 2.0f;
	private Transform candleGlow;
	private GameObject fire1, fire2;
	private bool active;
	private ProjectileSpeed speed;

	void Start()
	{
		speed = ProjectileSpeed.Slow;
		player = GameObject.Find("Player");
		candleGlow = transform.GetChild(0);
		fire1 = transform.GetChild(1).GetChild(0).gameObject;
		fire2 = transform.GetChild(1).GetChild(1).gameObject;
		timer = 0f;
		disableTime = 1200f;
		disableFire();
	}

	void Update()
	{
		if(active)
		{
			timer += Time.deltaTime;
			disableTimer += Time.deltaTime;
			if(timer > fireTime)
			{
				timer %= fireTime;
				StartCoroutine(createProjectile());
			}
			if(disableTimer > disableTime) disableFire();
		}
		else
		{
			timer = 0f;
			disableTimer = 0f;
		}
	}

	IEnumerator createProjectile()
	{
		if(!player)	player = GameObject.Find("Player");
		GameObject projectileClone = Instantiate(projectile, candleGlow.position, transform.rotation);
		yield return null;
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
	}

	public void setFirePeriod(float firePeriod)
	{
		fireTime = firePeriod;
	}

	public void setDisableAfter(float disableTime)
	{
		this.disableTime = disableTime;
	}

	public bool getFire()
	{
		return active;
	}

	public void setSpeed(int speed)
	{
		if(speed == 0) this.speed = ProjectileSpeed.Slow;
		else if(speed == 1) this.speed = ProjectileSpeed.Med;
		else this.speed = ProjectileSpeed.Med;
	}
}