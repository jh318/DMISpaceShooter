using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollMaterial : MonoBehaviour {

	Renderer render;
	Vector2 offset;
	public float speed = 2.0f;

	void Start(){
		render = GetComponent<Renderer> ();
	}

	void Update(){
		offset.x = offset.x + Time.deltaTime * speed;
		offset.x %= 1f;
		render.material.SetTextureOffset ("_MainTex", offset);
	}
}
