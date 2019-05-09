using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CandleEmitter : MonoBehaviour {

	public GameObject projectile;
	private GameObject player;
	private float timer;
	private const float FIRE_TIME = 2.0f;
	private Transform candleFire;

	void Start()
	{
		player = GameObject.Find("Player");
		candleFire = transform.GetChild(0);
		timer = 0f;
		Destroy(gameObject, 14.1f);
	}

	void Update()
	{
		timer += Time.deltaTime;
		if(timer > FIRE_TIME)
		{
			timer %= FIRE_TIME;
			StartCoroutine(createProjectile());
		}
	}

	IEnumerator createProjectile()
	{
		GameObject projectileClone = Instantiate(projectile, candleFire.position, transform.rotation);
		yield return null;
		projectileClone.GetComponent<Projectile>().Configure(ProjectileType.Linear, ProjectileSpeed.Slow, 0.0f);
		projectileClone.transform.localScale = new Vector2(3.6f, 3.6f);
	}

}