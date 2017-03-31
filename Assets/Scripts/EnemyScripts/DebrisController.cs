using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisController : EnemyController {


	//protected override void Start () {
	//	base.Start ();
	//	body.velocity = Vector2.right * -1 * speed;
	//}

	protected override void OnEnable(){
		base.OnEnable ();
		body.velocity = Vector2.right * -1 * speed;
	}
}
