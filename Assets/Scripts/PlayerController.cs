using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public static PlayerController player;
	public float speed = 1;
	public float shootSpeed = 1;
	public Transform gun;
	public string explosionName = "Explosion1Particle";


	//public float pitch = 20;

	private Animator anim;
	private Rigidbody2D body;


	void Awake(){
		if (player == null) {
			player = this;
		}
	}

	void Start(){
		body = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		float y = Input.GetAxis ("Vertical");
		body.velocity = Vector2.up * y * speed;
		anim.SetFloat ("Pitch", y);
		//transform.rotation.z = y * pitch;

		if (Input.GetButtonDown ("Jump")) {
			GameObject bullet = Spawner.Spawn ("Square");
			AudioManager.PlayEffect ("snd_explosion3", Random.Range(0.9f, 1.1f), Random.Range(0.8f, 1.0f));
			bullet.transform.position = gun.position;
			bullet.GetComponent<ProjectileController> ().Fire (gun.right);
		}
	}

	void OnCollisionEnter2D(Collision2D c){
		if (c.gameObject.GetComponent<EnemyController> ()) {
			AudioManager.PlayEffect ("snd_explosion9");
			gameObject.SetActive (false);
			GameObject explosion = Spawner.Spawn (explosionName);
			explosion.transform.position = transform.position;
			explosion.SetActive (true);
			explosion.GetComponent<ParticleSystem> ().Play ();
		}
	}
}
