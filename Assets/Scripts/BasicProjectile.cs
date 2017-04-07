using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour {

	public int damage = 1;

	void OnCollisionEnter2D(Collision2D c){
		if(c.gameObject.GetComponent<PlayerController>()){
			c.gameObject.GetComponent<PlayerController> ().healthPoints -= damage;
			Debug.Log ("collidedwithplayer");
		}
		if (c.gameObject.GetComponent<EnemyController> ()) {
			c.gameObject.GetComponent<EnemyController> ().healthPoints -= damage;
		}
	}
}
