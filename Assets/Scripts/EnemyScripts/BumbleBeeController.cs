using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumbleBeeController : EnemyController {

	//public Vector3 startPosition;
	public Vector3 endPosition;

	protected override void Start (){
		base.Start ();
		endPosition.x = Random.Range (0.0f, 5.0f);
		endPosition.y = Random.Range (-5.0f, 5.0f);
	}

	protected override void Update(){
		base.Update ();
		transform.position = Vector3.Lerp (transform.position, endPosition, (speed * Time.deltaTime));
		//transform.position = Vector3.Lerp (transform.rotation, transform.LookAt), Time.deltaTime);
		transform.right = Vector3.Lerp(
			transform.right, 
			(transform.position - PlayerController.player.transform.position).normalized, 
			Time.deltaTime * speed
		);
	}
}
