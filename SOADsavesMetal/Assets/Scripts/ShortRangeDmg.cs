using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortRangeDmg : MonoBehaviour {

	public int damage = 111;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Boss")
			col.gameObject.SendMessage("hit", damage);
		else if(col.gameObject.tag == "Projectile")
			Destroy(col.gameObject);
	}
}
