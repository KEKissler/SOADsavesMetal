using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CandleEmitter : MonoBehaviour {

	public GameObject projectile;
	private GameObject player;
	private float timer;
	private const float FIRE_TIME = 2.0f;

	void Start()
	{
		player = GameObject.Find("Player");
		timer = 0f;
	}

	void FixedUpdate()
	{
		timer += Time.deltaTime;
		if(timer > FIRE_TIME)
		{
			timer %= FIRE_TIME;
			//GameObject projectileClone = Instantiate(projectile, transform.position, Transform.LookAt(player.transform));
		}
	}

}