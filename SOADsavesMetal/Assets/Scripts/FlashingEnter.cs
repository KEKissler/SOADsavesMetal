using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingEnter : MonoBehaviour {
    private Text enterText;
	private Color color;
	[SerializeField] float fadeSpeed = 1f;
	void Awake(){
		enterText = GetComponent<Text>();
		color = enterText.color;
	}
	void Update (){
		float scale = (Mathf.Sin(Time.time * fadeSpeed) + 1f) / 2f;
		color.a = scale;
		enterText.color = color;
	}
}
