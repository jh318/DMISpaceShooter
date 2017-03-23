using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 1;

	public float shootSpeed = 1;

	//public float pitch = 20;

	private Animator anim;

	private Rigidbody2D body;

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
			bullet.transform.position = transform.position;
			bullet.GetComponent<ProjectileController> ().Fire (Vector2.right);
		}
	}
}
