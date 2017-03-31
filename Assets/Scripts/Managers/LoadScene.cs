using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

	public static LoadScene instance;
	public string level;

	void Awake(){
		if (instance == null) {
			instance = this;
		}
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Jump")) {
			LoadLevel ();
		}
	}

	void LoadLevel(){
		SceneManager.LoadScene (level);
	}
}
