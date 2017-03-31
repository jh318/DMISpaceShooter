using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public int healthPoints = 1;
	public float speed = 1;

	private Rigidbody2D body;
	private int maxHealth = 0;

	void SetMaxHealth(int x){ maxHealth = x; }
	int GetMaxHealth(){ return maxHealth; }

	void Start(){
		SetMaxHealth (healthPoints);
		body = GetComponent<Rigidbody2D> ();
		body.velocity = Vector2.right * -1 * speed;
	}
		
		

	void Update () {
		if (healthPoints <= 0) {
			gameObject.SetActive (false);
			healthPoints = GetMaxHealth ();
			AudioManager.PlayEffect ("snd_explosion9");
		}
		if (body.velocity.x <= 0) {
			body.velocity = Vector2.right * -1 * speed;
		}
	}

	void OnCollisionEnter2D(Collision2D c){
		if (c.gameObject.GetComponent<EnemyController> () == true) {
			healthPoints -= 3;
		}
	}
}
