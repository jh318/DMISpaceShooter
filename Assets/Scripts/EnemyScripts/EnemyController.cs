using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public int maxHealth = 3;
	public float speed = 1;
	public string explosionName = "Explosion1Particle";

	private Rigidbody2D _body;
	protected Rigidbody2D body {
		get {
			if (_body == null) {
				_body = GetComponent<Rigidbody2D> ();
			}
			return _body;
		}
	}

	protected int _healthPoints = 0;
	public int healthPoints {
		get {
			return _healthPoints;
		}
		set {
			if (_healthPoints > 0 && value <= 0) {
				gameObject.SetActive (false);
				GameManager.instance.score += 1;
				AudioManager.PlayEffect ("snd_explosion9");
				GameObject explosion = Spawner.Spawn (explosionName);
				explosion.transform.position = transform.position;
				explosion.SetActive (true);
				explosion.GetComponent<ParticleSystem> ().Play ();
			}
			_healthPoints = value;
		}
	}

	protected virtual void OnEnable(){
		healthPoints = maxHealth;
	}

	protected virtual void Start(){
	}
		
	protected virtual void Update () {
		
	}

	protected virtual void OnCollisionEnter2D(Collision2D c){
		if (c.gameObject.GetComponent<EnemyController> () == true) {
			healthPoints -= 3;
		}
	}


}
