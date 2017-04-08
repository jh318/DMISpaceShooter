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
	public float spinThreshold = 15;
	public ParticleSystem shieldParticles;
	public Gradient shieldGradient;
	public ParticleSystem gunChargeParticles;
	public Gradient gunChargeGradient;
	public GameObject chargedShot;
	public float chargedShotTime = 3.0f;

	//Bebop Gun
	public int bebopDamage = 10;
	public float bebopForce = 200.0f;

	//public float pitch = 20;
	private float chargedShotPower;
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
			StartCoroutine ("ChargingShot");
		}
		if (Input.GetButtonUp("Jump") == true && chargedShotPower >= 3) {
			StopCoroutine ("ChargingShot");
			chargedShotPower = 0;
			Debug.Log("Charge");
			ShootBebopGun ();
		}



		body.angularVelocity = Mathf.Lerp (body.angularVelocity, 0, Time.deltaTime * recoveryTime);
		if (Mathf.Abs (body.angularVelocity) < spinThreshold) {
			transform.right = Vector3.Lerp (transform.right, Vector3.right, Time.deltaTime * recoveryTime);
		}


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

	void ChargedShot(){
		StartCoroutine ("ChargingShot");
	}
	IEnumerator ChargingShot(){
		for (float t = 0; t < chargedShotTime; t += Time.deltaTime) {
			chargedShotPower++;
			Debug.Log("Charging...");
			gunChargeParticles.Play ();
			yield return new WaitForSeconds (1);
		}
	}

	IEnumerator GunCoolDown(){
		//GunReady = false;
		yield return new WaitForSeconds (2);
		gunReady = true;
	}

	void ShootBebopGun(){
		Vector3 hitPosition = gun.position + gun.right * 10;
		RaycastHit2D hit = Physics2D.CircleCast(gun.position, 0.125f, gun.right);
		gunChargeParticles.Stop ();
		if (hit.collider != null) {
			Debug.Log ("BebopHit!!!");
			hitPosition = hit.transform.position;
			GetComponent<LineRenderer> ().SetPositions (new Vector3[] {gun.position, hit.transform.position});
			if (hit.collider.gameObject.GetComponent<EnemyController> ()) {
				hit.collider.gameObject.GetComponent<EnemyController> ().healthPoints -= bebopDamage;
				hit.collider.gameObject.GetComponent<Rigidbody2D> ().AddForce (transform.right * bebopForce);
			}
		}
		StartCoroutine ("BebopFired", hitPosition);
	}

	IEnumerator BebopFired(Vector3 hitPosition){
		LineRenderer line = GetComponent<LineRenderer> ();
		for (float t = 0; t < 0.3f; t += Time.deltaTime) {
			float frac = t / 0.1f;
			line.SetPositions (new Vector3[]{ gun.position, Vector3.Lerp (gun.position, hitPosition, frac) });
			yield return new WaitForEndOfFrame ();
		}

		Vector3 start = gun.position;
		for (float t = 0; t < 0.7f; t += Time.deltaTime) {
			float frac = t / 0.7f;
			line.SetPositions (new Vector3[]{ Vector3.Lerp (start, hitPosition, frac), hitPosition });
			yield return new WaitForEndOfFrame ();
		}
	}
}
