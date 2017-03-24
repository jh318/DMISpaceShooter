using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public int healthPoints = 1;
	public float speed = 1;

	private int maxHealth = 0;

	void SetMaxHealth(int x){ maxHealth = x; }
	int GetMaxHealth(){ return maxHealth; }

	void Awake(){
		SetMaxHealth (healthPoints);
	}
		

	void Update () {
		if (healthPoints <= 0) {
			gameObject.SetActive (false);
			healthPoints = GetMaxHealth ();
		}
	}





}
