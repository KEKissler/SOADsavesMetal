using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using FMOD.Studio;

public class DummyCandle: MonoBehaviour
{
	private float fireScaleRange;
	private GameObject player;
	private int maxShots, currShots;
	private float timer, disableTimer;
	private float firePeriod = 2.0f;
	private Transform candleGlow;
	private GameObject fire1, fire2;
	private Vector2 fire1Start, fire2Start;
	private bool active;

	public float minFireScale, maxFireScale;

	#region FMODEvents
	[FMODUnity.EventRef]
	public string candleGrow;
	[FMODUnity.EventRef]
	public string candleExtinguish;
	#endregion

	void Start()
	{
		player = GameObject.Find("Player");
		candleGlow = transform.GetChild(0);
		fire1 = transform.GetChild(1).GetChild(0).gameObject;
		fire2 = transform.GetChild(1).GetChild(1).gameObject;
		fire1Start = fire1.transform.position;
		fire2Start = fire2.transform.position;
		timer = 0f;
		disableFire();
		fireScaleRange = maxFireScale - minFireScale;
	}

	void Update()
	{
		if (active)
		{
			timer += Time.deltaTime;
			disableTimer += Time.deltaTime;
			if (timer > firePeriod)
			{
				timer %= firePeriod;
				StartCoroutine(lerpFlameSizeToMinimum(0.04f));
			}
		}
		else
		{
			timer = 0f;
			disableTimer = 0f;
			fire1.transform.position = fire1Start;
			fire2.transform.position = fire2Start;
			CandleOff();
			enableFire();

		}
	}

	void resizeFlameWhileActive()
	{
		float timerPower = (float)Math.Pow(timer, 1.8f);
		float scaleVelocity = 0.00028f * 3 * timerPower * fireScaleRange;
		float scale = fire1.transform.localScale.x + scaleVelocity;
		setFlameSize(scale);
	}

	void setFlameSize(float scale)
	{
		float scaleDifferentialAboveBase = scale - minFireScale;
		fire1.transform.localScale = new Vector2(scale, scale);
		fire2.transform.localScale = new Vector2(scale, scale);
		fire1.transform.position = fire1Start + new Vector2(0, scaleDifferentialAboveBase / 2f);
		fire2.transform.position = fire2Start + new Vector2(0, scaleDifferentialAboveBase * 0.37f);
	}

	void resetFlameSize()
	{
		fire1.transform.localScale = new Vector2(minFireScale, minFireScale);
		fire2.transform.localScale = new Vector2(minFireScale, minFireScale);
		fire1.transform.position = fire1Start;
		fire2.transform.position = fire2Start;
	}

	IEnumerator CandleOff()
    {
		yield return new WaitForSeconds(5f);
	}

	IEnumerator lerpFlameSizeToMinimum(float maxTimeToLerp)
	{
		float currentFireScaleRange = fire1.transform.localScale.x - minFireScale;
		float timer = 0f;
		float timeToLerp = (maxTimeToLerp < firePeriod) ? maxTimeToLerp : firePeriod;
		while (timer < timeToLerp)
		{
			setFlameSize(minFireScale + (timeToLerp - timer) / timeToLerp * currentFireScaleRange);
			// Make this shrink a little sharper, more of a bounce
			timer += Time.deltaTime;
			yield return null;
		}
		resetFlameSize();
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
		resetFlameSize();
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