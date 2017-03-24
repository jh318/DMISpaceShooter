using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public string[] enemyPrefabNames;

	void Start(){
		StartCoroutine ("SpawnEnemiesCoroutine");
	}

	IEnumerator SpawnEnemiesCoroutine(){
		while (enabled) {
			yield return new WaitForSeconds (1);
			string enemyPrefabName = enemyPrefabNames [Random.Range (0, enemyPrefabNames.Length - 1)];
			GameObject enemy = Spawner.Spawn(enemyPrefabName);
			Vector3 pos = Camera.main.ViewportToWorldPoint (
				new Vector3 (
					0.8f, 
					Random.value, 
					-Camera.main.transform.position.z
				)
			);
			enemy.transform.position = pos;
			enemy.SetActive (true);
		}
	}
}
