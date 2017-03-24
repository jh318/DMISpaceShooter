using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

	public float initialSpeed = 10;
	public float lifeSpan = 3;

	private Rigidbody2D _body;
	public Rigidbody2D body{
		get{ 
			if (_body == null) _body = GetComponent<Rigidbody2D>();
			return _body;
		}
	}

	void OnCollisionEnter2D(Collision2D c){
		if (c.gameObject.GetComponent<EnemyController>()) {
			c.gameObject.SetActive (false);
			this.gameObject.SetActive (false);
		}
	}

	public void Fire(Vector3 direction){
		gameObject.SetActive (true);
		body.velocity = direction * initialSpeed;
		StartCoroutine ("LifeCycleCoroutine");
	}

	IEnumerator LifeCycleCoroutine(){
		yield return new WaitForSeconds (lifeSpan);
		gameObject.SetActive (false);
	}
}
