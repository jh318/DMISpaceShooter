using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSomething : MonoBehaviour {

	public string projectileName = "RedLaserPrefab";

	public float speed;

	GameObject projectilePrefab;
	Vector3 spawnLocation;
	Vector3 targetPosition;
	GameObject target;

	void OnEnable(){
		//projectilePrefab = Spawner.Spawn(projectileName);
		StartCoroutine ("ShootProjectile");
	}

	IEnumerator ShootProjectile(){
		while (enabled) {
			GameObject tempProjectile;
			spawnLocation = transform.position;
			targetPosition = PlayerController.player.transform.position;
			tempProjectile = Spawner.Spawn (projectileName);
			tempProjectile.transform.position = spawnLocation;
			tempProjectile.SetActive (true);
			tempProjectile.GetComponent<Rigidbody2D> ().velocity = (targetPosition - spawnLocation) * speed;
			yield return new WaitForSeconds (3);
		}
	}
}
