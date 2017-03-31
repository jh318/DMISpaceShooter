using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public int healthPoints = 1;
	public float speed = 1;
	public ParticleSystem explosion;

	private Rigidbody2D _body;
	protected Rigidbody2D body {
		get {
			if (_body == null) {
				_body = GetComponent<Rigidbody2D> ();
			}
			return _body;
		}
	}
	protected int maxHealth = 0;

	void SetMaxHealth(int x){ maxHealth = x; }
	int GetMaxHealth(){ return maxHealth; }

	protected virtual void Start(){
		SetMaxHealth (healthPoints);
	}
		
	protected virtual void Update () {
		if (healthPoints <= 0) {
			gameObject.SetActive (false);
			healthPoints = GetMaxHealth ();
			AudioManager.PlayEffect ("snd_explosion9");
			Instantiate (explosion, transform.position, Quaternion.identity);
		}
	}

	protected virtual void OnCollisionEnter2D(Collision2D c){
		if (c.gameObject.GetComponent<EnemyController> () == true) {
			healthPoints -= 3;
		}
	}


}
