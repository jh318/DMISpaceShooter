using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

	public string[] enemyPrefabNames;

	void Start(){
		StartCoroutine ("SpawnEnemiesCoroutine");
	}

	void Update(){
		if(!PlayerController.player.isActiveAndEnabled){
				StartCoroutine ("GameOverCountdown");
			}
	}

	IEnumerator SpawnEnemiesCoroutine(){
		while (enabled) {
			yield return new WaitForSeconds (1);
			string enemyPrefabName = enemyPrefabNames [Random.Range (0, enemyPrefabNames.Length)];
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


	IEnumerator GameOverCountdown(){
		yield return new WaitForSeconds (3);
		GameOver ();
	}

	void GameOver (){
			SceneManager.LoadScene ("start");
	}
}
