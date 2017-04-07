using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public static PlayerController player;
	public float speed = 1;
	public float shootSpeed = 1;
	public int maxhealth = 3;
	public Transform gun;
	public string explosionName = "Explosion1Particle";
	public float recoveryTime = 0.8f;
	public ParticleSystem shieldParticles;
	public Gradient shieldGradient;

	//public float pitch = 20;
	private int _healthPoints = 0;
	public int healthPoints {
		get{ 
			return _healthPoints;
		}
		set{ 
			if (_healthPoints > 0 && value <= 0) {
				gameObject.SetActive (false);
			}
			_healthPoints = value;
			ParticleSystem.MainModule main = shieldParticles.main;
			main.startColor = shieldGradient.Evaluate ((float)_healthPoints / (float)maxhealth);
		}
	}
	private bool gunReady = true;

	private Animator anim;
	private Rigidbody2D body;


	void Awake(){
		if (player == null) {
			player = this;
		}
	}

	void OnEnable(){
		healthPoints = maxhealth;
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
			ShootGun ();
		}
		body.angularVelocity = Mathf.Lerp (body.angularVelocity, 0, Time.deltaTime * recoveryTime);
		transform.right = Vector3.Lerp (transform.right, Vector3.right, Time.deltaTime * recoveryTime);
	}

	void OnCollisionEnter2D(Collision2D c){
		if (c.gameObject.GetComponent<EnemyController> ()) {
			AudioManager.PlayEffect ("snd_explosion9");
			healthPoints--;
			GameObject explosion = Spawner.Spawn (explosionName);
			explosion.transform.position = transform.position;
			explosion.SetActive (true);
			explosion.GetComponent<ParticleSystem> ().Play ();
		}
	}

	void ShootGun(){
		if (gunReady) {
			gunReady = false;
			StartCoroutine("GunCoolDown");
			GameObject bullet = Spawner.Spawn ("Square");
			AudioManager.PlayEffect ("snd_explosion3", Random.Range(0.9f, 1.1f), Random.Range(0.8f, 1.0f));
			bullet.transform.position = gun.position;
			bullet.GetComponent<ProjectileController> ().Fire (gun.right);
		}
	}

	IEnumerator GunCoolDown(){
		//GunReady = false;
		yield return new WaitForSeconds (2);
		gunReady = true;
	}

}
