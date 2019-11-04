using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortRangeDmg : MonoBehaviour {

	public int damage = 111;
	bool canHit = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Boss" && canHit) {
			col.gameObject.SendMessage("hit", damage);
			canHit = false;
		}
		else if(col.gameObject.tag == "Projectile")
			Destroy(col.gameObject);
		else if(col.gameObject.tag == "ShortRangeHittable") {
			col.gameObject.SendMessage("hit");
			Debug.Log("b");
		}
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if(col.gameObject.tag == "Boss" && canHit)
		{
			col.gameObject.SendMessage("hit", damage);
			canHit = false;
		}
		else if(col.gameObject.tag == "Projectile")
			Destroy(col.gameObject);
		else if(col.gameObject.tag == "ShortRangeHittable") {
			col.gameObject.SendMessage("hit");
		}
	}

	void refreshHit()
	{
		canHit = true;
	}
}
