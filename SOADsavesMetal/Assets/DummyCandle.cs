using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using FMOD.Studio;

public class DummyCandle: MonoBehaviour
{
	private static float TIMEOFF = 5f;

	private float disableTimer;
	private Transform candleGlow;
	private GameObject fire1, fire2;
	private Vector2 fire1Start, fire2Start;
	private bool active = true;
	
/*	public float minFireScale, maxFireScale;*/

	#region FMODEvents
	[FMODUnity.EventRef]
	public string candleGrow;
	[FMODUnity.EventRef]
	public string candleExtinguish;
	#endregion

	void Start()
	{
		candleGlow = transform.GetChild(0);
		fire1 = transform.GetChild(1).GetChild(0).gameObject;
		fire2 = transform.GetChild(1).GetChild(1).gameObject;
		enableFire();
	}

	void Update()
	{
		if (!active)
		{
			disableTimer += Time.deltaTime;
			if (disableTimer > TIMEOFF)
			{
				disableTimer = 0f;
				enableFire();
			}
		}		
	}

	public void enableFire()
	{
		active = true;
		fire1.SetActive(true);
		fire2.SetActive(true);
		PlayAudioEvent(candleGrow);
		candleGlow.gameObject.SetActive(true);
	}

	public void disableFire()
	{
		active = false;
		fire1.SetActive(false);
		fire2.SetActive(false);
		candleGlow.gameObject.SetActive(false);
	}

	public bool getCandleActive()
	{
		return active;
	}

	public void hit()
	{
		if (active)
			PlayAudioEvent(candleExtinguish);
		disableFire();
	}

	public void PlayAudioEvent(string fmodEvent)
	{
		EventInstance instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
		instance.start();
	}
}